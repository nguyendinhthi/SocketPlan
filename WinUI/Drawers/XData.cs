using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    /// <summary>AutoCADのオブジェクトに設定する拡張データを扱うクラス</summary>
    public class XData
    {
        /// <summary>配線の拡張データを扱うクラス</summary>
        public class Wire
        {
            //常に以下の形式で登録する
            //------------------
            //  0: 1001
            //  1: UnitWiring
            //  2: 1000
            //  3: (床下配線フラグ(UnderfloorWiring))
            //  4: 1000
            //  5: (接続するシンボルのHandleId)
            //------------------

            public static string GetUnderfloor(int objectId)
            {
                var data = Get(objectId);
                return data[3].ToString();
            }

            public static void SetUnderfloor(int objectId, string text)
            {
                var data = Get(objectId);
                data[3] = text;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetConnectedSymbol(int objectId)
            {
                var data = Get(objectId);
                return data[5].ToString();
            }

            public static void SetConnectedSymbol(int objectId, string text)
            {
                var data = Get(objectId);
                data[5] = text;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }

            private static List<object> Get(int objectId)
            {
                var data = AutoCad.Db.Object.GetExtendedData(objectId, Const.APPLICATION_TABLE_NAME);
                if (data == null)
                    data = Create();

                if (data.Count != 6)
                {
                    var newData = Create();

                    foreach (var dat in data)
                    {
                        if (string.IsNullOrEmpty(dat.ToString()))
                            continue;

                        var index = data.IndexOf(dat);
                        newData[index] = data[index];
                    }

                    data = newData;
                }

                return data;
            }

            private static List<object> Create()
            {
                var data = new List<object>();

                //アプリ名
                data.Add(1001); //次のデータがアプリケーションテーブル名であることを表すID
                data.Add(Const.APPLICATION_TABLE_NAME);

                //床下配線フラグ
                data.Add(1000); //次のデータが文字列であることを表すID
                data.Add("");

                //接続するシンボルのHandleId
                data.Add(1000);
                data.Add("");

                return data;
            }
        }

        /// <summary>プレートの拡張データを扱うクラス</summary>
        public class Plate
        {
            //常に以下の形式で登録する
            //------------------
            //  0: 1001
            //  1: UnitWiring
            //  2: 1000
            //  3: シンボルのHandleId
            //　以下、2,3の繰り返し
            //------------------

            public static List<string> GetPlateSymbolHandleIds(int objectId)
            {
                var data = AutoCad.Db.Object.GetExtendedData(objectId, Const.APPLICATION_TABLE_NAME);
                if (data == null)
                    return new List<string>();

                var list = new List<string>();
                for (int i = 0; i < data.Count; i = i + 2)
                {
                    var id = Convert.ToInt32(data[i]);
                    if (id == 1000)
                        list.Add(data[i + 1].ToString());
                }

                return list;
            }

            public static void SetPlateSymbolHandleIds(int objectId, List<string> texts)
            {
                var args = new List<object>();
                args.Add(1001); //次のデータがアプリケーションテーブル名であることを表すID
                args.Add(Const.APPLICATION_TABLE_NAME);

                foreach (var text in texts)
                {
                    args.Add(1000); //次のデータが文字列であることを表すID
                    args.Add(text);
                }

                AutoCad.Db.Object.SetExtendedData(objectId, args, Const.APPLICATION_TABLE_NAME);
            }
        }

        /// <summary>部屋外周線の拡張データを扱うクラス</summary>
        public class Room
        {
            //常に以下の形式で登録する
            //------------------
            //  0: 1001
            //  1: UnitWiring
            //  2: 1000
            //  3: (部屋名)
            //  4: 1000
            //  5: (帖数)
            //  6: 1000
            //  7: (タレ壁帖数)
            //  8: 1000
            //  9: (部屋コード)
            //------------------

            public static string GetRoomName(int objectId)
            {
                var data = Get(objectId);
                return data[3].ToString();
            }

            public static void SetRoomName(int objectId, string text)
            {
                var data = Get(objectId);
                data[3] = text;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetRoomJyou(int objectId)
            {
                var data = Get(objectId);
                return data[5].ToString();
            }

            public static void SetRoomJyou(int objectId, string text)
            {
                var data = Get(objectId);
                data[5] = text;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetRoomTarekabeJyou(int objectId)
            {
                var data = Get(objectId);
                return data[7].ToString();
            }

            public static void SetRoomTarekabeJyou(int objectId, string text)
            {
                var data = Get(objectId);
                data[7] = text;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static void SetRoomCode(int objectId, string code)
            {
                var data = Get(objectId);
                data[9] = code;
                AutoCad.Db.Object.SetExtendedData(objectId, data, Const.APPLICATION_TABLE_NAME);
            }
            
            public static string GetRoomCode(int objectId)
            {
                var data = Get(objectId);
                if(data.Count < 10)
                    return string.Empty;

                return data[9].ToString();
            }

            private static List<object> Get(int objectId)
            {
                var data = AutoCad.Db.Object.GetExtendedData(objectId, Const.APPLICATION_TABLE_NAME);
                if (data == null)
                    data = Create();

                //データの数が現在の既定数と違う時は、
                //新たに入れ物を作り、存在するデータだけそこに移し替える
                if (data.Count != 10)
                {
                    var newData = Create();

                    foreach (var dat in data)
                    {
                        if (string.IsNullOrEmpty(dat.ToString()))
                            continue;

                        var index = data.IndexOf(dat);
                        newData[index] = data[index];
                    }

                    data = newData;
                }

                return data;
            }

            private static List<object> Create()
            {
                var data = new List<object>();

                //アプリ名
                data.Add(1001); //次のデータがアプリケーションテーブル名であることを表すID
                data.Add(Const.APPLICATION_TABLE_NAME);

                //部屋名
                data.Add(1000);
                data.Add("");

                //帖数
                data.Add(1000); //次のデータが文字列であることを表すID
                data.Add("");

                //タレ壁帖数
                data.Add(1000); //次のデータが文字列であることを表すID
                data.Add("");

                //部屋コード
                data.Add(1000);
                data.Add("");

                return data;
            }
        }

        /// <summary>回路の拡張データを扱うクラス</summary>
        public class Kairo
        {
            //常に以下の形式で登録する
            //------------------
            //  0: 1001
            //  1: UnitWiring
            //  2: 1000
            //  3: (回路の枠のHandleId)
            //  4: 1000
            //  5: (回路のハッチングのHandleId)
            //  6: 1000
            //  7: (専用コンセントのHandleId)
            //------------------

            public static string GetRegionHandleId(int regionId)
            {
                var data = Get(regionId);
                return data[3].ToString();
            }

            public static void SetRegionHandleId(int regionId, string text)
            {
                var data = Get(regionId);
                data[3] = text;
                AutoCad.Db.Object.SetExtendedData(regionId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetHatchingHandleId(int hatchingId)
            {
                var data = Get(hatchingId);
                return data[5].ToString();
            }

            public static void SetHatchingHandleId(int hatchingHandleId, string text)
            {
                var data = Get(hatchingHandleId);
                data[5] = text;
                AutoCad.Db.Object.SetExtendedData(hatchingHandleId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetSymbolHandleId(int kairoNumberId)
            {
                var data = Get(kairoNumberId);
                return data[7].ToString();
            }

            public static void SetSymbolHandleId(int kairoNumberId, string symbolHandleId)
            {
                var data = Get(kairoNumberId);
                data[7] = symbolHandleId;
                AutoCad.Db.Object.SetExtendedData(kairoNumberId, data, Const.APPLICATION_TABLE_NAME);
            }

            private static List<object> Get(int objectId)
            {
                var data = AutoCad.Db.Object.GetExtendedData(objectId, Const.APPLICATION_TABLE_NAME);
                if (data == null)
                    data = Create();

                if (data.Count != 8)
                {
                    var newData = Create();

                    foreach (var dat in data)
                    {
                        if (string.IsNullOrEmpty(dat.ToString()))
                            continue;

                        var index = data.IndexOf(dat);
                        newData[index] = data[index];
                    }

                    data = newData;
                }

                return data;
            }

            private static List<object> Create()
            {
                var data = new List<object>();

                //アプリ名
                data.Add(1001); //次のデータがアプリケーションテーブル名であることを表すID
                data.Add(Const.APPLICATION_TABLE_NAME);

                //回路の枠のHandleId
                data.Add(1000); //次のデータが文字列であることを表すID
                data.Add("");

                //回路のハッチングのHandleId
                data.Add(1000);
                data.Add("");

                //専用コンセントのHandleId
                data.Add(1000);
                data.Add("");

                return data;
            }
        }

        /// <summary>シンボルの拡張データを扱うクラス</summary>
        public class Symbol
        {
            //常に以下の形式で登録する
            //------------------
            //  0: 1001
            //  1: UnitWiring
            //  2: 1000
            //  3: (このシンボルのハンドルID（専用回路との関連付け用）)

            public static string GetSocketBoxSeq(int symbolId)
            {
                var data = Get(symbolId);
                return data[3].ToString();
            }

            public static void SetSocketBoxSeq(int symbolId, int seq)
            {
                var data = Get(symbolId);
                data[3] = seq.ToString();
                AutoCad.Db.Object.SetExtendedData(symbolId, data, Const.APPLICATION_TABLE_NAME);
            }

            public static string GetSymbolHandleId(int symbolId)
            {
                var data = Get(symbolId);
                return data[3].ToString();
            }

            public static void SetSymbolHandleId(int symbolId, string symbolHandleId)
            {
                var data = Get(symbolId);
                data[3] = symbolHandleId;
                AutoCad.Db.Object.SetExtendedData(symbolId, data, Const.APPLICATION_TABLE_NAME);
            }

            private static List<object> Get(int objectId)
            {
                var data = AutoCad.Db.Object.GetExtendedData(objectId, Const.APPLICATION_TABLE_NAME);
                if (data == null)
                    data = Create();

                if (data.Count != 4)
                {
                    var newData = Create();

                    foreach (var dat in data)
                    {
                        if (string.IsNullOrEmpty(dat.ToString()))
                            continue;

                        var index = data.IndexOf(dat);
                        newData[index] = data[index];
                    }

                    data = newData;
                }

                return data;
            }

            private static List<object> Create()
            {
                var data = new List<object>();

                data.Add(1001); //次のデータがアプリケーションテーブル名であることを表すID
                data.Add(Const.APPLICATION_TABLE_NAME);

                data.Add(1000); //次のデータが文字列であることを表すID
                data.Add("");

                return data;
            }
        }

        //TODO ここに詰め込んだクラス、共通化して無駄なくしたいなぁ
    }
}