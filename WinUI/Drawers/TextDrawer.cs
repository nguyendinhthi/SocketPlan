using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class TextDrawer
    {
        public static void DrawTexts(Comment comment)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();

            //レイヤーを変える
            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_コメント, CadColor.BlackWhite, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_コメント);

            //テキストスタイルを作る
            AutoCad.Db.TextStyleTableRecord.Make(Const.Font.MSGothic, Const.Font.MSGothic);

            var textId = 0;
            do
            {
                textId = TextDrawer.DrawText(comment.Text, CadColor.Red);
            }
            while (!AutoCad.Status.IsCanceled());

            if (textId != 0)
                AutoCad.Db.Object.Erase(textId);
        }

        public static int DrawText(string text, CadColor color)
        {
            var center = AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout();

            var drawing = Drawing.GetCurrent();
            double textHeight = 120;
            if (drawing.IsElevation)
                textHeight = 240;

            //テキストを置く
            var textId = AutoCad.Db.Text.Make(text, Const.Font.MSGothic, textHeight, center);
            AutoCad.Db.Entity.SetColor(textId, color);

            //テキストを動かす
            AutoCad.Command.SendLineEsc("m\nl\n\n" + center.X + "," + center.Y);

            AutoCad.Status.WaitFinish();

            return textId;
        }

    }
}
