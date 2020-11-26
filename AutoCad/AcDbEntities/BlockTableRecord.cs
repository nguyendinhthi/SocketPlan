using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class BlockTableRecord : SymbolTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbBlockTableRecord; } }

        public List<int> GetBlockReferenceIds(int objectId)
        {
            Result<int[]> result = this.Get<int[], short>(objectId, "ブロック参照ID全取得", (short)1);

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public string GetBlockName(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "名前取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get block name.");

            return result.Value;
        }

        public void SetBlockPath(int objectId, string path)
        {
            if (!this.Set<string>(objectId, "パス設定", path))
                throw new AutoCadException("Failed to set block path.");
        }

        /// <summary>図面上の図形が増えると、重すぎて使い物にならない</summary>
        public List<int> GetInsideObjects(int blockId)
        {
            Result<int[]> result = this.Get<int[]>(blockId, "全図形取得");

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public void SetColorInsideBlock(int blockId, CadColor color)
        {
            //入れ子ブロックはとりあえずケアしない

            var ids = this.GetInsideObjects(blockId);
            foreach (var id in ids)
            {
                AutoCad.Db.Entity.SetColor(id, color);
            }
        }
    }
}