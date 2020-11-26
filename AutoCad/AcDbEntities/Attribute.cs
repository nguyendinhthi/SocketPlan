namespace Edsa.AutoCadProxy
{
    public class Attribute : Text
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbAttribute; } }

        public string GetTag(int objectId)
        {
            var result = this.Get<string>(objectId, "タグ取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get attribute tag.");

            return result.Value;
        }

        public new void SetVisible(int objectId, bool visible)
        {
            if (!this.Set<short>(objectId, "非表示設定", visible ? (short)0 : (short)1))
                throw new AutoCadException("Failed to set visible of attribute.");
        }

        public bool GetVisible(int objectId)
        {
            var result = this.Get<short>(objectId, "非表示？");
            if (!result.Success)
                throw new AutoCadException("Failed to get attribute visible.");

            return result.Value == 0; //0だとvisible
        }

        /// <summary>ブロックの左下(右上)からの相対座標を指定して属性を追加する</summary>
        public int Make(int blockId, string title, string value, PointD relativePoint, bool pointFrom左下, double textHeight)
        {
            var blockBound = AutoCad.Db.BlockReference.GetBlockBound(blockId);
            PointD basePoint;
            if (pointFrom左下)
                basePoint = blockBound[0];
            else
                basePoint = blockBound[1];

            PointD attPoint = new PointD(basePoint.X + relativePoint.X, basePoint.Y + relativePoint.Y);
            int attributeId = AutoCad.Db.BlockReference.AddAttribute(blockId, title, value, attPoint);

            AutoCad.Db.Text.SetHeight(attributeId, textHeight);

            AutoCad.Db.TextStyleTableRecord.Make(Font.MSGothic, Font.MSGothic);
            AutoCad.Db.Text.SetTextStyle(attributeId, Font.MSGothic);
            AutoCad.Db.Text.SetColor(attributeId, CadColor.Red);

            return attributeId;
        }

        /// <summary>ブロックの中心に属性を追加する</summary>
        public int Make(int blockId, string title, string value, CadColor color, double textHeight)
        {
            var blockBound = AutoCad.Db.BlockReference.GetBlockBound(blockId);
            var ld = blockBound[0];
            var ru = blockBound[1];
            var center = new PointD((ld.X + ru.X) / 2, (ld.Y + ru.Y) / 2);

            int attributeId = AutoCad.Db.BlockReference.AddAttribute(blockId, title, value, center);

            AutoCad.Db.Text.SetHeight(attributeId, textHeight);

            AutoCad.Db.TextStyleTableRecord.Make(Font.MSGothic, Font.MSGothic);
            this.SetTextStyle(attributeId, Font.MSGothic);
            AutoCad.Db.Entity.SetColor(attributeId, color);

            AutoCad.Db.Text.SetAlignment(attributeId, Align.中央);
            AutoCad.Db.Text.SetAlignmentPoint(attributeId, center);

            return attributeId;
        }

        /// <summary>ブロックの右下(ブロック内)に属性を追加する</summary>
        public int MakeRightDown(int blockId, string title, string value, CadColor color, double textHeight)
        {
            //ブロックの右下の座標を取得する
            var bound = AutoCad.Db.BlockReference.GetBlockBound(blockId);
            PointD rightDown = new PointD(bound[1].X, bound[0].Y);

            int attributeId = AutoCad.Db.BlockReference.AddAttribute(blockId, title, value, rightDown);

            AutoCad.Db.Text.SetHeight(attributeId, textHeight);
            AutoCad.Db.TextStyleTableRecord.Make(Font.MSGothic, Font.MSGothic);
            this.SetTextStyle(attributeId, Font.MSGothic);
            AutoCad.Db.Entity.SetColor(attributeId, color);

            AutoCad.Db.Text.SetAlignment(attributeId, Align.右下);
            AutoCad.Db.Text.SetAlignmentPoint(attributeId, rightDown);

            return attributeId;
        }

        /// <summary>ブロックの右上(ブロック内)に属性を追加する</summary>
        public int MakeRightUp(int blockId, string title, string value, CadColor color, double textHeight)
        {
            //ブロックの右上の座標を取得する
            var bound = AutoCad.Db.BlockReference.GetBlockBound(blockId);
            PointD rightUp = new PointD(bound[1].X, bound[1].Y);
            int attributeId = AutoCad.Db.BlockReference.AddAttribute(blockId, title, value, rightUp);

            AutoCad.Db.Text.SetHeight(attributeId, textHeight);
            AutoCad.Db.TextStyleTableRecord.Make(Font.MSGothic, Font.MSGothic);
            this.SetTextStyle(attributeId, Font.MSGothic);
            AutoCad.Db.Entity.SetColor(attributeId, color);

            AutoCad.Db.Text.SetAlignment(attributeId, Align.右上);
            AutoCad.Db.Text.SetAlignmentPoint(attributeId, rightUp);

            return attributeId;
        }

    }
}