using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class Plate : CadObject
    {
        public Plate(int objId, int floor) : base(objId, floor)
        {
            var handleIds = XData.Plate.GetPlateSymbolHandleIds(objId);

            this.SymbolIds = new List<int>();
            foreach (var handleId in handleIds)
            {
                var blockId = AutoCad.Db.Utility.GetObjectId(handleId);
                this.SymbolIds.Add(blockId);
            }
        }

        public List<int> SymbolIds { get; set; }
        public List<Symbol> Symbols { get; set; }

        public void SetSymbols(List<Symbol> allSymbols)
        {
            this.Symbols = new List<Symbol>();
            foreach (var symbolId in this.SymbolIds)
            {
                var symbol = allSymbols.Find(p => p.ObjectId == symbolId);
                if(symbol == null)
                    continue;

                this.Symbols.Add(symbol);
            }
        }

        /// <summary>プレートに関連づけたシンボルが削除されていたらエラーを表示する</summary>
        public void Validate()
        {
            //シンボルIDからシンボルに辿りつけないものがある場合はカウントがずれる。
            //その場合は
            if(this.SymbolIds != null && this.Symbols != null && this.SymbolIds.Count != this.Symbols.Count)
                throw new ApplicationException(Messages.PlateRelationWasLost(this));
        }

        public static List<Plate> GetAll(int floor)
        {
            var plateIds = Filters.GetPlateIds();

            var plates = new List<Plate>();

            foreach (var plateId in plateIds)
            {
                plates.Add(new Plate(plateId, floor));
            }

            return plates;
        }
    }
}
