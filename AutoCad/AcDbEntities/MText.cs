using System;
namespace Edsa.AutoCadProxy
{
    public class MText : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbMText; } }

        public enum Attachment
        {
            左上 = 1,
            上中心,
            右上,
            左中央,
            中央,
            右中央,
            左下,
            下中心,
            右下
        }

        private int MakeMText()
        {
            Result<int> result = this.Make();
            if (!result.Success)
                throw new AutoCadException("Failed to make multi text.");

            return result.Value;
        }

        public int Make(string text, string textStyle, double height, PointD position)
        {
            var id = this.MakeMText();
            this.SetText(id, text);
            this.SetPosition(id, position);
            this.SetHeight(id, height);

            AutoCad.Db.TextStyleTableRecord.Make(textStyle, textStyle);
            this.SetTextStyle(id, textStyle);

            return id;
        }

        public int Make(string text, string textStyle, double height, PointD position, Attachment attachment)
        {
            var textId = this.Make(text, textStyle, height, position);
            AutoCad.Db.MText.SetAttachment(textId, attachment);

            return textId;
        }

        public int Make(string text, double height, PointD position, Attachment attachment)
        {
            var textId = this.Make(text, Font.MSGothic, height, position, attachment);
            return textId;
        }

        public int MakeV(string text, double height, PointD position, Attachment attachment)
        {
            var textId = this.Make(text, Font.MSGothicV, height, position, attachment);
            this.SetRotation(textId, Math.PI * 3 / 2);

            return textId;
        }

        public void SetText(int objectId, string text)
        {
            if (!this.Set<string>(objectId, "内容設定", text))
                throw new AutoCadException("Failed to set text of multi text.");
        }

        public void SetPosition(int objectId, PointD position)
        {
            if (!this.Set<double[]>(objectId, "位置指定", new double[] { position.X, position.Y, 0 }))
                throw new AutoCadException("Failed to set position of multi text.");
        }

        public void SetRotation(int objectId, double rotation)
        {
            if (!this.Set<double>(objectId, "回転角度設定", rotation))
                throw new AutoCadException("Failed to set rotation of multi text.");
        }

        public void SetHeight(int objectId, double height)
        {
            if (!this.Set<double>(objectId, "文字高さ設定", height))
                throw new AutoCadException("Failed to set height of multi text.");
        }

        public void SetTextStyle(int objectId, string style)
        {
            if (!this.Set<string>(objectId, "文字スタイル設定", style))
                throw new AutoCadException("Failed to set text style of multi text.");
        }

        public void SetAttachment(int objectId, Attachment attachment)
        {
            if (!this.Set<short>(objectId, "位置合わせ設定", (short)attachment))
                throw new AutoCadException("Failed to set text attachment of multi text.");
        }

        public string GetText(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "内容取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get text of multi text.");

            return result.Value;
        }
    }
}