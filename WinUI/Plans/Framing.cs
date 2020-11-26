using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.SocketPlanServiceReference;
using System.IO;
using SocketPlan.WinUI.Properties;

namespace SocketPlan.WinUI
{
    public class Framing
    {
        //全ての基点となる座標
        private PointD POS_ORIGIN = new PointD(0, 0);
        //図面枠に関する座標
        private PointD POS_DRAWING_BASE = new PointD(7, 4);
        private PointD SIZE_DRAWING = new PointD(395, 277);
        private PointD POS_DRAWING_END { get { return this.POS_DRAWING_BASE.Plus(this.SIZE_DRAWING); } }
        //図面のズームに使う座標
        private PointD MARGIN_ZOOM = new PointD(10, 10);
        private PointD POS_ZOOM_AREA_START { get { return this.POS_ORIGIN.Minus(this.MARGIN_ZOOM); } }
        private PointD POS_ZOOM_AREA_END { get { return this.POS_DRAWING_END.Plus(this.MARGIN_ZOOM); } }
        
        private int scale;
        private SocketPlanType type;

        private List<Drawing> drawings = new List<Drawing>();

        public Framing(int scale, SocketPlanType type)
        {
            this.scale = scale;
            this.type = type;
        }

        public void Run()
        {
            var progress = new ProgressManager();
            progress.Processes += new CadEventHandler(this.InitializeCad);
            progress.Processes += new CadEventHandler(this.CheckSocketPlan);
            progress.Processes += new CadEventHandler(this.DrawFrame);
            progress.Processes += new CadEventHandler(this.DeleteOtherSheets);
            progress.Processes += new CadEventHandler(this.FinalizeCad);
            progress.Run();
        }

        [ProgressMethod("Initializing CAD setting...")]
        private void InitializeCad()
        {
            AutoCad.PrepareAutoProcess();
        }

        [ProgressMethod("Checking socket plan...")]
        private void CheckSocketPlan()
        {
            var socketPlans = new List<Drawing>();
            foreach(var drawing in Drawing.GetAll(false))
            {
                if (drawing.Name.Contains("Individeual") ||
                    drawing.Name.Contains("Pattern"))
                    continue;

                var fileName = UnitWiring.GetSocketPlanFileName(drawing.Name, this.type) + ".dwg";
                var directory = Path.Combine(Settings.Default.DrawingDirectory, Static.ConstructionCode);
                var path = Path.Combine(directory, fileName);
                if (!File.Exists(path))
                    throw new ApplicationException("Socket plan is not generated yet." +
                        Environment.NewLine + Path.GetFileName(fileName));

                var sp = Drawing.Create(path, "Local");
                socketPlans.Add(sp);
            }

            var opener = new OpenDrawingForm();
            opener.OpenFilesForSocketPlan(socketPlans);

            // これやると、図面操作できるようになる
            this.drawings = Drawing.GetAllForSocketPlan(this.type);
        }

        [ProgressMethod("Framing...")]
        private void DrawFrame()
        {
            AutoCad.Db.Database.SetFileDialogMode(false);

            foreach (var drawing in this.drawings)
            {
                WindowController2.BringDrawingToTop(drawing.WindowHandle);

                var layout = this.GetLayout(drawing);
                if (layout == null)
                    throw new ApplicationException("Failed to get template file.");

                this.InsertLayoutTemplate(layout);

                this.SetLayer();

                //ビューポートの縮尺を設定してからこれを呼ばないと、図枠の縮尺が正しく記入されない
                this.InsertLayoutTexts(drawing, layout);

                AutoCad.Command.ZoomAll();
                AutoCad.Command.RefreshExEx();
            }
        }

        private Layout GetLayout(Drawing drawing)
        {
            if (drawing.Floor == 1)
                return UnitWiring.Masters.Layouts.Find(p => p.Id == Const.LayoutId.SocketPlan1F);
            else
                return UnitWiring.Masters.Layouts.Find(p => p.Id == Const.LayoutId.SocketPlan2F);
        }

        private void SetLayer()
        {
            //回路用のレイヤーを作る
            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_SocketPlan, CadColor.BlackWhite, Const.LineWeight.Default);

            //回路用のレイヤーを現在層にする
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan);
        }

        private void InsertLayoutTemplate(Layout layout)
        {
            AutoCad.Command.DropLayout(layout.TabName);
            AutoCad.Command.InsertLayout(layout.TemplatePath, layout.TabName);
            AutoCad.Command.SetCurrentLayout(layout.TabName);
            AutoCad.Command.Zoom(this.POS_ZOOM_AREA_START, this.POS_ZOOM_AREA_END);
            AutoCad.Command.SetGlobalScaleFactor(22.5, false);
            AutoCad.Command.DropWasteLayout();
            AutoCad.Command.ZoolAllInViewport();

            int viewportId = AutoCad.Db.Viewport.GetId();
            AutoCad.Db.Viewport.SetCustomScale(viewportId, 1.0 / this.scale);

            this.HideWasteLayer();
        }

        private void HideWasteLayer()
        {
            var freezeLayers = new List<string>();
            var layerIds = AutoCad.Db.LayerTable.GetLayerIds();
            foreach (var layerId in layerIds)
            {
                var layerName = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);
                if (layerName.Contains(Const.Layer.電気_外周) ||
                    layerName.Contains(Const.Layer.電気_部屋))
                    freezeLayers.Add(layerName);
            }

            var viewportId = AutoCad.Db.Viewport.GetId();
            if (freezeLayers.Count != 0)
                AutoCad.Db.Viewport.SetFrozen(viewportId, freezeLayers);
        }

        private void InsertLayoutTexts(Drawing drawing, Layout currentLayout)
        {
            var texts = new List<LayoutText>();
            using (var service = new SocketPlanService())
            {
                texts = new List<LayoutText>(service.GetLayoutTextsByPlanNo(Static.ConstructionCode, Static.Drawing.PlanNoWithHyphen, currentLayout.Id));
            }
            LayoutDrawer.InsertLayoutTexts(texts);
        }

        [ProgressMethod("Deleting other sheets...")]
        private void DeleteOtherSheets()
        {
            var others = UnitWiring.Masters.Layouts.FindAll(p =>
                p.Id != Const.LayoutId.SocketPlan1F &&
                p.Id != Const.LayoutId.SocketPlan2F);

            foreach (var drawing in this.drawings)
            {
                drawing.Focus();
                others.ForEach(p => AutoCad.Command.DropLayout(p.TabName));
            }
        }

        [ProgressMethod("Finalizing...")]
        private void FinalizeCad()
        {
            Drawing.Bring1FDrawingToTop(this.type);
            AutoCad.FinalizeAutoProcess();

            if(this.drawings.Count == 0)
                return;

            var constructionCode = this.drawings[0].ConstructionCode;
            var planNo = this.drawings[0].PlanNo;
            var zumenNo = this.drawings[0].RevisionNo;

            using (var service = new SocketPlanService())
            {
                service.LogSocketPlanFramed(constructionCode, planNo, zumenNo);
            }
        }
    }
}
