using System;
using System.Collections.Generic;
using System.Text;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class ProductVa
    {
        /// <summary>
        /// 仕様書の商品の中には、専用コンセントではない一般コンセントの商品がある。そいつらを識別するメソッド
        /// </summary>
        public bool IsOrdinary 
        {
            get
            {
                if (this.Class1Code == "0070" && this.Class2Code == "0310")
                    return true; //洗面化粧台

                if (this.Class1Code == "0100" && this.Class2Code == "0320")
                    return true; //セカンド洗面化粧台

                if (this.Class1Code == "0200" && this.Class2Code == "0600" && this.Va < 1600)
                    return true; //1Fトイレ

                if (this.Class1Code == "0210" && this.Class2Code == "0600" && this.Va < 1600)
                    return true; //1Fトイレ
                
                if (this.Class1Code == "0215" && this.Class2Code == "0600" && this.Va < 1600)
                    return true; //1Fトイレ

                //便器部分ですが、1600VAと記載されているものが専用で問題ありません。 from Mr.Tasaki 2013/4/23

                //商品個別に対応してたらあれがないこれがない言われるので、600VA以下は全部一般回路に混ぜてしまえ。
                //TODO しばらく何も言われなかったら上の処理は削除しよう。 2013/05/31 Kitamura
                if (this.Va <= 600)
                    return true; 

                return false;
            }
        }
    }
}
