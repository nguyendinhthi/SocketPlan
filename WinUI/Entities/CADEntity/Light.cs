using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class Light
    {
        #region コンストラクタ

        private Light()
        {
        }

        public Light(Symbol symbol)
            : this()
        {
            this.Symbol = symbol;

            var att = symbol.Attributes.Find(p => p.Tag.ToUpper() == Const.AttributeTag.LIGHT_NO.ToUpper());
            if (att != null && 0 < att.Value.Length)
            {
                var roomCodeChar = att.Value.ToCharArray()[0];
                var seqNoString = att.Value.Substring(1);

                int seqNo;
                if (!int.TryParse(seqNoString, out seqNo))
                    return;

                this.RoomCode = roomCodeChar;
                this.SeqNo = seqNo;
            }
        }

        #endregion

        #region プロパティ

        public string LightNo
        {
            get
            {
                if (!Char.IsLetter(this.RoomCode) || this.SeqNo == 0)
                    return string.Empty;

                return this.RoomCode.ToString() + this.SeqNo.ToString();
            }
        }

        public char RoomCode { get; set; }
        public int SeqNo { get; set; }
        public string RoomName { get { return this.Symbol.RoomName; } }
        public int Floor { get { return this.Symbol.Floor; } }
        public string Hinban
        {
            get
            {
                var att = this.Symbol.Attributes.Find(p => p.Tag == Const.AttributeTag.HINBAN);
                if (att == null)
                    return string.Empty;

                return att.Value;
            }
        }
        public bool IsAllocated { get { return this.SeqNo != 0; } }
        public bool IsLight { get { return this.Symbol.IsLight; } }
        public PointD Position { get { return this.Symbol.Position; } }
        public string SymbolImageCode { get { return this.Symbol.Equipment.照明シンボルイメージコード; } }

        public Symbol Symbol { get; set; }

        public bool IsSetItem { get { return this.Symbol.Equipment.CustomActionセット品; } }
        public bool IsSetItemMain { get { return this.IsSetItem && !string.IsNullOrEmpty(this.Hinban); } }
        public bool IsSetItemSub { get { return this.IsSetItem && string.IsNullOrEmpty(this.Hinban); } }

        #endregion

        #region メソッド

        public List<string> GetSerialsForView()
        {
            var segments = this.Hinban.Split(';');
            return new List<string>(segments);
        }

        /// <summary>品番の中に「AAAA x 4」のように数量が含まれていたら、分解して「AAAA」4つを返す</summary>
        public List<string> GetSerialsForCount()
        {
            var serials = this.GetSerialsForView();
            var newList = new List<string>();

            foreach (var serial in serials)
            {
                newList.AddRange(TextObject.GetCountedText(serial));
            }

            return newList;
        }

        /// <summary>
        /// 重複品番名は取り除いて、付いてる品番名を返す
        /// </summary>
        /// <returns></returns>
        public List<string> GetSerialsForDistinct()
        {

            var serials = this.GetSerialsForView();
            var newList = new List<string>();

            foreach (var serial in serials)
            {
                List<string> countedTexts = TextObject.GetCountedText(serial);
                if (newList.IndexOf(countedTexts[0]) == -1)
                    newList.Add(countedTexts[0]);
            }

            return newList;
        }

        public void SetLightNo(char roomCode, int seqNo)
        {
            this.RoomCode = roomCode;
            this.SeqNo = seqNo;
        }

        public void ClearLightNo()
        {
            this.SetLightNo(default(char), default(int));
        }

        public void DrawLightNo()
        {
            var lightNo = this.Symbol.Attributes.Find(p => p.Tag == Const.AttributeTag.LIGHT_NO);
            if (lightNo == null)
            {
                var attId = AutoCad.Db.Attribute.Make(this.Symbol.ObjectId, Const.AttributeTag.LIGHT_NO, this.LightNo, CadColor.BlackWhite, Const.TEXT_HEIGHT);
                AutoCad.Db.Attribute.SetVisible(attId, true);
                AutoCad.Db.Attribute.SetRotation(attId, 0);
            }
            else
            {
                AutoCad.Db.Attribute.SetText(lightNo.ObjectId, this.LightNo);
                AutoCad.Db.Attribute.SetVisible(lightNo.ObjectId, true);
                AutoCad.Db.Attribute.SetRotation(lightNo.ObjectId, 0);
            }

            //TODO 旧バージョンのタグ名がなくなるまでは、下のコードで対処しておこう。１か月くらいしたら消そう。
            var gomiNo = this.Symbol.Attributes.Find(p => p.Tag == "LightNo");
            if (gomiNo != null)
                AutoCad.Db.Object.Erase(gomiNo.ObjectId);
        }

        public bool HasLightNo()
        {
            return !string.IsNullOrEmpty(this.LightNo);
        }

        public bool HasRegisteredHinban()
        {
            var hinbans = this.GetSerialsForView();

            foreach (var hinban in hinbans)
            {
                if (!this.IsRegisteredHinban(hinban))
                    return false;
            }

            return true;
        }

        private bool IsRegisteredHinban(string hinban)
        {
            foreach (var category in UnitWiring.Masters.LightSerialCategories)
            {
                foreach (var serial in category.LightSerials)
                {
                    if (serial.Name != hinban)
                        continue;

                    if (serial.IsDeleted)
                        continue;

                    return true;
                }
            }

            return false;
        }

        public bool IsInRoomOfSiyo(List<SiyoHeya> siyoHeyas)
        {
            var roomCode = RoomObject.GetRoomCodeInSiyo(this.Symbol.Room, this.RoomName, siyoHeyas);
            return !string.IsNullOrEmpty(roomCode);
        }

        /// <summary>廃盤チェックをすべき現場だったらtrueを返す</summary>
        public static bool ShouldCheckHaiban(string constructionCode)
        {
            using (var service = new SocketPlanService())
            {
                //加工依頼済みで長期物件以外だったら廃盤チェックしない
                var isBefore = service.IsBeforeProcessRequest(constructionCode);
                return isBefore;
            }
        }

        #endregion
    }
}
