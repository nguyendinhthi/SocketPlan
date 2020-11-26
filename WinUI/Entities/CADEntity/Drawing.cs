using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class Drawing
    {
        //新ファイル名(from 2013/11/1らへん)
        //電気図面：HPD-0000000-9999-0101A-D1-1.dwg
        //　他図面：HPD-0000000-9999-0101A-D1-BS-1.dwg
        //          0   1       2    3     4  5  6

        //電気図面以外はDrawingインスタンスにする必要が無いから、このクラスは電気図面にだけ対応させよう。

        #region プロパティ

        public IntPtr WindowHandle { get; set; }
        public string FullPath { get; set; }
        public int ModelId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Location { get; set; }

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.FullPath);
            }
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(this.FullPath);
            }
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(this.FullPath);
            }
        }

        public string Directory
        {
            get
            {
                return Path.GetDirectoryName(this.FullPath);
            }
        }

        private List<string> Segments
        {
            get
            {
                return new List<string>(this.Name.Split('-'));
            }
        }

        public bool IsDenkiPlan
        {
            get
            {
                return this.Segments.Count == 6;
            }
        }

        public string Suffix
        {
            get
            {
                if (this.Segments.Count > 6)
                    return this.Segments[5];
                else
                    return string.Empty;
            }
        }

        public bool IsPiPlan
        {
            get
            {
                if(this.Segments.Count != 7)
                    return false;

                if (this.Segments[5] != "PI")
                    return false;

                return true;
            }
        }
        public bool IsPERPlan
        {
            get
            {
                if (this.Segments.Count != 7)
                    return false;
                if (this.Segments[5] != "PER")
                    return false;

                return true;
            }
        }

        public bool IsSocketPlan
        {
            get
            {
                if (this.Segments.Count != 7)
                    return false;

                if (this.Segments[5] != "SP_Individual" &&
                    this.Segments[5] != "SP_Pattern")
                    return false;

                return true;
            }
        }

        /// <summary>HJAは加工依頼後の図面。他はしらね。</summary>
        public string Prefix
        {
            get
            {
                return this.Segments[0];
            }
        }

        public string ConstructionCode
        {
            get
            {
                return this.Segments[1] + "-" + this.Segments[2];
            }
        }

        public string PlanNo
        {
            get
            {
                return this.Segments[3];
            }
        }

        public string PlanNoWithHyphen
        {
            get
            {
                return Utilities.ConvertPlanNo(this.PlanNo);
            }
        }

        public string RevisionNo
        {
            get
            {
                return this.Segments[4];
            }
        }

        public string FloorCode
        {
            get
            {
                if (this.Segments.Count > 6)
                    return this.Segments[6];
                else
                    return this.Segments[5];
            }
        }

        public string FloorCodeForPI
        {
            get
            {
                if (this.Segments.Count <= 6)
                    return string.Empty;
                else
                    return this.Segments[6];
            }
        }

        public int Floor
        {
            get
            {
                if (this.IsElevation)
                    return 0;

                if (this.IsBasement)
                    return 6;

                if (this.IsLoft)
                    return 7;

                if (this.IsPentHouse)
                    return 8;

                if (this.IsParapet)
                    return 9;

                int floor;
                if (int.TryParse(this.FloorCode, out floor))
                    return floor;

                return 0;
            }
        }

        public int FloorForPI
        {
            get
            {
                if (this.Segments.Count < 7)
                    return 0;

                if (this.FloorCodeForPI == "B")
                    return 6;

                if (this.FloorCodeForPI == "R")
                    return 7;

                if (this.FloorCodeForPI == "PH")
                    return 8;

                if (this.FloorCodeForPI == "P")
                    return 9;

                int floor;
                if (int.TryParse(this.Segments[6],out floor))
                    return floor;

                return 0;
            }
        }

        /// <summary>仕様書ではその他の階は全部9階になっている</summary>
        public int SiyoFloor
        {
            get
            {
                if (6 <= this.Floor)
                    return 9;

                return this.Floor;
            }
        }

        public bool IsElevation
        {
            get
            {
                return this.FloorCode.StartsWith("E");
            }
        }

        public bool IsLoft
        {
            get
            {
                return this.FloorCode == "R";
            }
        }

        public bool IsPentHouse
        {
            get
            {
                return this.FloorCode == "PH";
            }
        }

        public bool IsParapet
        {
            get
            {
                return this.FloorCode == "P";
            }
        }

        //ロフトとパラペットの記号が一緒だ・・・ｼﾗﾈ
        //↑ロフトがRでパラペットがPとなりました。
        public bool IsBasement
        {
            get
            {
                return this.FloorCode == "B";
            }
        }

        public string ConstructionCodePlanRevisionNo
        {
            get
            {
                return this.ConstructionCode + "-" + this.PlanRevisionNoWithoutHyphen;
            }
        }

        public string PlanRevisionNoWithoutHyphen
        {
            get
            {
                return this.PlanNo + "-" + this.RevisionNo;
            }
        }

        public string PlanRevisionNoWithHyphen
        {
            get
            {
                return Utilities.ConvertPlanNo(this.PlanNo) + "-" + this.RevisionNo;
            }
        }

        #endregion

        #region メソッド

        /// <summary>この図面をAutoCADの最前面に持ってくる</summary>
        public void Focus()
        {
            WindowController2.BringDrawingToTop(this.WindowHandle);
        }

        #endregion

        #region staticメソッド

        /// <summary>照明仕様書用の階に変更する</summary>
        public static int GetFloorNumberForLightCsv(int floor)
        {
            //その他の階は9にする
            if (6 <= floor)
                return 9;

            return floor;
        }

        /// <summary>拾い帳票用の階表記に変更する</summary>
        public static string GetFloorCodeForWirePickingReport(int floor)
        {
            if (floor == 6) //地下
                return "B";
            if (floor == 7) //ロフト
                return "R";
            if (floor == 8) //ペントハウス
                return "PH";
            if (floor == 9) //パラペット
                return "P";

            return floor.ToString();
        }

        /// <summary>図面枠、ブレーカーリスト用の階表記に変更する</summary>
        public static string GetFloorName(int floor)
        {
            if (floor == 6) //地下
                return "地下";
            if (floor == 7) //ロフト
                return "R";
            if (floor == 8) //ペントハウス
                return "PH";
            if (floor == 9) //パラペット
                return "P";

            return floor.ToString();
        }

        public static List<Drawing> GetAll(bool withElevationPlan)
        {
            //AutoCAD2013でレイヤ管理画面を表示したまま、図面を切り替えるとエラーになる。
            //対策としてここでやっときゃいいっしょ。
            AutoCad.Command.CloseLayerManager();

            var drawingHandles = WindowController2.GetDrawingHandles();

            //MDIの子画面のサイズを小さくされていると、図面のファイル名を正しく取れないことがある。それ対策。
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {
                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;
                 
                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (!drawing.IsDenkiPlan)
                    continue;

                if (!withElevationPlan && drawing.IsElevation)
                    continue;

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                drawings.Add(drawing);
            }

            drawings.Sort((p, q) => { return p.FloorCode.CompareTo(q.FloorCode); });

            return drawings;
        }

        public static List<Drawing> GetAllForSocketPlan(SocketPlanType? type)
        {
            //AutoCAD2013でレイヤ管理画面を表示したまま、図面を切り替えるとエラーになる。
            //対策としてここでやっときゃいいっしょ。
            AutoCad.Command.CloseLayerManager();

            var drawingHandles = WindowController2.GetDrawingHandles();

            //MDIの子画面のサイズを小さくされていると、図面のファイル名を正しく取れないことがある。それ対策。
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {
                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;

                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (!drawing.IsSocketPlan)
                    continue;

                if (type.HasValue)
                {
                    if (type.Value == SocketPlanType.Individual &&
                        drawing.FileName.Contains("Pattern"))
                        continue;

                    if (type.Value == SocketPlanType.Pattern &&
                        drawing.FileName.Contains("Individual"))
                        continue;
                }

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                drawings.Add(drawing);
            }

            drawings.Sort((p, q) => { return p.FloorCode.CompareTo(q.FloorCode); });

            return drawings;
        }

        /// <summary>SocketPlan用</summary>
        public static List<Drawing> GetAllForSP()
        {
            //AutoCAD2013でレイヤ管理画面を表示したまま、図面を切り替えるとエラーになる。
            //対策としてここでやっときゃいいっしょ。
            AutoCad.Command.CloseLayerManager();

            var drawingHandles = WindowController2.GetDrawingHandles();

            //MDIの子画面のサイズを小さくされていると、図面のファイル名を正しく取れないことがある。それ対策。
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {
                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;

                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (!drawing.IsPiPlan)
                    continue;

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                drawings.Add(drawing);
            }

            drawings.Sort((p, q) => { return p.FloorForPI.CompareTo(q.FloorForPI); });

            return drawings;
        }

        public static List<Drawing> GetAllForPI()
        {
            //AutoCAD2013でレイヤ管理画面を表示したまま、図面を切り替えるとエラーになる。
            //対策としてここでやっときゃいいっしょ。
            AutoCad.Command.CloseLayerManager();

            var drawingHandles = WindowController2.GetDrawingHandles();

            //MDIの子画面のサイズを小さくされていると、図面のファイル名を正しく取れないことがある。それ対策。
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {
                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;

                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (!drawing.IsPiPlan)
                    continue;

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                drawings.Add(drawing);
            }

            drawings.Sort((p, q) => { return p.FloorForPI.CompareTo(q.FloorForPI); });

            return drawings;
        }

        public static List<Drawing> GetAllForPER()
        {
            //AutoCAD2013でレイヤ管理画面を表示したまま、図面を切り替えるとエラーになる。
            //対策としてここでやっときゃいいっしょ。
            AutoCad.Command.CloseLayerManager();

            var drawingHandles = WindowController2.GetDrawingHandles();

            //MDIの子画面のサイズを小さくされていると、図面のファイル名を正しく取れないことがある。それ対策。
            if (drawingHandles.Count != 0)
                WindowController2.Maximize(drawingHandles[0]);

            var drawings = new List<Drawing>();
            foreach (var handle in drawingHandles)
            {
                var title = WindowController2.GetWindowTitle(handle);
                string fileName = Path.GetFileNameWithoutExtension(title);

                if (!fileName.Contains(Static.ConstructionCode))
                    continue;

                var drawing = new Drawing();
                drawing.WindowHandle = handle;
                drawing.FullPath = title;

                if (!drawing.IsPERPlan)
                    continue;

                if (drawing.PlanNo != Static.Drawing.PlanNo)
                    continue;

                if (drawing.RevisionNo != Static.Drawing.RevisionNo)
                    continue;

                drawings.Add(drawing);
            }

            drawings.Sort((p, q) => { return p.FloorCode.CompareTo(q.FloorCode); });

            return drawings;
        }

        public static Drawing GetCurrent()
        {
            var currentDrawingHandle = WindowController2.GetTopDrawingHandle();

            var drawing = new Drawing();
            drawing.WindowHandle = currentDrawingHandle;
            drawing.FullPath = WindowController2.GetWindowTitle(currentDrawingHandle);

            return drawing;
        }

        /// <summary>指定したフォルダ内から対象現場の図面をリストアップする</summary>
        public static List<Drawing> Search(string constructionCode, string path, string locationName)
        {
            //このメソッドを呼ぶ時点ではStatic.ConstructionCodeが登録されていない。
            if (!System.IO.Directory.Exists(path + constructionCode))
                return new List<Drawing>();

            var fullPaths = System.IO.Directory.GetFiles(path + constructionCode);
            var drawings = new List<Drawing>();
            foreach (var fullPath in fullPaths)
            {
                var fileName = Path.GetFileName(fullPath);

                if (!Drawing.StartWithAllowedPrefix(fileName))
                    continue;

                if (fileName.Contains("recover")) //AutoCADが強制終了した時に出来ちゃうrecoverファイルは無視する
                    continue;

                var extension = Path.GetExtension(fileName);
                if (!(extension.ToUpper() == ".DXF" || extension.ToUpper() == ".DWG"))
                    continue;

                var drawing = Create(fullPath, locationName);
                if(drawing.IsDenkiPlan)
                    drawings.Add(drawing);
            }

            return drawings;
        }

        public static Drawing Create(string fullPath, string locationName)
        {
            var drawing = new Drawing();
            drawing.FullPath = fullPath;
            drawing.Location = locationName;
            drawing.UpdatedDate = File.GetLastWriteTime(fullPath);

            return drawing;
        }

        private static bool StartWithAllowedPrefix(string fileName)
        {
            foreach (var prefix in Properties.Settings.Default.DrawingPrefixes)
            {
                if (fileName.StartsWith(prefix))
                    return true;
            }

            return false;
        }

        public static int OpenAll(List<Drawing> drawings)
        {
            var drawingHandles = WindowController2.GetDrawingHandles();
            if (drawingHandles.Count == 0)
            { //図面が一枚も開かれていない場合、コマンドラインが表示されず、以降の処理がスカる。それ対策
                //SendCommandだとﾃﾝﾌﾟﾚｰﾄﾌｧｲﾙ名を入力するまで処理が帰ってこない
                AutoCad.File.Create(".");
            }

            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();

            AutoCad.Db.Database.SetFileDialogMode(false);

            AutoCad.Command.DisableSDI();

            //AutoCAD2013対応ここから
            AutoCad.Command.SmoothZoomOff();
            AutoCad.Command.SetInsertUnitToMM();
            //ここまで

            drawings.Sort((p, q) => p.FloorCode.CompareTo(q.FloorCode)); //1階図面を最後に開くために階の降順でソートする

            int count = 0;
            //同ファイル名の図面を開くと、読み取り専用になって動かなくなるので、同名が既に開いていたら開かない。
            var openedDrawings = Drawing.GetAll(true);
            foreach (var drawing in drawings)
            {
                if (openedDrawings.Exists(p => p.FileName == drawing.FileName))
                    continue;

                AutoCad.File.Open(drawing.FullPath);

                AutoCad.Command.ZoomAll();
                count++;
            }

            AutoCad.Db.Database.SetFileDialogMode(true);

            return count;
        }

        public static int OpenAllForSocketPlan(List<Drawing> drawings)
        {
            var drawingHandles = WindowController2.GetDrawingHandles();
            if (drawingHandles.Count == 0)
            { //図面が一枚も開かれていない場合、コマンドラインが表示されず、以降の処理がスカる。それ対策
                //SendCommandだとﾃﾝﾌﾟﾚｰﾄﾌｧｲﾙ名を入力するまで処理が帰ってこない
                AutoCad.File.Create(".");
            }

            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();

            AutoCad.Db.Database.SetFileDialogMode(false);

            AutoCad.Command.DisableSDI();

            //AutoCAD2013対応ここから
            AutoCad.Command.SmoothZoomOff();
            AutoCad.Command.SetInsertUnitToMM();
            //ここまで

            AutoCad.Command.SendLineEsc("PICKSTYLE 1"); //グループ選択必須

            drawings.Sort((p, q) => p.FloorCode.CompareTo(q.FloorCode)); //1階図面を最後に開くために階の降順でソートする

            int count = 0;
            //同ファイル名の図面を開くと、読み取り専用になって動かなくなるので、同名が既に開いていたら開かない。
            var openedDrawings = Drawing.GetAllForSocketPlan(null);
            foreach (var drawing in drawings)
            {
                if (openedDrawings.Exists(p => p.FileName == drawing.FileName))
                    continue;

                AutoCad.File.Open(drawing.FullPath);

                AutoCad.Command.ZoomAll();
                AutoCad.Command.SetCurrentLayoutToModel();
                count++;
            }

            AutoCad.Db.Database.SetFileDialogMode(true);

            return count;
        }

        public static int SaveAll(string savePath)
        {
            if (!savePath.EndsWith(@"\"))
                savePath += @"\";

            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();

            if (!System.IO.Directory.Exists(savePath + Static.ConstructionCode))
                System.IO.Directory.CreateDirectory(savePath + Static.ConstructionCode);

            var drawings = Drawing.GetAll(true);
            drawings.AddRange(Drawing.GetAllForSocketPlan(null));

            AutoCad.Db.Database.SetFileDialogMode(false);

            foreach (var drawing in drawings)
            {
                WindowController2.BringDrawingToTop(drawing.WindowHandle);

                string fileName = Static.Drawing.Prefix + "-" + 
                                  Static.Drawing.ConstructionCodePlanRevisionNo + "-" +
                                  drawing.FloorCode + ".dwg";

                if (drawing.IsSocketPlan)
                    fileName = drawing.FileName;

                string filePath = savePath + Static.ConstructionCode + "\\" + fileName;

                var overwrite = false; //別の場所から同名のファイルを持ってきて保存する時は上書き確認する
                if (drawing.FullPath.ToUpper() != filePath.ToUpper() &&
                    drawing.FileName.ToUpper() == fileName.ToUpper())
                    overwrite = true;

                AutoCad.File.Save(filePath, overwrite);

                drawing.FullPath = filePath;
            }

            var groundFloor = drawings.Find(p => p.Floor == 1);
            if (groundFloor != null)
                WindowController2.BringDrawingToTop(groundFloor.WindowHandle);

            AutoCad.Db.Database.SetFileDialogMode(true);

            return drawings.Count;
        }

        public static void CloseAll()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();

            AutoCad.Db.Database.SetFileDialogMode(false);

            var drawings = Drawing.GetAll(true);
            drawings.AddRange(Drawing.GetAllForSocketPlan(null));

            foreach (var drawing in drawings)
            {
                WindowController2.BringDrawingToTop(drawing.WindowHandle);
                AutoCad.Command.Prepare();

                try
                {
                    while (true)
                    {
                        //closeコマンドはスクリプトを使わないと、保存を確かめるダイアログが出て処理が止まってしまう。
                        AutoCad.Command.SendLineEsc(@"script C:\UnitWiring\Scripts\Close.scr");

                        var currentHandle = WindowController2.GetTopDrawingHandle();

                        if (drawing.WindowHandle != currentHandle)
                            break;
                    }
                }
                catch
                { //取得に失敗したということは開いている図面が無いということなので抜けて良し
                }

            }

            AutoCad.Db.Database.SetFileDialogMode(true);
        }
        public static void CloseSelected(List<Drawing> drawings)
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();

            AutoCad.Db.Database.SetFileDialogMode(false);

            foreach (var drawing in drawings)
            {
                WindowController2.BringDrawingToTop(drawing.WindowHandle);
                AutoCad.Command.Prepare();

                try
                {
                    while (true)
                    {
                        //closeコマンドはスクリプトを使わないと、保存を確かめるダイアログが出て処理が止まってしまう。
                        AutoCad.Command.SendLineEsc(@"script C:\UnitWiring\Scripts\Close.scr");

                        var currentHandle = WindowController2.GetTopDrawingHandle();

                        if (drawing.WindowHandle != currentHandle)
                            break;
                    }
                }
                catch
                { //取得に失敗したということは開いている図面が無いということなので抜けて良し
                }

            }

            AutoCad.Db.Database.SetFileDialogMode(true);
        }

        public static void Bring1FDrawingToTop()
        {
            var drawings = Drawing.GetAll(false);
            var groundFloor = drawings.Find(p => p.Floor == 1);
            if (groundFloor != null)
                WindowController2.BringDrawingToTop(groundFloor.WindowHandle);
        }

        public static void Bring1FDrawingToTop(SocketPlanType type)
        {
            var drawings = Drawing.GetAll(false);

            Drawing groundFloor;
            if (type == SocketPlanType.Individual)
                groundFloor = drawings.Find(p => p.Floor == 1 && p.Name.Contains("Individual"));
            else if (type == SocketPlanType.Pattern)
                groundFloor = drawings.Find(p => p.Floor == 1 && p.Name.Contains("Pattern"));
            else
                throw new ApplicationException("Invalid socket plan type is selected.");

            if (groundFloor != null)
                WindowController2.BringDrawingToTop(groundFloor.WindowHandle);
        }

        #endregion
    }
}
