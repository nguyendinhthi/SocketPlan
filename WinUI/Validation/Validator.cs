using System.Collections.Generic;
using System.Windows.Forms;

namespace SocketPlan.WinUI
{
    /// <summary>
    /// 検証の前後の処理を共通化しようとしたら、こうなりました。。何も考えずに真似して使ってください。
    /// </summary>
    public class Validator
    {
        static public bool isVisible = false;
        public delegate ErrorDialog ValidatorAction();
        public ValidatorAction Validate;

        /// <summary>messageIdはそのエラー固有の文字列を渡す。承認したことのあるmessageIdだったらエラーは表示しない。</summary>
        public void Run(string messageId)
        {
            if (isVisible == true)
                return;

            //展示場のとき、検証なし
            if (Static.IsTenjijyo)
                return;
            
            //承認済みなら、何もせずに終了して処理を続行する
            if (UnitWiring.AlreadyApproved(messageId))
                return;

            //検証を実行する
            var errorDialog = this.Validate();

            //検証結果に問題が無かったら終了して処理を続行する
            if (errorDialog == null)
                return;

            var dialogResult = errorDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                //エラーダイアログで承認されたなら、何もせずに終了して処理を続行する
                return;
            }

            //承認されなかったら例外を投げて全ての処理を抜ける
            throw new DoNothingException();
        }
    }
}