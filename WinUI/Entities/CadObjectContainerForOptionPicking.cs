using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class CadObjectContainerForOptionPicking : CadObjectContainer
    {
        public CadObjectContainerForOptionPicking(List<Drawing> drawings, CadObjectTypes objectTypes) : base(drawings, objectTypes)
        {
            this.RemoveIgnoreTexts();
        }

        private void RemoveIgnoreTexts() 
        {
            List<TextObject> texts = new List<TextObject>();
            texts.AddRange(this.Texts);
            foreach (var text in texts)
            {
                if (text.Text.Contains(Const.Text.基礎先行配管))
                    if (!AutoCad.Db.Entity.GetLayerName(text.ObjectId).Contains("電気_"))
                        this.Texts.Remove(text);
            }
        }

        /// <summary>
        /// 平面図と立面図でコメントを多重集計して困るので、
        /// オプション拾いでは立面図の基礎先行配管コメントを無視する
        /// </summary>
        protected override List<string> GetSimpleTexts()
        {
            var texts = new List<string>();

            foreach (var text in this.Texts)
            {
                if (text.Floor == 0 && text.TextWithoutQty == Const.Text.基礎先行配管)
                    continue;

                texts.Add(text.Text);
            }

            return texts;
        }

        /// <summary>
        /// 平面図と立面図でコメントを多重集計して困るので、
        /// オプション拾いでは立面図の基礎先行配管コメントを無視する
        /// </summary>
        protected override List<string> GetSymbolTexts()
        {
            var texts = new List<string>();

            foreach (var symbol in this.Symbols)
            {
                foreach (var att in symbol.OtherAttributes)
                {
                    if (symbol.Floor == 0 && att.Value.Contains(Const.Text.基礎先行配管))
                        continue;

                    if (AutoCad.Db.Attribute.GetVisible(att.ObjectId))
                        texts.Add(att.Value);
                }
            }

            return texts;
        }

        public override List<Symbol> FindChainedGroupedSymbols(Symbol currentSymbol, ref List<Symbol> symbols, ref List<Wire> wires)
        {
            var groupSymbols = new List<Symbol>();
            var connectedWires = new List<Wire>();
            foreach (var wire in wires)
            {
                if (wire is RisingWire)
                {
                    var rw = wire as RisingWire;
                    if (!rw.IsConnectedConsideringClipped(currentSymbol))
                        continue;

                    var connectedSymbol = symbols.Find(p => rw.IsConnectedConsideringClipped(p));
                    if (connectedSymbol == null)
                        continue;

                    groupSymbols.Add(connectedSymbol);
                    connectedWires.Add(wire);
                }
                else
                {
                    if (!wire.IsConnected(currentSymbol))
                        continue;

                    var connectedSymbol = symbols.Find(p => wire.IsConnected(p));
                    if (connectedSymbol == null)
                        continue;

                    groupSymbols.Add(connectedSymbol);
                    connectedWires.Add(wire);
                }
            }

            //判定が終わったシンボルと配線を排除する
            foreach (var groupSymbol in groupSymbols)
            {
                symbols.Remove(groupSymbol);
            }

            foreach (var connectedWire in connectedWires)
            {
                wires.Remove(connectedWire);
            }

            var chainedSymbols = new List<Symbol>();
            foreach (var groupSymbol in groupSymbols)
            {
                //再帰呼び出し
                chainedSymbols.AddRange(this.FindChainedGroupedSymbols(groupSymbol, ref symbols, ref wires));
            }

            groupSymbols.AddRange(chainedSymbols);

            return groupSymbols;
        }

        public List<TextObject> FindAllTextForEx()
        {
            var texts = new List<TextObject>();
            texts.AddRange(this.GetSimpleTextsForEx());
            texts.AddRange(this.GetSymbolTextsForEx());

            var splitTexts = this.GetSplitTextsForEx(texts);
            var appearTexts = this.FindTextsForCsvForEx(splitTexts);

            appearTexts.ForEach(p => p.FillRoom(this.RoomOutlines));

            return appearTexts;
        }

        private List<TextObject> GetSimpleTextsForEx()
        {
            var texts = new List<TextObject>();
            foreach (var text in this.Texts)
            {
                if (text.Floor == 0 && text.TextWithoutQty == Const.Text.基礎先行配管)
                    continue;

                texts.Add(text);
            }

            return texts;
        }

        private List<TextObject> GetSymbolTextsForEx()
        {
            var texts = new List<TextObject>();
            this.Symbols.ForEach(symbol =>
            {
                foreach (var att in symbol.OtherAttributes)
                {
                    if (AutoCad.Db.Attribute.GetVisible(att.ObjectId))
                    {
                        if (symbol.Floor == 0 && att.Value.Contains(Const.Text.基礎先行配管))
                            continue;

                        var text = new TextObject(att.ObjectId, symbol.Floor);
                        texts.Add(text); //シンボルコメント一覧
                    }
                }
            });

            return texts;
        }

        private List<TextObject> GetSplitTextsForEx(List<TextObject> texts)
        {
            var splitTexts = new List<TextObject>();
            texts.ForEach(text =>
            {
                var s = TextObject.GetCountedText(text.Text);

                for (var i = 0; i < s.Count; i++)
                {
                    var split = new TextObject(text.ObjectId, text.Floor);
                    split.Text = s[i];
                    splitTexts.Add(split);
                }
            });
            return splitTexts;
        }

        private List<TextObject> FindTextsForCsvForEx(List<TextObject> texts)
        {
            var appearTexts = new List<TextObject>();
            texts.ForEach(text =>
            {
                if (UnitWiring.Masters.Comments.Exists(com => com.Text == text.Text && com.AppearInCsv))
                    appearTexts.Add(text);
            });
            return appearTexts;
        }
    }
}