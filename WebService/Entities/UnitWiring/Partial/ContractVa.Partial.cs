using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketPlan.WebService
{
    public partial class ContractVa
    {
        public static string GetContractVa(decimal soVa)
        {
            var list = ContractVa.GetAll();
            foreach (var entity in list)
            {
                if (soVa == 0 && soVa == entity.TotalVaLower) //0もありうる
                    return entity.ContractBaseVa;

                if (entity.TotalVaLower < soVa && soVa <= entity.TotalVaUpper)
                    return entity.ContractBaseVa;
            }

            return null;
        }
    }
}
