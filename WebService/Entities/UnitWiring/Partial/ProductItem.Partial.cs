using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ProductItem
    {
        public static List<ProductItem> Get(string constructionCode, string planNo)
        {
            var productItems = ProductItem.GetAll();

            var result = new List<ProductItem>();

            if (ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
                var shohins = tbl_siyo_syohin.Get(constructionCode, siyoCode);
                foreach (var shohin in shohins)
                {
                    var items = productItems.FindAll(p =>
                            shohin.komokuCd1 == p.Class1Code &&
                            shohin.komokuCd2 == p.Class2Code &&
                            shohin.shohinCd == p.ProductCode);

                    result.AddRange(items);
                }
            }
            else
            {
                var shohins = SpecificationProductDetail.Get(constructionCode);

                foreach (var shohin in shohins)
                {
                    var items = productItems.FindAll(p =>
                            shohin.Class1Code == p.Class1Code &&
                            shohin.Class2Code == p.Class2Code &&
                            shohin.ProductCode == p.ProductCode);

                    result.AddRange(items);
                }
            }

            result.Sort((p, q) =>
            {
                if (p.Class1Code != q.Class1Code)
                    return p.Class1Code.CompareTo(q.Class1Code);

                if (p.Class2Code != q.Class2Code)
                    return p.Class2Code.CompareTo(q.Class2Code);

                return p.ProductCode.CompareTo(q.ProductCode);
            });

            return result;
        }

        public static void DeleteAll()
        {
            var db = ProductItem.GetDatabase();
            db.ExecuteNonQuery("DELETE FROM ProductItems");
        }
    }
}
