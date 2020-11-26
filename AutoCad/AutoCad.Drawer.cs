using System;
using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public partial class AutoCad
    {
        public class Drawer
        {
            //throw new ApplicationException("Failed to get line info.\nPlease draw line again.");

            public static DrawResult DrawPolyline(bool withId)
            {
                var result = new DrawResult();

                AutoCad.Command.DrawPolyline();

                var isCanceled = AutoCad.Status.IsCanceled();

                if (AutoCad.Status.ContainTextInLastHistory("始点を指定: *キャンセル*", "Specify start point: *Cancel*"))
                {
                    result.Status = DrawStatus.Canceled;
                    return result; ////線を全くひかずにキャンセルしていたら何もせず終了
                }

                if (withId)
                {
                    var lastObjId = AutoCad.Selection.GetLastObjectId();

                    if (!lastObjId.HasValue || !AutoCad.Db.Polyline.IsType(lastObjId.Value))
                    {
                        result.Status = DrawStatus.Failed;
                        return result;
                    }

                    result.ObjectId = lastObjId.Value;
                }

                if (isCanceled)
                    result.Status = DrawStatus.DrawnAndCanceled;
                else
                    result.Status = DrawStatus.Drawn;

                return result;
            }

            /// <summary>線の太さとかを指定したかったら追加パラメータを設定してください</summary>
            public static DrawResult DrawRectangle(string additionalParams)
            {
                //IDが不要な処理が欲しくなったら引数にwithIdを追加してください

                var result = new DrawResult();

                AutoCad.Command.DrawRectangle(additionalParams);

                //Rectangleは作図後にEscできないのでDrawStatus.DrawnAndCanceledはありえない

                if (AutoCad.Status.IsCanceled())
                {
                    result.Status = DrawStatus.Canceled;
                    return result;
                }

                var lastObjId = AutoCad.Selection.GetLastObjectId();
                if (!lastObjId.HasValue || !AutoCad.Db.Polyline.IsType(lastObjId.Value))
                {
                    result.Status = DrawStatus.Failed;
                    return result;
                }

                result.ObjectId = lastObjId.Value;
                result.Status = DrawStatus.Drawn;

                return result;
            }
        }
    }
}