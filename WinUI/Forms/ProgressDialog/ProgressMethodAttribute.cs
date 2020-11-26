using System;

namespace SocketPlan.WinUI
{
    /// <summary>
    /// ProgressDialogに表示するメッセージを設定する属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ProgressMethodAttribute : System.Attribute
    {
        #region コンストラクタ

        public ProgressMethodAttribute(string message)
        {
            this.Message = message;
        }

        #endregion

        #region プロパティ
        
        public string Message { get; set; }

        #endregion
    }
}
