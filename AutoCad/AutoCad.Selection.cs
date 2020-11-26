using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    //AutoCAD上での、図形選択に関する処理
    public partial class AutoCad
    {
        public class Selection
        {
            /// <summary>AutoCAD上で図形を選択させ、その図形のObjectIdを返す。</summary>
            public static List<int> SelectObjects()
            {
                return SelectObjects(string.Empty);
            }
            /// <summary>AutoCAD上で図形を選択させ、その図形のObjectIdを返す。(Prompt付き)</summary>
            public static List<int> SelectObjects(string prompt)
            {
                //AutoCADを最前面に持ってくる
                WindowController2.BringAutoCadToTop();

                //Selectの最後(LAST)で選択できる図形をなくす。
                AutoCad.Command.SendLineEsc("point 0.003,0.003");
                AutoCad.Command.SendLineEsc("zoom 0.001,0.001 0.005,0.005");
                AutoCad.Command.SendLineEsc("erase last ");
                AutoCad.Command.SendLineEsc("zoom p");

                //ユーザーに選択を促す
                AutoCad.Command.SendLineEsc("select");
                if (!string.IsNullOrEmpty(prompt))
                    AutoCad.Prompt(prompt);

                AutoCad.Status.WaitFinish();
                if (AutoCad.Status.IsCanceled())
                    return new List<int>();

                using (var reactor = new AutoCad.Reactor())
                {
                    //移動変位0で移動(リアクタに記録する)
                    AutoCad.Command.SendLineEsc("move p  0,0,0 0,0,0");

                    return reactor.GetReactor();
                }
            }

            /// <summary>選択している図形のObjectIdを取得する</summary>
            public static List<int> GetSelectedObjectIds()
            {
                using (var reactor = new AutoCad.Reactor())
                {
                    //移動変位0で移動(リアクタに記録する)
                    AutoCad.Command.SendLine("move");

                    if (!AutoCad.Status.ContainTextInLastHistory("認識された数:", "found"))
                    {
                        AutoCad.Command.Prepare();
                        return new List<int>();
                    }

                    AutoCad.Command.SendLine("0,0,0 0,0,0");

                    return reactor.GetReactor();
                }
            }

            /// <summary>図面をクリックさせて、クリックした座標を取得する</summary>
            public static PointD GetClickPoint()
            {
                AutoCad.Command.SendLineEsc("POINT");

                AutoCad.Status.WaitHistory("点を指定:", "Specify a point:");
                if (AutoCad.Status.IsCanceled())
                    return null;

                var pointId = AutoCad.Selection.GetLastObjectId();
                if (!pointId.HasValue)
                    return null;

                var point = AutoCad.Db.Point.GetPosition(pointId.Value);

                AutoCad.Db.Entity.Erase(pointId.Value);

                return point;
            }

            /// <summary>図面をクリックさせて、クリックした座標を取得する。ラバーハンド付</summary>
            public static PointD GetClickPoint(PointD basePoint)
            {
                AutoCad.Command.SendLineEsc("POINT " + basePoint.ToString());

                var pointId = AutoCad.Selection.GetLastObjectId();
                if (!pointId.HasValue)
                    return null;

                AutoCad.Command.SendLineEsc("MOVE\nLAST\n\n" + basePoint.ToString());

                AutoCad.Status.WaitHistory("目的点を指定 または <基点を移動距離として使用>:", "Specify second point of displacement or <use first point as displacement>:");

                if (AutoCad.Status.IsCanceled())
                {
                    AutoCad.Db.Entity.Erase(pointId.Value);
                    return null;
                }

                var point = AutoCad.Db.Point.GetPosition(pointId.Value);

                AutoCad.Db.Entity.Erase(pointId.Value);

                return point;
            }

            /// <summary>最後に選択した図形のIDを返す</summary>
            public static int? GetLastObjectId()
            {
                //これ使うの禁止。図面上の図形が多くなると処理が超重くなる。
                //    var result = this.DoUtil<int>("最後の図形"); 

                //置いた図形を画面外にもっていくと間違った図形のIDを取る
                var ids = AutoCad.Selection.GetLastObjectIds();
                if (ids.Count == 0)
                    return null;

                return ids[ids.Count - 1];
            }

            public static List<int> GetLastObjectIds()
            {
                AutoCad.Command.SendLine("select l "); //これで他の図形が選ばれてしまうことがある

                using (var reactor = new AutoCad.Reactor())
                {
                    //移動変位0で移動(リアクタに記録する)
                    AutoCad.Command.SendLineEsc("move p  0,0,0 0,0,0");

                    return reactor.GetReactor();
                }
            }
        }
    }
}
