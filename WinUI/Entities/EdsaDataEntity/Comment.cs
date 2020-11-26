using System;
using System.Collections.Generic;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class Comment
    {
        public bool SingleForPlate
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_SingleForPlate);
            }
        }

        public bool CannotKatteniSwitch
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ不可);
            }
        }

        public bool ForKatteniSwitch
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ);
            }
        }

        public bool ForKatteniSwitchWallParent
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_壁_親);
            }
        }

        public bool ForKatteniSwitchWallChild
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_壁_子);
            }
        }

        public bool ForKatteniSwitchCeilingParent
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_天井_親);
            }
        }

        public bool ForKatteniSwitchCeilingChild
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_天井_子);
            }
        }

        public bool ForKatteniSwitchControlUnit
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_操作ユニット);
            }
        }

        public bool ForKatteniSwitchToilet
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_かってにスイッチ_トイレ);
            }
        }

        public bool ForAketaraSwitch
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ); }
        }
        public bool ForAketaraSwitch1st3Way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_1st3Way); }
        }
        public bool ForAketaraSwitch2nd3Way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_2nd3Way); }
        }
        public bool ForAketaraSwitchCase1
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_Case1); }
        }
        public bool ForAketaraSwitchCase2
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_Case2); }
        }
        public bool ForAketaraSubSwitch
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_Sub); }
        }
        public bool ForAketaraSwitchNormal
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_あけたらスイッチ_ノーマル); }
        }

        public bool ForTottaraRimokon
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_とったらリモコン); }
        }
        public bool ForTottaraRimokon1st3Way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_とったらリモコン_1st3Way); }
        }
        public bool ForTottaraRimokon2nd3Way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_とったらリモコン_2nd3Way); }
        }
        public bool ForTottaraRimokonCase1
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_とったらリモコン_Case1); }
        }
        public bool ForTottaraRimokonNormal
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_とったらリモコン_ノーマル); }
        }

        public bool ForLEDLightCon
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン); }
        }
        public bool ForLEDLightCon1st3way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_1st3way); }
        }
        public bool ForLEDLightCon2nd3Way
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_2nd3Way); }
        }
        public bool ForLEDLightConCase1
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_Case1); }
        }
        public bool ForLEDLightConCase2
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_Case2); }
        }
        public bool ForLEDLightConCase3
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_Case3); }
        }
        public bool ForLEDLightConNormal
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_LEDライコン_ノーマル); }
        }

        public bool ForOtherSwitch
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_OtherSwitch); }
        }
        public bool ForOtherSwitchCase1
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_OtherSwitchCase1); }
        }
        public bool ForOtherSwitchCase2
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_OtherSwitchCase2); }
        }
        public bool ForOtherSwitchCase3
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_OtherSwitchCase3); }
        }
        public bool ForOtherSwitchCase4
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_OtherSwitchCase4); }
        }

        public bool ForSignalWireConnectable
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_SignalWireConnectable); }
        }
        public bool ForSubSwitch
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_サブスイッチ); }
        }
        public bool ForWireInWall
        {
            get { return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_壁内配線); }
        }

        public static bool IsSignalWireConnectable(Symbol symbol)
        {
            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForSignalWireConnectable);
        }
        public static bool IsSubSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForSubSwitch);
        }
        public static bool IsAketaraSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitch);
        }
        public static bool IsAketaraSwitch1st3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitch1st3Way);
        }
        public static bool IsAketaraSwitch2nd3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitch2nd3Way);
        }
        public static bool IsAketaraSwitchCase1(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitchCase1);
        }
        public static bool IsAketaraSwitchCase2(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitchCase2);
        }
        public static bool IsAketaraSwitchNormal(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSwitchNormal);
        }
        public static bool IsAketaraSubSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForAketaraSubSwitch);
        }
        public static bool IsTottaraRimokon(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForTottaraRimokon);
        }
        public static bool IsTottaraRimokon1st3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForTottaraRimokon1st3Way);
        }
        public static bool IsTottaraRimokon2nd3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForTottaraRimokon2nd3Way);
        }
        public static bool IsTottaraRimokonCase1(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForTottaraRimokonCase1);
        }
        public static bool IsTottaraRimokonNormal(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForTottaraRimokonNormal);
        }
        public static bool IsLEDLightCon(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightCon);
        }
        public static bool IsLEDLightCon1st3way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightCon1st3way);
        }
        public static bool IsLEDLightCon2nd3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightCon2nd3Way);
        }
        public static bool IsLEDLightConCase1(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightConCase1);
        }
        public static bool IsLEDLightConCase2(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightConCase2);
        }
        public static bool IsLEDLightConCase3(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightConCase3);
        }
        public static bool IsLEDLightConNormal(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForLEDLightConNormal);
        }

        public static bool IsOtherSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForOtherSwitch);
        }
        public static bool IsOtherSwitchCase1(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForOtherSwitchCase1);
        }
        public static bool IsOtherSwitchCase2(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForOtherSwitchCase2);
        }
        public static bool IsOtherSwitchCase3(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForOtherSwitchCase3);
        }
        public static bool IsOtherSwitchCase4(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForOtherSwitchCase4);
        }

        public static bool HasWireInWall(Symbol symbol)
        {
            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForWireInWall);
        }

        public bool ForSmartSeries
        {
            get
            {
                return Array.Exists(this.Specifications, p => p.Id == Const.Specification.コメント_スマートシリーズ);
            }
        }

        public static bool IsAutoSmartSeries(Symbol symbol)
        {
            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForSmartSeries);
        }

        public static bool IsKatteniSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitch);
        }

        public static bool IsKatteniSwitchWallParent(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchWallParent);
        }

        public static bool IsKatteniSwitchWallChild(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchWallChild);
        }

        public static bool IsKatteniSwitchCeilingParent(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchCeilingParent);
        }

        public static bool IsKatteniSwitchCeilingChild(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchCeilingChild);
        }

        public static bool IsKatteniSwitchControlUnit(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchControlUnit);
        }

        public static bool IsKatteniSwitch3Way(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.Text == Const.KatteniSwitchSerial.WTK1274W);
        }

        public static bool IsKatteniSwitchToilet(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            var comments = Comment.GetSymbolCommentsWithoutKomemark(symbol);
            return comments.Exists(p => p.ForKatteniSwitchToilet);
        }

        public static bool IsKatteniSwitchParent(Symbol symbol)
        {
            return
                Comment.IsKatteniSwitchWallParent(symbol) ||
                Comment.IsKatteniSwitchCeilingParent(symbol);
        }
        
        public static bool IsKatteniSwitchChild(Symbol symbol)
        {
            return
                Comment.IsKatteniSwitchWallChild(symbol) ||
                Comment.IsKatteniSwitchCeilingChild(symbol);
        }

        public static string GetKatteniSwitchProductCode(Symbol symbol)
        {
            var comments = Comment.GetSymbolComments(symbol);
            var comment = comments.Find(p => IsKatteniSwitchProductCode(p));

            if (comment == null)
                return "";

            return comment.Text;
        }
        public static string GetAketaraSwitchSerial(Symbol symbol)
        {
            var comments = Comment.GetSymbolComments(symbol);
            var comment = comments.Find(p => p.ForAketaraSwitch);
            if (comment == null)
                return "";
            return comment.Text;
        }
        public static string GetTottaraRimokonSerial(Symbol symbol)
        {
            var comments = Comment.GetSymbolComments(symbol);
            var comment = comments.Find(p => p.ForTottaraRimokon);
            if (comment == null)
                return "";
            return comment.Text;
        }
        public static string GetLEDLightConSerial(Symbol symbol)
        {
            var comments = Comment.GetSymbolComments(symbol);
            var comment = comments.Find(p => p.ForLEDLightCon);
            if (comment == null)
                return "";
            return comment.Text;
        }
        public static string GetOtherSwitchSerial(Symbol symbol)
        {
            var comments = Comment.GetSymbolComments(symbol);
            var comment = comments.Find(p => p.ForOtherSwitch);
            if (comment == null)
                return "";
            return comment.Text;
        }

        public static bool IsKatteniSwitchProductCode(Comment comment)
        {
            return comment.ForKatteniSwitch;
        }

        private static List<Comment> GetSymbolCommentsWithoutKomemark(Symbol symbol)
        {
            var comments = new List<Comment>();

            foreach (var attribute in symbol.OtherAttributes)
            {
                //※マークは取り除いて返す
                var text = attribute.Value.Replace("※", "");
                var comment = UnitWiring.Masters.Comments.Find(p => p.Text == text);
                if (comment != null)
                    comments.Add(comment);
            }

            return comments;
        }

        /// <summary>
        /// オプション拾いでは※マーク付きのかってにスイッチを無視する。その時に使う
        /// </summary>
        private static List<Comment> GetSymbolComments(Symbol symbol)
        {
            var comments = new List<Comment>();

            foreach (var attribute in symbol.OtherAttributes)
            {
                var comment = UnitWiring.Masters.Comments.Find(p => p.Text == attribute.Value);
                if (comment != null)
                    comments.Add(comment);
            }

            return comments;
        }
    }
}
