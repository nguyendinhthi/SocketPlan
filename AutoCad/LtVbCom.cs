using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic;

namespace Edsa.AutoCadProxy
{
    /// <summary>LT_VBCOMLib.Applicationをラップする</summary>
    public class LtVbCom
    {
        /* ラップしている理由
         * AutoCADが何か処理をしている時に、COMオブジェクトのメソッドを実行すると、
         * アプリケーションがビジーであるとCOMExceptionが発生する(ことがある)。
         * それの対策として、ビジーだった場合は再試行するようにしている。
         */

        internal const int LOOP_COUNT = 10;
        internal const int LOOP_SLEEP = 100;

        private LT_VBCOMLib.Application comObject;

        #region COMオブジェクトの取り扱いメソッド

        public void LoadComObject()
        {
            this.ReleaseComObject();

            if (this.TryLoadComObject())
                return;

            ////図面にLT_VBCOM.dwgを突っ込んでから、もう一度ロードを試みる

            //図面内にLT_VBCOMブロックが残ってたらここで消し去る
            AutoCad.Command.PurgeBlocks();

            //LT VB-COM図面を図面上に置く
            AutoCad.Command.SendLineEsc("-insert\n" + @"C:\Program Files\polymorphism\LT_VBCOM2013\LT_VBCOM.dwg" + "\n0,0\n1.0\n\n0.0");

            //「元に戻す」で図面上からLT VB-COMを消す。（イメージとしては、キャッシュに残っている状態にしている）
            AutoCad.Command.SendLine("_undo\n1");

            if (this.TryLoadComObject())
                return;

            throw new AutoCadException("Cannot load LT VB-COM.\nPlease restart AutoCAD.");
        }

        public void ReleaseComObject()
        {
            if (this.HasComObject())
                Marshal.FinalReleaseComObject(this.comObject);

            this.comObject = null;
        }

        public bool HasComObject()
        {
            return this.comObject != null;
        }

        private bool TryLoadComObject()
        {
            try
            {
                this.comObject = (LT_VBCOMLib.Application)Interaction.GetObject(null, "LT_VBCOM.Application");

                if (!this.HasComObject())
                    return false;
            }
            catch
            {
                return false;
            }

            //常にエラーを抑制したいので、必ず最初に呼ばれるここでエラー表示を設定している
            this.SetErrorType(1);

            return true;
        }

        #endregion

        #region ラッパーメソッド

        public bool Cad(string objectType, int objectId, string method, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_cad(objectType, (uint)objectId, method, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            //繰り返し実行してもダメなら、最後にもう一回実行して例外を垂れ流す
            return this.comObject.get_cad(objectType, (uint)objectId, method, ref setData, ref getData);
        }

        public bool Table(string objectType, string method, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_table(objectType, method, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_table(objectType, method, ref setData, ref getData);
        }

        public bool Db(string method, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_db(method, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_db(method, ref setData, ref getData);
        }

        public bool Util(string method, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_util(method, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_util(method, ref setData, ref getData);
        }

        public bool Make(string objectType, ref object newName, ref object setData, ref object parentData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_make(objectType, ref newName, ref setData, ref parentData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_make(objectType, ref newName, ref setData, ref parentData, ref getData);
        }

        public bool MakeEntity(string objectType, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_makeEntity(objectType, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_makeEntity(objectType, ref setData, ref getData);
        }

        public bool Geom(string objectType, ref object initData, string method, ref object setData, ref object getData)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_geom(objectType, ref initData, method, ref setData, ref getData);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_geom(objectType, ref initData, method, ref setData, ref getData);
        }

        public bool CheckTypeIs(string objectType, int objectId)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_typeCheck(objectType, (uint)objectId);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_typeCheck(objectType, (uint)objectId);
        }

        /// <summary>
        /// エラーの表示方法を設定します。
        /// 0:ダイアログで表示 1:コマンドプロンプトに表示 2:非表示
        /// </summary>
        public bool SetErrorType(ushort errorType)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_errorTypeSet(errorType);
                }
                catch (COMException)
                {
                    //this.SleepAndLoad(); //ここで永久ループする可能性あり
                }
            }

            return this.comObject.get_errorTypeSet(errorType);
        }

        public bool StartDatabaseReactor()
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.startDatabaseReactor;
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.startDatabaseReactor;
        }

        public bool EndDatabaseReactor()
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.endDatabaseReactor;
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.endDatabaseReactor;
        }

        public object GetDatabaseReactor(ushort type)
        {
            for (int i = 0; i < LtVbCom.LOOP_COUNT; i++)
            {
                try
                {
                    return this.comObject.get_getDatabaseReactor(type);
                }
                catch (COMException)
                {
                    this.SleepAndLoad();
                }
            }

            return this.comObject.get_getDatabaseReactor(type);
        }

        #endregion

        private void SleepAndLoad()
        {
            Thread.Sleep(LtVbCom.LOOP_SLEEP);
            this.LoadComObject();
        }
    }
}
