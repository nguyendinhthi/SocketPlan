using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class MessageDialog
    {
        private const string TITLE = "SocketPlan";

        public static DialogResult ShowInformation(IWin32Window owner, string message)
        { //ownerはフォームの裏に隠れてしまう対策
            return MessageBox.Show(owner, message, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowError(Exception exception)
        {
            if (exception.InnerException != null)
                exception = exception.InnerException;

            if (exception is DoNothingException)
                return DialogResult.OK; //何もせず抜ける。例外をもみ消すのだ。

            if(exception is ApplicationException)
                return MessageBox.Show(exception.Message, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //if(exception.InnerException != null)
            //    return MessageBox.Show(exception.InnerException.ToString(), TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return MessageBox.Show(exception.ToString(), TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowOkCancel(string message)
        {
            return MessageBox.Show(message, TITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        public static DialogResult ShowYesNo(string message)
        {
            return MessageBox.Show(message, TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ShowYesNoCancel(string message)
        {
            return MessageBox.Show(message, TITLE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
