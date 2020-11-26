using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class LayoutText
    {
        public string Value { get; set; }

        private static List<LayoutText> Get(int layoutId)
        {
            return LayoutText.GetAll().FindAll(p => p.LayoutId == layoutId);
        }

        public static List<LayoutText> Get(string constructionCode, int layoutId)
        {
            return LayoutText.Get(constructionCode, 0, layoutId);
        }

        public static List<LayoutText> Get(string constructionCode, int siyoCode, int layoutId)
        {
            var layoutTexts = LayoutText.Get(layoutId);

            foreach (var layoutText in layoutTexts)
            {
                var query = Query.Get(layoutText.QueryId);
                var sql = string.Format(query.SqlStatement, constructionCode, siyoCode);

#if DEBUG
                //KSKのデバッグ環境では、HRDSQL,HRDSQL2へリンクを張っていない為、一部クエリが通らない。ここでごまかす。
                sql = sql.Replace("HRDSQL.", "");
                sql = sql.Replace("HRDSQL2.", "");
#endif

                var db = Construction.GetDatabase(); //HRDSQL4のConnectionStringにするため、Constructionを使っている。手抜き

                var result = db.ExecuteScalar(sql);

                if (result != null)
                    layoutText.Value = result.ToString();
                else
                    layoutText.Value = "★未設定";
            }

            return layoutTexts;
        }

        public static List<LayoutText> GetBySiyoCode(string constructionCode, int siyoCode)
        {
            var layoutTexts = LayoutText.GetAll();

            foreach (var layoutText in layoutTexts)
            {
                var query = Query.Get(layoutText.QueryId);
                var sql = string.Format(query.SqlStatement, constructionCode, siyoCode);

#if DEBUG
                //KSKのデバッグ環境では、HRDSQL,HRDSQL2へリンクを張っていない為、一部クエリが通らない。ここでごまかす。
                sql = sql.Replace("HRDSQL.", "");
                sql = sql.Replace("HRDSQL2.", "");
#endif

                var db = Construction.GetDatabase(); //HRDSQL4のConnectionStringにするため、Constructionを使っている。手抜き

                var result = db.ExecuteScalar(sql);
                if (result != null)
                    layoutText.Value = result.ToString();
                else
                    layoutText.Value = "";
            }

            return layoutTexts;
        }
    }
}
