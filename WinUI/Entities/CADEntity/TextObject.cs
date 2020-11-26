using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class TextObject : CadObject
    {
        public RoomObject Room { get; set; }
        public string RoomName { get; set; }

        public string Text { get; set; }

        public TextObject(int textId, int floor)
            : base(textId, floor)
        {
            this.Text = AutoCad.Db.Text.GetText(textId);

            var countedTexts = TextObject.GetCountedText(this.Text);
            this.TextWithoutQty = countedTexts[0];
            this.Qty = countedTexts.Count;
            this.Position = AutoCad.Db.Text.GetPosition(textId);
        }

        public string CeilingPanelName { get; set; }

        public string TextWithoutQty { get; set; }
        public int Qty { get; set; }
        public PointD Position { get; set; }

        public bool Is蓄電ﾕﾆｯﾄ
        {
            get
            {
                //TODO FloorTextsマスタで設定できるようにしたいな。
                return this.Text == Const.Text.蓄電ﾕﾆｯﾄ;
            }
        }

        public bool IsEVPS
        {
            get
            {
                //TODO FloorTextsマスタで設定できるようにしたいな。
                return this.Text == Const.Text.EVPS;
            }
        }

        public bool IsPowerConditioner
        {
            get
            {
                //TODO FloorTextsマスタで設定できるようにしたいな。
                return Utilities.In(this.Text, "①S", "②S", "③S", "④S", "⑤S", "①L", "②L", "③L", "④L", "⑤L");
            }
        }

        public bool IsBayWindow
        {
            get
            {
                var demadoText = this.GetProofreadedText(this.Text);

                if (demadoText.Contains(Const.BayWindow.ﾊｰﾓﾆｰﾌﾟﾚｼｬｽﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾊｰﾓﾆﾍﾞｲｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾍﾞｲｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾌﾟﾚｼｬｽｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾌｧﾆﾁｬｰｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.ﾊｰﾓﾆｰPFｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindow.百年出窓))
                    return true;
                else
                    return false;
            }
        }

        public bool IsBayWindowNoLight
        {
            get
            {
                var demadoText = this.GetProofreadedText(this.Text);

                if (demadoText.Contains(Const.BayWindowNoLight.ｽｸｴｱｳｲﾝﾄﾞｳ) ||
                    demadoText.Contains(Const.BayWindowNoLight.ﾊｰﾓﾆｰﾍﾞｲｳｲﾝﾄﾞｳ_90A) ||
                    demadoText.Contains(Const.BayWindowNoLight.ﾊｰﾓﾆｰﾘｽﾞﾑｳｲﾝﾄﾞｳ_60U) ||
                    demadoText.Contains(Const.BayWindowNoLight.ﾊｰﾓﾆｰﾛｲﾔﾙｳｲﾝﾄﾞｳ_90A) ||
                    demadoText.Contains(Const.BayWindowNoLight.ﾘｽﾞﾑｳｲﾝﾄﾞｳ_30U) ||
                    demadoText.Contains(Const.BayWindowNoLight.百年J6060) ||
                    demadoText.Contains(Const.BayWindowNoLight.地袋付和室出窓_90U))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>ほげx3のようなテキストを{ほげ,ほげ,ほげ}のリストにして返す</summary>
        public static List<string> GetCountedText(string text)
        {
            var segments = text.Split('x');

            //xが含まれなかったらそのまま返す
            if (segments.Length <= 1)
                return new List<string> { text };

            //xの後ろが数値じゃなかったらそのまま返す
            int qty;
            if (!int.TryParse(segments[1].Trim(), out qty))
                return new List<string> { text };

            var realText = segments[0].Trim();

            var countedTexts = new List<string>();
            for (int i = 0; i < qty; i++)
            {
                countedTexts.Add(realText);
            }

            return countedTexts;
        }

        public static List<TextObject> GetAll(int floor)
        {
            var texts = new List<TextObject>();

            var textIds = AutoCad.Db.Text.GetAll();
            foreach (var textId in textIds)
            {
                texts.Add(new TextObject(textId, floor));
            }

            return texts;
        }

        public void FillRoom(List<RoomObject> rooms)
        {
            this.Room = null;
            this.RoomName = Const.Room.外部;

            foreach (var room in rooms)
            {
                if (this.Floor != room.Floor)
                    continue;

                if (!this.Position.IsIn(room.ObjectId))
                    continue;

                this.Room = room;
                this.RoomName = room.Name;
                return;
            }
        }

        //全角、ハイフンとかを読み替えて半角で戻す
        private string GetProofreadedText(string text)
        {
            text = text.Replace("－", "ー");
            text = text.Replace("-", "ー");
            text = Microsoft.VisualBasic.Strings.StrConv(text, Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            text = text.Replace(" ", "");
            return text;
        }
    }
}
