using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    /// <summary>
    /// Layoutはデータベースで使われているのでAcDbを付けました
    /// </summary>
    public class LayoutAcDb : PlotSettings
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbLayout; } }

        public int GetTabOrder(int objectId)
        {
            Result<int> result = this.Get<int>(objectId, "タブオーダー取得");

            if (!result.Success)
                throw new AutoCadException("Failed to get layout tab order.");

            return result.Value;
        }

        public string GetName(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "レイアウト名取得");

            if (!result.Success)
                throw new AutoCadException("Failed to get layout name.");

            return result.Value;
        }

        public bool IsTabSelected(int objectId)
        {
            Result<short> result = this.Get<short>(objectId, "タブ選択取得");

            if (!result.Success)
                throw new AutoCadException("Failed to get tab selected.");

            return result.Value == 1;
        }

        public int GetCurrent()
        {
            var layoutIds = AutoCad.Db.Dictionary.GetLayoutIds();
            foreach (var layoutId in layoutIds)
            {
                if (AutoCad.Db.Layout.IsTabSelected(layoutId))
                    return layoutId;
            }

            return 0;
        }
    }
}