namespace Edsa.AutoCadProxy
{
    public class LayerTableRecord : SymbolTableRecord
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLayerTableRecord; } }

        public string GetLayerName(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "名前取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get layer name.");

            return result.Value;
        }

        public void SetLayerName(int objectId, string name)
        {
            if (!this.Set<string>(objectId, "名前設定", name))
                throw new AutoCadException("Failed to set layer name.");
        }

        public bool IsFrozen(int objectId)
        {
            var result = this.Get<short>(objectId, "フリーズ？");
            if (!result.Success)
                throw new AutoCadException("Failed to get layer frozen.");

            return result.Value == (short)1;
        }

        public void SetVisible(int objectId, bool visible)
        {
            if (!this.Set<short>(objectId, "オフ設定", visible ? (short)0 : (short)1))
                throw new AutoCadException("Failed to set layer visible.");
        }

        /// <summary>trueで見えなくする</summary>
        public void SetFrozen(int objectId, bool isFrozen)
        {
            if (!this.Set<short>(objectId, "フリーズ設定", isFrozen ? (short)1 : (short)0))
                throw new AutoCadException("Failed to set layer frozen.");
        }

        public void SetColor(int objectId, CadColor color)
        {
            if (!this.Set<int>(objectId, "色設定", (short)color.Code))
                throw new AutoCadException("Failed to set layer color.");
        }

        public void SetWeight(int objectId, int weight)
        {
            if (!this.Set<short>(objectId, "線の太さ設定", (short)weight))
                throw new AutoCadException("Failed to set layer weight.");
        }

        public int Make(string layerName, CadColor color, int lineWeight)
        {
            int layerId;

            if (AutoCad.Db.LayerTable.Exist(layerName))
                layerId = AutoCad.Db.LayerTable.GetLayerId(layerName);
            else
                layerId = AutoCad.Db.LayerTable.Add(layerName);

            this.SetColor(layerId, color);
            this.SetWeight(layerId, lineWeight);

            return layerId;
        }

        /// <summary>trueでロックする</summary>
        public void SetLock(int objectId, bool locked)
        {
            if (!this.Set<short>(objectId, "ロック設定", locked ? (short)1 : (short)0))
                throw new AutoCadException("Failed to lock(unlock) layer.");
        }
        
        public bool IsLocked(int layerId)
        {
            var result = this.Get<short>(layerId, "ロック？");
            if (!result.Success)
                throw new AutoCadException("<LT VB-COM> Failed to get layer is locked.");

            return result.Value == (short)1;
        }
    }
}