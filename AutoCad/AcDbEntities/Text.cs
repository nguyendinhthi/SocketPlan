using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Text : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbText; } }

        public List<int> GetAll()
        {
            Filter filter = new Filter();
            filter.Add(FilterOption.ObjectType.Text);
            return filter.Execute();
        }

        public string GetText(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "文字列取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get text.");

            return result.Value;
        }

        public void SetText(int objectId, string text)
        {
            if (!this.Set<string>(objectId, "文字列設定", text))
                throw new AutoCadException("Failed to set text.");
        }

        public double GetTextHeight(int objectId)
        {
            Result<double> result = this.Get<double>(objectId, "高さ取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get text height.");

            return result.Value;
        }

        public void SetHeight(int objectId, double height)
        {
            if (!this.Set<double>(objectId, "高さ設定", height))
                throw new AutoCadException("Failed to set height of text.");
        }

        public PointD GetPosition(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "基点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get position of text.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public void SetRotation(int objectId, double rotation)
        {
            if (!this.Set<double>(objectId, "回転角度設定", rotation))
                throw new AutoCadException("Failed to set rotation of multi text.");
        }

        public double GetRotation(int objectId)
        {
            Result<double> result = this.Get<double>(objectId, "回転角度取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get text height.");

            return result.Value;
        }

        public void SetTextStyle(int objectId, string styleName)
        {
            if (!this.Set<string>(objectId, "文字スタイル設定", styleName))
                throw new AutoCadException("Failed to set style of text.");
        }

        public void SetWidthFactor(int objectId, double factor)
        {
            if (!this.Set<double>(objectId, "幅係数設定", factor))
                throw new AutoCadException("Failed to set width factor of text.");
        }

        public void MoveText(int objId, PointD point)
        { //文字を移動したい時は、このメソッドよりSetAlignmentPointを使うと良い。
            if (!this.Set<double[]>(objId, "setPosition", new double[] { point.X, point.Y, 0 }))
                throw new AutoCadException("Failed to move text.");
        }

        public void SetAlignment(int objectId, Align align)
        {
            if (!this.Set<short>(objectId, "位置合わせ設定", (short)align))
                throw new AutoCadException("Failed to set alignment of text.");
        }

        public PointD GetAlignmentPoint(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "位置合わせ点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get alignment point of text.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public void SetAlignmentPoint(int objectId, PointD point)
        {
            if (!this.Set<double[]>(objectId, "位置合わせ点設定", new double[] { point.X, point.Y, 0 }))
                throw new AutoCadException("Failed to set alignment point of text.");
        }

        public int Make(string text, string textStyle, double height, PointD position)
        {
            List<object> setData = new List<object>();

            List<object> point = new List<object>();
            point.Add(position.X);
            point.Add(position.Y);
            point.Add(0d);

            setData.Add(point.ToArray());
            setData.Add(text);
            setData.Add(textStyle);
            setData.Add(height);
            setData.Add(0d);

            Result<int> result = this.Make<object[]>(setData.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to make text.");

            return result.Value;
        }

        public int Make(string text, double size, PointD point, Align align)
        {
            AutoCad.Db.TextStyleTableRecord.Make(Font.MSGothic, Font.MSGothic);
    
            var objId = this.Make(text, Font.MSGothic, size, point);
            this.SetAlignment(objId, align);
            this.SetAlignmentPoint(objId, point);
            return objId;
        }

        public int MakeP(string text, double size, PointD point, Align align)
        {
            AutoCad.Db.TextStyleTableRecord.Make(Font.MSPGothic, Font.MSPGothic);

            var objId = this.Make(text, Font.MSPGothic, size, point);
            this.SetAlignment(objId, align);
            this.SetAlignmentPoint(objId, point);
            return objId;
        }

        public bool IsIntersectForText(int textIdA, int textIdB)
        {
            var isIntersect = AutoCad.Db.Text.GetIntersect2D(textIdA, textIdB).Count > 0;
            if (isIntersect)
                return true;

            var boundA = AutoCad.Db.Text.GetEntityBound(textIdA);
            var boundB = AutoCad.Db.Text.GetEntityBound(textIdB);

            //比較が文字と文字の場合、包含関係にあると、交点なしと判定されてしまう。それ防止。

            if (boundA[0].IsOn(boundB[0], boundB[1], false))
                return true;

            if (boundB[0].IsOn(boundA[0], boundA[1], false))
                return true;

            return false;
        }
    }
}