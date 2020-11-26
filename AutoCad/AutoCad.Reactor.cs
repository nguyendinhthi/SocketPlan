using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Edsa.AutoCadProxy
{
    public partial class AutoCad
    {
        public class Reactor : IDisposable
        {
            /// <summary>【警告】usingで括って使ってください。ユーザーが図形に対して操作した時に、図形Idが記録される</summary>
            internal Reactor() //詳しくは良く分からない・・・
            {
                this.StartReactor();
            }

            #region IDisposable メンバ

            void IDisposable.Dispose()
            {
                this.EndReactor();
            }

            #endregion

            private void StartReactor()
            {
                if (!AutoCad.vbcom.StartDatabaseReactor())
                    throw new AutoCadException("Failed to start reactor.");
            }

            private void EndReactor()
            {
                if (!AutoCad.vbcom.EndDatabaseReactor())
                    throw new AutoCadException("Failed to close reactor.");
            }

            public List<int> GetReactor()
            {
                var result = AutoCad.vbcom.GetDatabaseReactor(1);

                if (result == null)
                    return new List<int>();
                else
                    return new List<int>((int[])result);
            }
        }
    }
}