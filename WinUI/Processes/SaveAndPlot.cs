using System.Collections.Generic;
using Edsa.AutoCadProxy;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace SocketPlan.WinUI
{
    public class SaveAndPlot
    {
        #region 内部クラス
        private class PdfInfo
        {
            public PdfInfo()
            {
                this.Pages = new List<PageInfo>();
            }

            public string PdfKind { get; set; }
            public string Floor { get; set; }
            public string FileName { get; set; }
            public string DwgFileName { get; set; }
            public List<PageInfo> Pages { get; set; }
        }

        private class PageInfo
        {
            public int PageIdx { get; set; }
            public string LayoutName { get; set; }
        }
        #endregion

        private List<PdfInfo> pdfFileInfos = new List<PdfInfo>();
        private List<Drawing> socketPlanDrawings = new List<Drawing>();
        private string plotterName = "";
        private string directoryPath = "";

        public void Run()
        {
            this.Prepare();
            this.GetExportDrawings();
            this.PlotDWG();
            this.SortPdfInfos();
            this.WaitPrintFinished();
            this.CreateMargedPdf();
            this.SaveDrawings();
            this.DeleteWorkPdf();
        }

        [ProgressMethod("Prepare...")]
        private void Prepare()
        {
            if (AutoCad.Status.IsJapanese())
                this.plotterName = "ISO 拡張 A3 (420.00 x 297.00 ミリ)";
            else
                this.plotterName = "ISO expand A3 (420.00 x 297.00 MM)";

            this.directoryPath = Properties.Settings.Default.DrawingDirectory + Static.ConstructionCode + "\\";
        }

        [ProgressMethod("Getting drawings...")]
        public void GetExportDrawings()
        {
            AutoCad.Command.CloseLayerManager();
            var drawingHandles = WindowController2.GetDrawingHandles();
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {

                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;

                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                if (!drawing.IsSocketPlan)
                    continue;

                this.socketPlanDrawings.Add(drawing);
            }
        }

        [ProgressMethod("Plotting pdf...")]
        private void PlotDWG()
        {
            AutoCad.Db.Database.SetFileDialogMode(false);
            foreach (var drawing in this.socketPlanDrawings)
            {
                drawing.Focus();

                var fileInfo = this.CreatePdfInfo(drawing);
                if (System.IO.File.Exists(directoryPath + fileInfo.FileName))
                    System.IO.File.Delete(directoryPath + fileInfo.FileName);

                System.Threading.Thread.Sleep(100);
                AutoCad.Command.SendLineEsc("-EXPORT P A " + directoryPath + fileInfo.FileName);

                this.pdfFileInfos.Add(fileInfo);
            }
        }


        [ProgressMethod("Sorting pdf files...")]
        private void SortPdfInfos()
        {
            this.pdfFileInfos.Sort((p, q) =>
            {
                var pNameArray = p.DwgFileName.Split('_');
                var qNameArray = q.DwgFileName.Split('_');
                return pNameArray[pNameArray.Length - 1].CompareTo(qNameArray[qNameArray.Length - 1]);
            });
        }

        private PdfInfo CreatePdfInfo(Drawing drawing)
        {
            var pdfInfo = new PdfInfo();
            pdfInfo.FileName = drawing.FileName + ".pdf";
            pdfInfo.DwgFileName = drawing.FileName;
            pdfInfo.Floor = drawing.FloorCode;
            pdfInfo.PdfKind = drawing.Suffix;

            drawing.Focus();
            var layoutIds = AutoCad.Db.Dictionary.GetLayoutIds();
            foreach (var id in layoutIds)
            {
                var name = AutoCad.Db.Layout.GetName(id);
                if (name == "Model")
                    continue;

                var pageInfo = new PageInfo();
                pageInfo.PageIdx = AutoCad.Db.Layout.GetTabOrder(id) - 1;
                pageInfo.LayoutName = name;
                pdfInfo.Pages.Add(pageInfo);

                //ページ設定もここでやってしまう
                AutoCad.Command.SetCurrentLayout(name);
                System.Threading.Thread.Sleep(50);
                var cmd = "-PLOT Y " + name + "\nDWG To PDF.pc3\n" + this.plotterName + "\nM L N E F\nC\nN \nY N N N ./DUMMY.DWG\nY N";
                AutoCad.Command.SendLineEsc(cmd);
            }
            return pdfInfo;
        }

        /// <summary>
        /// 印刷完了待ち
        /// </summary>
        [ProgressMethod("Waiting for a plotter...")]
        private void WaitPrintFinished()
        {
            decimal timer = 0;
            while (true)
            {
                bool finished = true;
                foreach (var pdf in this.pdfFileInfos)
                {
                    if (!System.IO.File.Exists(this.directoryPath + pdf.FileName))
                        finished = false;
                }

                if (finished)
                    break;

                System.Threading.Thread.Sleep(1000);
                timer += 1000;

                //タイムアウト5分
                if (timer > 300000)
                    throw new ApplicationException("Plotting is time out.\nPlease try again.");
            }
        }

        [ProgressMethod("Editting pdf files...")]
        private void CreateMargedPdf()
        {
            PdfDocument outputDocument = new PdfDocument();
            outputDocument.PageLayout = PdfPageLayout.OneColumn;
            outputDocument.PageMode = PdfPageMode.UseOC;
            foreach (var pdfInfo in this.pdfFileInfos)
            {
                PdfDocument inputDocument = PdfReader.Open(this.directoryPath + pdfInfo.FileName, PdfDocumentOpenMode.Import);
                foreach (var pageInfo in pdfInfo.Pages)
                {
                    PdfPage page = inputDocument.Pages[pageInfo.PageIdx];
                    outputDocument.AddPage(page);
                }

                var idx = this.pdfFileInfos.IndexOf(pdfInfo);
                if (idx == this.pdfFileInfos.Count - 1)
                {
                    if (outputDocument.Pages.Count > 0)
                    {
                        var code = this.socketPlanDrawings[0].ConstructionCodePlanRevisionNo;
                        string newFileName = code + "-HSP.pdf";

                        if (System.IO.File.Exists(this.directoryPath + newFileName))
                            System.IO.File.Delete(this.directoryPath + newFileName);

                        outputDocument.Save(this.directoryPath + newFileName);
                    }
                    outputDocument.Dispose();
                }
            }
        }

        [ProgressMethod("Saving...")]
        public void SaveDrawings()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Db.Database.SetFileDialogMode(false);

            foreach (var dwg in this.socketPlanDrawings)
            {
                WindowController2.BringDrawingToTop(dwg.WindowHandle);

                AutoCad.File.Save(this.directoryPath + dwg.FileName, true);
            }

            AutoCad.Db.Database.SetFileDialogMode(true);
        }

        [ProgressMethod("Finishing...")]
        public void DeleteWorkPdf()
        {
            foreach (var pdfInfo in this.pdfFileInfos)
            {
                if (System.IO.File.Exists(this.directoryPath + pdfInfo.FileName))
                    System.IO.File.Delete(this.directoryPath + pdfInfo.FileName);
            }
        }
    }
}
