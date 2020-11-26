using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public class LayoutDrawer
    {
        public static List<int> InsertLayoutTexts(List<LayoutText> layoutTexts)
        {
            AutoCad.Db.TextStyleTableRecord.Make(Const.Font.MSGothic, Const.Font.MSGothic);

            var textIds = new List<int>();

            foreach (var layoutText in layoutTexts)
            {
                PointD point = new PointD();
                point.X = Convert.ToDouble(layoutText.PointX);
                point.Y = Convert.ToDouble(layoutText.PointY);
                point.Z = 0;
                int objId = 0;
                if (layoutText.TypeId == Const.LayoutTextType.縮尺)
                {
                    int viewportId = AutoCad.Db.Viewport.GetId();

                    var customScale = AutoCad.Db.Viewport.GetCustomScale(viewportId);

                    double scale = 1 / customScale;

                    int[] scaleList = { 50, 60, 70, 80, 90, 100, 110, 120 };
                    foreach (var s in scaleList)
                    {
                        if (scale <= s)
                        {
                            scale = s;
                            break;
                        }
                    }

                    AutoCad.Db.Viewport.SetCustomScale(viewportId, 1.0 / scale);

                    objId = AutoCad.Db.Text.Make("1/" + scale, Const.Font.MSGothic, (double)layoutText.FontSize, point);
                }
                else if (layoutText.TypeId == Const.LayoutTextType.図面番号)
                {
                    objId = AutoCad.Db.Text.Make(Static.Drawing.PlanRevisionNoWithHyphen, Const.Font.MSGothic, (double)layoutText.FontSize, point);
                }
                else
                {
                    var text = layoutText.Value.Trim();

                    if (string.IsNullOrEmpty(text))
                        continue;

                    objId = AutoCad.Db.Text.Make(text, Const.Font.MSGothic, (double)layoutText.FontSize, point);
                }

                AutoCad.Db.Text.SetAlignment(objId, Align.中央);
                AutoCad.Db.Text.SetAlignmentPoint(objId, point);

                var textWidth = AutoCad.Db.Entity.GetWidth(objId);
                if (Convert.ToDouble(layoutText.MaxWidth) < textWidth)
                    AutoCad.Db.Text.SetWidthFactor(objId, Convert.ToDouble(layoutText.MaxWidth) / textWidth);

                textIds.Add(objId);
            }

            return textIds;
        }
    }
}
