using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ProductVa
    {
        public int Floor;
        public string RoomCode;
        public string RoomName;

        public static List<ProductVa> Get(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            var heyas = SiyoHeya.Get(constructionCode, siyoCode);
            var productVas = ProductVa.GetAll();
            var result = new List<ProductVa>();

            if(ConstructionSchedule.IsBeforeProcessRequest(constructionCode))
            {
                var shohins = tbl_siyo_syohin.Get(constructionCode, siyoCode);

                foreach (var shohin in shohins)
                {
                    var products = productVas.FindAll(p =>
                            shohin.komokuCd1 == p.Class1Code &&
                            shohin.komokuCd2 == p.Class2Code &&
                            shohin.shohinCd == p.ProductCode);

                    foreach (var product in products)
                    {
                        var clone = product.Clone();

                        if (shohin.floorNum.HasValue)
                            clone.Floor = shohin.floorNum.Value;

                        if (clone.Floor == 0 || clone.Floor == 9)
                            clone.Floor = 1;

                        clone.RoomCode = shohin.roomCd;

                        var heya = heyas.Find(p => p.RoomCode == clone.RoomCode);
                        if (heya != null)
                            clone.RoomName = heya.RoomName;
                        else
                            clone.RoomName = string.Empty;

                        result.Add(clone);
                    }
                }
            }
            else
            {
                var shohins = SpecificationProductDetail.Get(constructionCode);

                foreach (var shohin in shohins)
                {
                    var products = productVas.FindAll(p =>
                            shohin.Class1Code == p.Class1Code &&
                            shohin.Class2Code == p.Class2Code &&
                            shohin.ProductCode == p.ProductCode);

                    foreach (var product in products)
                    {
                        var clone = product.Clone();

                        clone.Floor = shohin.Floor;

                        if (clone.Floor == 0 || clone.Floor == 9)
                            clone.Floor = 1;

                        clone.RoomCode = shohin.RoomCode;

                        var heya = heyas.Find(p => p.RoomCode == clone.RoomCode);
                        if (heya != null)
                            clone.RoomName = heya.RoomName;
                        else
                            clone.RoomName = string.Empty;

                        result.Add(clone);
                    }
                }
            }

            result.Sort((p, q) => 
                {
                    if(p.Class1Code != q.Class1Code)
                        return p.Class1Code.CompareTo(q.Class1Code);

                    if (p.Class2Code != q.Class2Code)
                        return p.Class2Code.CompareTo(q.Class2Code);

                    return p.ProductCode.CompareTo(q.ProductCode);
                });

            return result;
        }

        public static void Delete()
        {
            string sql = string.Empty;
            sql = "Delete From ProductVas";

            ProductVa.GetDatabase().ExecuteNonQuery(sql);
        }
    }
}
