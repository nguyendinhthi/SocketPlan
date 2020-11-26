using System;
using System.Collections.Generic;
using System.Text;

namespace Edsa.AutoCadProxy
{
    public class LinetypeTableRecord : SymbolTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLinetypeTableRecord; } }

        public void SetComment(int linetypeId, string comment)
        {
            if (!this.Set(linetypeId, "コメント設定", comment))
                throw new AutoCadException("Failed to set comment to linetype.");
        }

        public void SetPatternLength(int linetypeId, double patternLength)
        {
            if (!this.Set(linetypeId, "パターンの長さ設定", patternLength))
                throw new AutoCadException("Failed to set pattern length to linetype.");
        }

        public void SetDashCount(int linetypeId, int count)
        {
            if (!this.Set(linetypeId, "ダッシュ数設定", count))
                throw new AutoCadException("Failed to set dash count to linetype.");
        }

        public void SetDashLengthAt(int linetypeId, int index, double dashLength)
        {
            var args = new List<object>();
            args.Add(index);
            args.Add(dashLength);

            if (!this.Set(linetypeId, "ダッシュの長さ順番指定設定", args.ToArray()))
                throw new AutoCadException("Failed to set dash length to linetype.");
        }
    }
}
