using System;

namespace Edsa.AutoCadProxy
{
    public class Utility
    {
        public Result<G> DoUtil<G, S>(string method, S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;
            bool isSucceeded = AutoCad.vbcom.Util(method, ref thisSetData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        private Result<G> DoUtil<G>(string method)
        {
            object setData = null;
            object getData = null;
            bool isSucceeded = AutoCad.vbcom.Util(method, ref setData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        public bool DoUtil<S>(string method, S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;

            return AutoCad.vbcom.Util(method, ref thisSetData, ref getData);
        }

        public int GetObjectId(string handleId)
        {
            var result = this.DoUtil<int, string>("ハンドル番号からID取得", handleId);
            if (!result.Success)
                return 0;

            return result.Value;
        }
    }
}
