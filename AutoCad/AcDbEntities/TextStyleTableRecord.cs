using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class TextStyleTableRecord : SymbolTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbTextStyleTableRecord; } }

        public void SetFont(int objectId, string fontName)
        {
            List<object> font = new List<object>();
            font.Add(fontName);
            font.Add((short)0); //太字
            font.Add((short)0); //斜体
            font.Add((short)128); //Windowsキャラクターセット
            font.Add((short)17); //ピッチ

            if (!this.Set<object[]>(objectId, "フォント設定", font.ToArray()))
                throw new AutoCadException("Failed to set font of text style.");
        }

        public int Make(string styleName, string fontName)
        {
            int styleId;

            if (AutoCad.Db.TextStyleTable.Exist(styleName))
                styleId = AutoCad.Db.TextStyleTable.GetId(styleName);
            else
                styleId = AutoCad.Db.TextStyleTable.Add(styleName);

            this.SetFont(styleId, fontName);

            return styleId;
        }
    }
}
