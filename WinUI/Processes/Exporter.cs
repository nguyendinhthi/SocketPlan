using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using System.IO;
using SocketPlan.WinUI.Properties;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.MSOffice;
using System.Data;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    public class Exporter
    {
        private string filePath;
        private List<Drawing> drawings;
        private List<SocketBoxPickingItem> items = new List<SocketBoxPickingItem>();

        public void Run()
        {
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            dialog.FileName = this.GetDefaultFileName();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            this.filePath = dialog.FileName;

            var progress = new ProgressManager();
            progress.Processes += new CadEventHandler(this.CheckSocketPlan);
            progress.Processes += new CadEventHandler(this.RegisterToDB);
            progress.Processes += new CadEventHandler(this.OutputExcel);
            progress.Run();
        }

        private string GetDefaultFileName()
        {
            var drawings = Drawing.GetAll(false);
            if(drawings.Count == 0)
                return string.Empty;

            var constructionCode = drawings[0].ConstructionCode;
            var planNo = drawings[0].PlanNo;
            var zumenNo = drawings[0].RevisionNo;
            return constructionCode + "-" + planNo + "-" + zumenNo + "-SocketPlanPicking.xls";
        }

        [ProgressMethod("Checking socket plan...")]
        private void CheckSocketPlan()
        {
            var socketPlans = new List<Drawing>();
            foreach (var drawing in Drawing.GetAll(false))
            {
                if (drawing.Name.Contains("Individeual") ||
                    drawing.Name.Contains("Pattern"))
                    continue;

                var directory = Path.Combine(Settings.Default.DrawingDirectory, Static.ConstructionCode);
                var individualName = UnitWiring.GetSocketPlanFileName(drawing.Name, SocketPlanType.Individual) + ".dwg";
                var individualPath = Path.Combine(directory, individualName);
                var patternName = UnitWiring.GetSocketPlanFileName(drawing.Name, SocketPlanType.Pattern) + ".dwg";
                var patternPath = Path.Combine(directory, patternName);
                
                if (File.Exists(individualPath))
                {
                    socketPlans.Add(Drawing.Create(individualPath, "Local"));    
                    continue;
                }

                if (File.Exists(patternPath))
                {
                    socketPlans.Add(Drawing.Create(patternPath, "Local"));
                    continue;
                }

                throw new ApplicationException("Socket plan is not generated yet. Please generate automatically." +
                        Environment.NewLine + Path.GetFileName(individualName) +
                        Environment.NewLine + Path.GetFileName(patternPath));
            }

            var opener = new OpenDrawingForm();
            opener.OpenFilesForSocketPlan(socketPlans);

            // これやると、図面操作できるようになる
            this.drawings = Drawing.GetAllForSocketPlan(SocketPlanType.Individual);
            this.drawings.AddRange(Drawing.GetAllForSocketPlan(SocketPlanType.Pattern));
        }

        [ProgressMethod("Registering to DB...")]
        private void RegisterToDB()
        {
            if(this.drawings.Count == 0)
                return;

            var constructionCode = this.drawings[0].ConstructionCode;
            var planNo = this.drawings[0].PlanNo;
            var zumenNo = this.drawings[0].RevisionNo;

            using (var service = new SocketPlanService())
            {
                this.items.AddRange(service.RegisterSocketBoxPickingItems(constructionCode, planNo, zumenNo));
            }
        }

        [ProgressMethod("Outputing excel...")]
        private void OutputExcel()
        {
            var table = this.CreateTable();
            foreach (var item in items)
            {
                var row = table.NewRow();
                row["ConstructionCode"] = item.ConstructionCode;
                row["PlanNo"] = item.PlanNo;
                row["ZumenNo"] = item.ZumenNo;
                row["Floor"] = item.Floor;
                row["RoomCode"] = item.RoomCode;
                row["RoomName"] = item.RoomName;
                row["Color"] = item.Color;
                row["Shape"] = item.Shape;
                row["SocketBoxName"] = item.SocketBoxName;
                row["Quantity"] = item.Quantity;
                switch (item.SocketBoxSize)
                {
                    case 0:
                        row["SocketBoxSize"] = "None";
                        break;
                    case 1:
                        row["SocketBoxSize"] = "Single";
                        break;
                    case 2:
                        row["SocketBoxSize"] = "Double";
                        break;
                    case 3:
                        row["SocketBoxSize"] = "Triple";
                        break;
                    case 4:
                        row["SocketBoxSize"] = "Quadruple";
                        break;
                    default:
                        row["SocketBoxSize"] = "";
                        break;
                }
                row["SizeQuantity"] = item.SizeQuantity;
                table.Rows.Add(row);
            }

            var temp = Path.GetTempFileName();
            using (var excel = new ExcelProxy(temp))
            {
                using (var sheet = excel.GetSheet("SocketPlan"))
                {
                    sheet.Set(table, true);
                }
            }

            File.Copy(temp, this.filePath, true);
            File.Delete(temp);
        }

        private DataTable CreateTable()
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("ConstructionCode"));
            table.Columns.Add(new DataColumn("PlanNo"));
            table.Columns.Add(new DataColumn("ZumenNo"));
            table.Columns.Add(new DataColumn("Floor"));
            table.Columns.Add(new DataColumn("RoomCode"));
            table.Columns.Add(new DataColumn("RoomName"));
            table.Columns.Add(new DataColumn("Color"));
            table.Columns.Add(new DataColumn("Shape"));
            table.Columns.Add(new DataColumn("SocketBoxName"));
            table.Columns.Add(new DataColumn("Quantity"));
            table.Columns.Add(new DataColumn("SocketBoxSize"));
            table.Columns.Add(new DataColumn("SizeQuantity"));
            return table;
        }
    }
}
