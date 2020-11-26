using System.Collections.Generic;
using System;
namespace Edsa.AutoCadProxy
{
    public class LinetypeTable : SymbolTable
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLinetypeTable; } }

        public bool Exist(string linetypeName)
        {
            Result<short> result = this.DoTable<short, string>("所有？", linetypeName);
            if (!result.Success)
                throw new AutoCadException("Failed to get line type.");

            return result.Value == 1;
        }

        public int Add(string linetypeName)
        {
            Result<int> result = this.DoTable<int, string>("追加", linetypeName);
            if (!result.Success)
                throw new AutoCadException("Failed to add line type.");

            return result.Value;
        }

        public int Get(string linetypeName)
        {
            var result = this.DoTable<int, string>("名前から取得", linetypeName);
            if (!result.Success)
                throw new AutoCadException("Failed to get linetype.");

            return result.Value;
        }

        public List<string> GetAll()
        {
            var result = this.DoTable<string[]>("全レコード名取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get all linetype.");

            return new List<string>(result.Value);
        }

        public new int Make(string linetypeName)
        {
            if (this.Exist(linetypeName))
                return this.Get(linetypeName);

            return this.Add(linetypeName);
        }

        public int Make(string linetypeName, List<double> dash, string comment)
        {
            var linetypeId = this.Make(linetypeName);

            AutoCad.Db.LinetypeTableRecord.SetComment(linetypeId, comment);

            double patternLength = 0;
            dash.ForEach(p => patternLength += Math.Abs(p));
            AutoCad.Db.LinetypeTableRecord.SetPatternLength(linetypeId, patternLength);

            AutoCad.Db.LinetypeTableRecord.SetDashCount(linetypeId, dash.Count);

            for (int i = 0; i < dash.Count; i++)
            {
                AutoCad.Db.LinetypeTableRecord.SetDashLengthAt(linetypeId, i, dash[i]);
            }

            return linetypeId;
        }
    }
}