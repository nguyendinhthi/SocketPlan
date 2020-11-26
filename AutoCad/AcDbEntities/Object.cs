using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Object
    {
        protected virtual ObjectType ObjectType { get { return ObjectType.AcDbObject; } }

        public int GetOwnerId(int objectId)
        {
            var result = this.Get<int>(objectId, "オーナーID取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get owner id.");

            return result.Value;
        }

        public void SetOwnerId(int objectId, int ownerId)
        {
            if (!this.Set<int>(objectId, "オーナーID設定", ownerId))
                throw new AutoCadException("Failed to set owner id.");
        }

        public void Erase(int objectId)
        {
            if (!this.Do(objectId, "erase"))
                throw new AutoCadException("Failed to erase object.");
        }

        public bool IsErased(int objectId)
        {
            Result<short> result = this.Get<short>(objectId, "削除？");
            if (!result.Success)
                throw new AutoCadException("Failed to get IsErased.");

            return result.Value == (short)1;
        }

        public string GetHandleId(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "ハンドル番号取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get handle id.");

            return result.Value;
        }

        protected bool Do(int objectId, string method)
        {
            object setData = null;
            object getData = null;
            return AutoCad.vbcom.Cad(this.ObjectType.ToString(), objectId, method, ref setData, ref getData);
        }

        protected Result<G> Get<G>(int objectId, string method)
        {
            object setData = null;
            object getData = null;
            bool isSucceeded = AutoCad.vbcom.Cad(this.ObjectType.ToString(), objectId, method, ref setData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        protected Result<G> Get<G, S>(int objectId, string method, S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;
            bool isSucceeded = AutoCad.vbcom.Cad(this.ObjectType.ToString(), objectId, method, ref thisSetData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        protected bool Set<S>(int objectId, string method, S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;

            return AutoCad.vbcom.Cad(this.ObjectType.ToString(), objectId, method, ref thisSetData, ref getData);
        }

        protected bool Set(int objectId, string method)
        {
            object thisSetData = null;
            object getData = null;

            return AutoCad.vbcom.Cad(this.ObjectType.ToString(), objectId, method, ref thisSetData, ref getData);
        }

        protected Result<int> Make()
        {
            object setData = null;
            object getData = null;

            bool isSuccess = AutoCad.vbcom.MakeEntity(this.ObjectType.ToString(), ref setData, ref getData);
            if (getData == null)
                return new Result<int>(isSuccess, 0);

            return new Result<int>(isSuccess, (int)getData);
        }

        protected Result<int> Make<S>(S setData)
        {
            object thisSetData = (object)setData;
            object getData = null;

            bool isSuccess = AutoCad.vbcom.MakeEntity(this.ObjectType.ToString(), ref thisSetData, ref getData);

            if (getData == null)
                return new Result<int>(isSuccess, 0);

            return new Result<int>(isSuccess, (int)getData);
        }

        protected Result<int> Make(string name)
        {
            object newName = (object)name;
            object setData = null;
            object parentData = null;
            object getData = null;

            bool isSuccess = AutoCad.vbcom.Make(this.ObjectType.ToString(), ref newName, ref setData, ref parentData, ref getData);
            if (getData == null)
                return new Result<int>(isSuccess, 0);

            return new Result<int>(isSuccess, (int)getData);
        }

        protected Result<G> Geom<G>(string method)
        {
            object initData = null;
            object setData = null;
            object getData = null;

            bool isSucceeded = AutoCad.vbcom.Geom(this.ObjectType.ToString(), ref initData, method, ref setData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        protected Result<G> Geom<G, S, I>(string method, I initData, S setData)
        {
            object thisInitData = (object)initData;
            object thisSetData = (object)setData;
            object getData = null;

            bool isSucceeded = AutoCad.vbcom.Geom(this.ObjectType.ToString(), ref thisInitData, method, ref thisSetData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }

        public bool IsType(int objectId)
        {
            return AutoCad.vbcom.CheckTypeIs(this.ObjectType.ToString(), objectId);
        }

        public List<object> GetExtendedData(int objectId, string applicationTableName)
        {
            AutoCad.Db.RegAppTable.Make(applicationTableName);

            var result = this.Get<object[], string>(objectId, "拡張データ取得", applicationTableName);
            if (!result.Success)
                throw new AutoCadException("Failed to get extended item.");

            if (result.Value == null)
                return null;

            return new List<object>(result.Value);
        }

        public void SetExtendedData(int objectId, List<object> args, string applicationTableName)
        {
            AutoCad.Db.RegAppTable.Make(applicationTableName);

            if (!this.Set<object[]>(objectId, "拡張データ設定", args.ToArray()))
                throw new AutoCadException("Failed to set extended item.");
        }
    }
}