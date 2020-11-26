using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Leader : Curve
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLeader; } }

        private void AddVertex(int objectId, PointD point)
        {
            if (!this.Set<double[]>(objectId, "頂点追加", new double[] { point.X, point.Y, 0 }))
                throw new AutoCadException("Failed to add vertex of leader.");
        }

        public int MakeLeader()
        {
            Result<int> result = base.Make();
            if (!result.Success)
                throw new AutoCadException("Failed to make leader.");

            return result.Value;
        }

        public int Make(List<PointD> points)
        {
            var id = this.MakeLeader();

            foreach (var point in points)
            {
                this.AddVertex(id, point);
            }

            return id;
        }

        public int Make(List<PointD> points, int? arrowHeadBlockId)
        {
            var leaderId = this.MakeLeader();

            foreach (var point in points)
            {
                this.AddVertex(leaderId, point);
            }

            this.SetArrowHead(leaderId, arrowHeadBlockId);
            this.SetLineWeight(leaderId, LineWeight._0_00);

            return leaderId;
        }

        public int Make(List<PointD> points, int? arrowHeadBlockId, int lineWeight)
        {
            var leaderId = this.MakeLeader();

            foreach (var point in points)
            {
                this.AddVertex(leaderId, point);
            }

            this.SetArrowHead(leaderId, arrowHeadBlockId);
            this.SetLineWeight(leaderId, lineWeight);

            return leaderId;
        }

        public int Make(List<PointD> points, string text, double textSize, out int textId)
        {
            MText.Attachment attachment;
            if (points[0].X < points[1].X)
                attachment = MText.Attachment.左下;
            else
                attachment = MText.Attachment.右下;

            textId =AutoCad.Db.MText.Make(text, Font.MSGothic, textSize, points[1], attachment);
            var leaderId = this.Make(points);

            this.AttachAnnotation(leaderId, textId);
            this.SetDimtad(leaderId, 3);//JIS

            return leaderId;
        }

        public int Make(List<PointD> points, string text, double textSize)
        {
            int textId;
            return this.Make(points, text, textSize, out textId);
        }

        public int Make(List<PointD> points, string text, double textSize, CadColor color)
        {
            int textId;
            var leaderId = this.Make(points, text, textSize, out textId);

            AutoCad.Db.Leader.SetLineColor(leaderId, color);
            AutoCad.Db.MText.SetColor(textId, color);

            return leaderId;
        }

        public int Make(List<PointD> points, string text, double textSize, CadColor leaderColor, CadColor textColor)
        {
            int textId;
            var leaderId = this.Make(points, text, textSize, out textId);

            AutoCad.Db.Leader.SetLineColor(leaderId, leaderColor);
            AutoCad.Db.MText.SetColor(textId, textColor);

            return leaderId;
        }

        public void AttachAnnotation(int leaderId, int annotationId)
        {
            if (!this.Set(leaderId, "注釈設定", annotationId))
                throw new AutoCadException("Failed to attach annotation.");
        }

        /// <summary>引き出し線を文字のどこに繋げるかを設定する</summary>
        public void SetDimtad(int leaderId, short dimtad)
        {
            if (!this.Set(leaderId, "寸法線上記入設定", dimtad))
                throw new AutoCadException("Failed to attach dimtad.");
        }

        public void SetLineColor(int leaderId, CadColor color)
        {
            if (!this.Set(leaderId, "寸法線の色設定", color.Code))
                throw new AutoCadException("Failed to set color to leader.");
        }

        public int GetAnnotationId(int leaderId)
        {
            var result = this.Get<int>(leaderId, "注釈ID取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get annotation text.");

            return result.Value;
        }

        private void SetArrowHead(int objectId, int? arrowHeadBlockId)
        {
            if (!this.Do(objectId, "矢印付き設定"))
                throw new AutoCadException("Failed to set arrowhead of leader.");

            if (arrowHeadBlockId.HasValue)
            {
                if (!this.Set<double>(objectId, "矢印サイズ設定", 1))
                    throw new AutoCadException("Failed to set arrowhead of leader.");

                if (!this.Set<int>(objectId, "引出線矢印タイプ設定", arrowHeadBlockId.Value))
                    throw new AutoCadException("Failed to set arrowhead of leader.");
            }
            else
            {
                if (!this.Set<double>(objectId, "矢印サイズ設定", 100))
                    throw new AutoCadException("Failed to set arrowhead of leader.");

                if (!this.Set<string>(objectId, "引出線矢印タイプ設定", "_OPEN30"))
                    throw new AutoCadException("Failed to set arrowhead of leader.");
            }
        }

        public PointD GetFirstVertex(int leaderId)
        {
            Result<double[]> result = this.Get<double[]>(leaderId, "先頭頂点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get first vertex of leader.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public PointD GetLastVertex(int leaderId)
        {
            Result<double[]> result = this.Get<double[]>(leaderId, "最終頂点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get last vertex of leader.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public int GetVertexNumber(int leaderId)
        {
            var result = this.Get<int>(leaderId, "頂点数取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get vertex number.");

            return result.Value;
        }
    }
}