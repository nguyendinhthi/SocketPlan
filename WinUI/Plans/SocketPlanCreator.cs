using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.SocketPlanServiceReference;
using System.IO;
using SocketPlan.WinUI.Properties;
using System.Windows.Forms;

// SocketPlan
namespace SocketPlan.WinUI
{
    public enum SocketPlanDirection { RightUp, LeftUp, LeftDown, RightDown }

    public class SocketPlanCreator
    {
        private CadObjectContainer container;

        // SocketBoxの判定に使用
        private List<PartsColorEntry> roomColorEntries = new List<PartsColorEntry>();
        private bool isRound;
        private SocketPlanType SocketPlanType;
        private List<Drawing> drawings;
        private Dictionary<int, PointD> centerPoints = new Dictionary<int, PointD>();

        private bool isSubeteName;
        private bool isSubeteHotaru;
        private List<SocketBox> SocketBoxes = new List<SocketBox>();
        private List<SocketBox> specificateBoxes;
        private List<Symbol> denSymbols = new List<Symbol>();

        public SocketPlanCreator(SocketPlanType socketPlanType)
        {
            this.SocketPlanType = socketPlanType;
        }

        public void Run()
        {
            var progress = new ProgressManager();
            progress.Processes += new CadEventHandler(this.InitializeCad);
            progress.Processes += new CadEventHandler(this.CreateCopy);
            progress.Processes += new CadEventHandler(this.ReInitializeCad);
            progress.Processes += new CadEventHandler(this.DeleteOldSocketBox);
            progress.Processes += new CadEventHandler(this.LoadServerData);
            progress.Processes += new CadEventHandler(this.LoadCadObjectContainer);
            progress.Processes += new CadEventHandler(this.AdjustScale);
            progress.Processes += new CadEventHandler(this.CheckLuckingRooms);
            progress.Processes += new CadEventHandler(this.GetSocketBoxPatterns);
            progress.Processes += new CadEventHandler(this.CalculateBoxLocations);
            progress.Processes += new CadEventHandler(this.DrawSocketBoxes);
            progress.Processes += new CadEventHandler(this.RegisterToDb);
            progress.Processes += new CadEventHandler(this.FinalizeCad);
            progress.Run();
        }

        #region 前準備

        [ProgressMethod("Initializing CAD setting...")]
        private void InitializeCad()
        {
            AutoCad.PrepareAutoProcess();
        }

        [ProgressMethod("Copying DWG...")]
        private void CreateCopy()
        {
            //古い図面を閉じておく
            var oldDwgs = Drawing.GetAllForSocketPlan(null);
            if (oldDwgs.Count > 0)
                Drawing.CloseSelected(oldDwgs);
            
            var spDrawings = new List<Drawing>();
            foreach (var drawing in this.GetAllOriginalDrawings())
            {
                var copyName = UnitWiring.GetSocketPlanFileName(drawing.FileName, this.SocketPlanType);
                var directory = Path.Combine(Settings.Default.DrawingDirectory, Static.ConstructionCode);
                var originalPath = Path.Combine(directory, drawing.FileName);
                var copyPath = Path.Combine(directory, copyName);

                if (File.Exists(copyPath))
                    File.Delete(copyPath);

                File.Copy(originalPath, copyPath);

                var copy = Drawing.Create(copyPath, "Local");
                spDrawings.Add(copy);
            }

            var opener = new OpenDrawingForm();
            opener.OpenFilesForSocketPlan(spDrawings);
        }

        private List<Drawing> GetAllOriginalDrawings()
        {
            var drawings = Drawing.GetAll(false);
            //drawings.RemoveAll(p => p.FileName.Contains("Individual") || p.FileName.Contains("Pattern"));
            return drawings;
        }

        [ProgressMethod("Initializing CAD setting again...")]
        private void ReInitializeCad()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();
            AutoCad.PrepareAutoProcess();
        }

        [ProgressMethod("Removing old socket box...")]
        private void DeleteOldSocketBox()
        {
            using (var service = new SocketPlanService())
            {
                service.DeleteSocketBoxesAutoMatically(Static.ConstructionCode);
            }
        }

        [ProgressMethod("Loading Server data...")]
        private void LoadServerData()
        {
            using (var service = new SocketPlanServiceNoTimeout())
            {
                this.roomColorEntries.AddRange(service.GetPartsColorEntries(Static.ConstructionCode));

                var entries = service.GetShikakuTableEntries(Static.ConstructionCode);
                this.isRound = Array.Exists(entries, p => p.ItemId == Const.ShikakuItemId.SOCKETPLATE_ROUND && p.Value == "True");
            }

            using (var sevice = new SocketPlanService())
            {
                this.specificateBoxes = new List<SocketBox>(sevice.GetSocketBoxes(Static.ConstructionCode));
            }
        }

        [ProgressMethod("Loading CAD data...")]
        private void LoadCadObjectContainer()
        {
            this.drawings = Drawing.GetAllForSocketPlan(this.SocketPlanType);
            this.container = new CadObjectContainer(this.drawings,
                CadObjectTypes.RoomOutline |
                CadObjectTypes.Symbol |
                CadObjectTypes.Text |
                CadObjectTypes.Plate |
                CadObjectTypes.HouseOutline |
                CadObjectTypes.Clip
            );

            this.container.FillSymbolRooms();
            this.container.FillIsOutsideAndIsOutdoor();

            this.isSubeteHotaru = false;
            this.isSubeteName = false;

            if (this.container.Symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.全てﾈーﾑｽｲｯﾁ))
                this.isSubeteName = true;

            if (this.container.Symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.全てﾎﾀﾙ ||
                                                   p.Equipment.Name == Const.EquipmentName.全てﾎﾀﾙｽｲｯﾁ))
                this.isSubeteHotaru = true;

            if (this.container.Symbols.Exists(p => p.Equipment.Name == Const.EquipmentName.全てﾎﾀﾙﾈｰﾑｽｲｯﾁ ||
                                                   p.Equipment.Name == Const.EquipmentName.全てﾎﾀﾙｽｲｯﾁﾈｰﾑｽｲｯﾁ))
            {
                this.isSubeteName = true;
                this.isSubeteHotaru = true;
            }
        }

        [ProgressMethod("Adjusting scale...")]
        private void AdjustScale()
        {
            foreach (var drawing in this.drawings)
            {
                drawing.Focus();

                AutoCad.Command.ZoomAll();
                this.centerPoints.Add(drawing.Floor, AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout());
            }
        }

        [ProgressMethod("Checking rooms...")]
        private void CheckLuckingRooms()
        {
            var lucks = new List<PartsColorEntry>();
            foreach (var room in this.container.RoomOutlines)
            {
                if (this.roomColorEntries.Exists(p =>
                    p.Floor == room.Floor &&
                    p.RoomName == room.Name))
                    continue;

                var entry = new PartsColorEntry();
                entry.Floor = room.Floor;
                entry.RoomName = room.Name;

                lucks.Add(entry);
            }

            var dialog = new LuckingRoomColorEntryForm(lucks);
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                throw new ApplicationException("Socket plan function is cancelled.");

            this.roomColorEntries.AddRange(dialog.LuckEntries);
        }

        #endregion

        #region SocketBoxの判断

        [ProgressMethod("Checking socket box pattern...")]
        private void GetSocketBoxPatterns()
        {
            var symbols = this.container.Symbols;
            var usedSymbols = new List<Symbol>();

            foreach (var symbol in this.container.Symbols)
            {
                this.RestoreClipedSymbolInfo(symbol);
            }

            foreach (var symbol in this.container.Symbols)
            {
                if (usedSymbols.Contains(symbol))
                    continue;

                var group = new List<Symbol>();
                group.Add(symbol);

                if (!symbol.IsSingleSocketBox)
                    this.SetSameGroupSymbols(group, symbol);

                if (group.Count == 0)
                    continue;

                var targetPattern = this.findSocketBoxPattern(ref group);
                if (targetPattern == null)
                    continue;

                if (this.hasSetteigai(group))
                    continue;

                var box = this.CreateSocketBox(targetPattern, group[0]);
                this.SocketBoxes.Add(box);

                usedSymbols.AddRange(group);
            }
        }

        private SocketBoxPattern findSocketBoxPattern(ref List<Symbol> symbolGroup)
        {
            var matchedPatterns = new List<SocketBoxPattern>();
            foreach (var pattern in UnitWiring.Masters.SocketBoxPatterns)
            {
                if (!this.IsMatchedGroup(pattern, symbolGroup))
                    continue;

                matchedPatterns.Add(pattern);
            }

            symbolGroup = this.sortGroupSymbols(symbolGroup); //図面の通りに並べ変える

            if (matchedPatterns.Count == 1)
                return matchedPatterns[0];
            else
            {
                foreach (var pattern in matchedPatterns)
                {
                    if (this.IsMatchSymbolOrder(pattern, symbolGroup))
                        return pattern;
                }
            }
            return null;
        }

        private bool IsMatchSymbolOrder(SocketBoxPattern pattern, List<Symbol> group)
        {
            var details = pattern.DetailsList;
            if (details == null)
                return false;

            details.Sort((p, q) => p.Seq.CompareTo(q.Seq));
            details.Reverse(); //detailsの登録順は壁側が末尾に来る。

            if (details.Count != group.Count)
                return false;

            for (var i = 0; i < details.Count; i++)
            {
                if (details[i] == null || group[i] == null)
                    return false;

                if (details[i].Equipment.Id != group[i].Equipment.Id)
                    return false;
            }
            return true;
        }

        private List<Symbol> sortGroupSymbols(List<Symbol> symbols)
        {
            var result = new List<Symbol>();

            while (result.Count < symbols.Count)
            {
                Symbol target = new Symbol();
                if (result.Count == 0)
                    target = null;
                else
                    target = result[result.Count - 1];

                result.Add(findNextSymbol(target, symbols));
            }
            return result;
        }

        private Symbol findNextSymbol(Symbol targetSymbol, List<Symbol> symbols)
        {
            if (targetSymbol == null)
            {
                foreach (var symbol in symbols)
                {
                    if (!symbols.Exists(p => p.Contains(symbol.ClippedPosition) && p != symbol))
                        return symbol;
                }
            }
            else
            {
                foreach (var symbol in symbols)
                {
                    if (symbol == targetSymbol)
                        continue;
                    if (targetSymbol.Contains(symbol.ClippedPosition))
                        return symbol;
                }
            }
            return null;
        }

        ////グループ内に設定外品が含まれているか
        private bool hasSetteigai(List<Symbol> group)
        {
            foreach (var symbol in group)
            {
                if (symbol.OtherAttributes.Exists(p => p.Value.StartsWith("DEN-")))
                {
                    this.denSymbols.Add(symbol);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Symbolクラスにあるメソッドとは違い、シンボル同士でくっついてるものを取る</summary>
        private void SetSameGroupSymbols(List<Symbol> group, Symbol currentSymbol)
        {
            foreach (var symbol in this.container.Symbols)
            {
                if (group.Contains(symbol))
                    continue;

                if (currentSymbol.Floor == symbol.Floor &&
                    currentSymbol.Height == symbol.Height &&
                    currentSymbol.IsConnectedByActualPosition(symbol))
                {
                    group.Add(symbol);
                    this.SetSameGroupSymbols(group, symbol);
                }
            }
        }

        private void RestoreClipedSymbolInfo(Symbol symbol)
        {
            //クリップ内の先頭はパラメータを書き換えられているので復元する
            if (symbol.Clipped)
            {
                symbol.ObjectId = symbol.ClippedObjectId;
                symbol.Position = symbol.ClippedPosition;
                symbol.ActualPosition = symbol.ClippedPosition;
                symbol.Rotation = symbol.ClippedRotation;
                symbol.BlockName = symbol.ClippedBlockName;

                //座標がどうにもならないので図面から取り直す                
                var bounds = AutoCad.Db.BlockReference.GetBlockBound(symbol.ClippedObjectId);
                symbol.PositionBottomLeft = new PointD(bounds[0].X, bounds[0].Y);
                symbol.PositionTopRight = new PointD(bounds[1].X, bounds[1].Y);

                var blockId = AutoCad.Db.BlockReference.GetBlockId(symbol.ClippedObjectId);
                var refId = AutoCad.Db.BlockReference.Make(blockId, new PointD(0, 0));
                bounds = AutoCad.Db.BlockReference.GetBlockBound(refId);
                symbol.PointBottomLeft = bounds[0];
                symbol.PointTopRight = bounds[1];
                AutoCad.Db.BlockReference.Erase(refId);

                symbol.RealPointBottomLeft = symbol.PointBottomLeft.Clone();
                symbol.RealPointTopRight = symbol.PointTopRight.Clone();
            }
        }

        private bool IsMatchedGroup(SocketBoxPattern pattern, List<Symbol> group)
        {
            if (pattern.Details.Length != group.Count)
                return false;

            var alreadyMutched = new List<SocketBoxPatternDetail>();

            foreach (var symbol in group)
            {
                bool isFound = false;
                foreach (var detail in pattern.Details)
                {
                    if (alreadyMutched.Contains(detail))
                        continue;

                    if (symbol.Equipment.Name != detail.EquipmentName)
                        continue;

                    var comments = new List<string>();

                    Array.ForEach(detail.Comments, p => comments.Add(p.Comment.Text));
                    if (detail.Equipment != null)
                    {
                        foreach (var text in detail.Equipment.Texts)
                        {
                            if (text.Value.StartsWith("H=") || text.Value.StartsWith("DEN-"))
                                continue;

                            comments.Add(text.Value);
                        }
                    }

                    if (!this.IsMatchComment(comments, symbol))
                        continue;

                    isFound = true;
                    alreadyMutched.Add(detail);
                    break;
                }

                if (!isFound)
                    return false;
            }

            return true;
        }

        private bool IsMatchComment(List<string> comments, Symbol symbol)
        {
            var values = new List<string>();
            foreach (var attribute in symbol.Attributes)
            {
                if (attribute.Tag != Const.AttributeTag.OTHER)
                    continue;

                if (attribute.Value.StartsWith("H=") || attribute.Value.StartsWith("DEN-"))
                    continue;

                values.Add(attribute.Value);
            }

            // 全てﾈｰﾑ、全てﾎﾀﾙ等に対応(メソッド名わかりづらい、ごめん)
            if (symbol.IsSwitch && !this.IsNotNameHotaruSwitch(symbol))
            {
                if (this.isSubeteName && !values.Exists(p => p == Const.Text.ﾈｰﾑ))
                    values.Add(Const.Text.ﾈｰﾑ);

                if (this.isSubeteHotaru && !values.Exists(p => p == Const.Text.ﾎﾀﾙ))
                    values.Add(Const.Text.ﾎﾀﾙ);
            }

            if (comments.Count != values.Count)
                return false;

            foreach (var comment in comments)
            {
                string mutched = null;
                foreach (var value in values)
                {
                    if (comment == value)
                    {
                        mutched = value;
                        break;
                    }
                }

                if (mutched == null)
                    return false;

                values.Remove(mutched);
            }

            return true;
        }

        private bool IsNotNameHotaruSwitch(Symbol symbol)
        {
            if (!symbol.IsSwitch)
                return false;

            foreach (var attribute in symbol.Attributes)
            {
                if (UnitWiring.Masters.NotNameHotaruSwitchSerials.Exists(p =>
                    p.Hinban == attribute.Value))
                    return true;
            }

            if (UnitWiring.Masters.NotNameHotaruSwitches.Exists(p =>
                p.EquipmentName == symbol.Equipment.Name))
                return true;

            return false;
        }

        private SocketBox CreateSocketBox(SocketBoxPattern pattern, Symbol symbol)
        {
            var box = new SocketBox();
            box.ConstructionCode = Static.ConstructionCode;
            box.PlanNo = Static.Drawing.PlanNo;
            box.ZumenNo = Static.Drawing.RevisionNo;
            box.Floor = symbol.Floor;
            box.SetSeq = 1; //固定

            if (symbol.Room != null)
                box.RoomCode = symbol.Room.CodeInSiyo;

            box.RoomName = symbol.RoomName;
            box.Color = this.GetColorName(symbol);
            box.Shape = this.GetShapeName();
            box.PatternId = pattern.Id;
            box.Size = pattern.SocketBoxSize;
            box.Height = Convert.ToInt32(symbol.Height);
            box.Direction = symbol.GetRotate();
            box.DwgPath = this.GetDwgPath(pattern, symbol);
            box.SymbolEquipmentName = symbol.Equipment.Name;
            box.patternName = pattern.Name;

            if (string.IsNullOrEmpty(box.DwgPath) || !File.Exists(box.DwgPath))
                box.IsExistsDwg = false;
            else
                box.IsExistsDwg = true;

            if (symbol.Clipped)
                box.SymbolLocation = symbol.ClippedPosition;
            else
                box.SymbolLocation = symbol.Position;

            box.ActualSymbolLocation = symbol.ActualPosition;
            return box;
        }

        private string GetColorName(Symbol symbol)
        {
            PartsColorEntry partsColor;
            if (symbol.IsOutdoor)
                partsColor = this.roomColorEntries.Find(p => p.RoomName == Const.Room.外部);
            else
                partsColor = this.roomColorEntries.Find(p => p.Floor == symbol.Floor && p.RoomName == symbol.RoomName);

            if (partsColor == null)
                return string.Empty;

            if (partsColor.WallColorId == Const.PartsColor.White)
                return "White";
            else if (partsColor.WallColorId == Const.PartsColor.Beigu)
                return "Beigu";
            else
                return string.Empty;
        }

        private string GetShapeName()
        {
            return this.isRound ? "Round" : "Square";
        }

        private string GetDwgPath(SocketBoxPattern pattern, Symbol symbol)
        {
            if (symbol.IsOutdoor)
                return this.GetOutsideDwgPath(pattern, this.isRound);

            var room = this.roomColorEntries.Find(p => p.Floor == symbol.Floor && p.RoomName == symbol.RoomName);
            if (room == null)
                return string.Empty;

            if (this.SocketPlanType == SocketPlanType.Individual)
                return this.GetIndividualDwgPath(pattern, room.WallColorId, this.isRound);
            else if (this.SocketPlanType == SocketPlanType.Pattern)
                return this.GetPatternDwgPath(pattern, room.WallColorId, this.isRound);
            else
                throw new ApplicationException("Invalid Socket plan type.");
        }

        private string GetOutsideDwgPath(SocketBoxPattern pattern, bool isRound)
        {
            if (isRound)
                return pattern.IndividualWRDwgPath;
            else
                return pattern.IndividualWSDwgPath;
        }

        private string GetIndividualDwgPath(SocketBoxPattern pattern, int colorId, bool isRound)
        {
            if (colorId == Const.PartsColor.White)
            {
                if (isRound)
                    return pattern.IndividualWRDwgPath;
                else
                    return pattern.IndividualWSDwgPath;
            }
            else if (colorId == Const.PartsColor.Beigu)
            {
                if (isRound)
                    return pattern.IndividualBRDwgPath;
                else
                    return pattern.IndividualBSDwgPath;
            }
            else
            {
                return pattern.IndividualWRDwgPath;
            }
        }

        private string GetPatternDwgPath(SocketBoxPattern pattern, int colorId, bool isRound)
        {
            if (colorId == Const.PartsColor.White)
            {
                if (isRound)
                    return pattern.PatternWRDwgPath;
                else
                    return pattern.PatternWSDwgPath;
            }
            else if (colorId == Const.PartsColor.Beigu)
            {
                if (isRound)
                    return pattern.PatternBRDwgPath;
                else
                    return pattern.PatternBSDwgPath;
            }
            else
            {
                return pattern.PatternWRDwgPath;
            }
        }

        #endregion

        #region 位置計算

        [ProgressMethod("Calculating socket box location...")]
        private void CalculateBoxLocations()
        {
            this.SeparateBoxes();
            this.SetShownLocation();
        }

        private void SeparateBoxes()
        {
            foreach (var box in this.SocketBoxes)
            {
                var centerPoint = this.centerPoints[box.Floor];
                box.SPDirection = Utilities.GetSocketPlanDirection(box.SymbolLocation, centerPoint);
            }
        }

        private void SetShownLocation()
        {
            this.SetRightUpLocation();
            this.SetLeftUpLocation();
            this.SetLeftDownLocation();
            this.SetRightDownLocation();
            // TOTO これはなかなか難しい this.SetUpRightUpLocation();

            this.SocketBoxes.Clear();
            this.SocketBoxes.AddRange(this.RightUp);
            this.SocketBoxes.AddRange(this.LeftUp);
            this.SocketBoxes.AddRange(this.LeftDown);
            this.SocketBoxes.AddRange(this.RightDown);

            var seq = 1;
            this.SocketBoxes.ForEach(p => p.Seq = seq++);

        }

        private enum LimitType { Min, Max }

        private double GetHouseX(int floor, LimitType limit)
        {
            var vertexes = this.GetHouseVertexes(floor);

            if (limit == LimitType.Min)
                vertexes.Sort((p, q) => p.X.CompareTo(q.X));
            else if (limit == LimitType.Max)
                vertexes.Sort((p, q) => q.X.CompareTo(p.X));
            else
                throw new ApplicationException("Invalid limit type for house outline.");

            return vertexes[0].X;
        }

        private double GetHouseY(int floor, LimitType limit)
        {
            var vertexes = this.GetHouseVertexes(floor);

            if (limit == LimitType.Min)
                vertexes.Sort((p, q) => p.Y.CompareTo(q.Y));
            else if (limit == LimitType.Max)
                vertexes.Sort((p, q) => q.Y.CompareTo(p.Y));
            else
                throw new ApplicationException("Invalid limit type for house outline.");

            return vertexes[0].Y;
        }

        private List<PointD> GetHouseVertexes(int floor)
        {
            var lines = this.container.HouseOutlines.FindAll(p => p.Floor == floor);
            if (lines == null)
                throw new ApplicationException("Failed to get house outline.");

            var vertexes = new List<PointD>();
            foreach (var line in lines)
            {
                vertexes.AddRange(AutoCad.Db.Polyline.GetVertex(line.ObjectId));
            }

            if (vertexes.Count == 0)
                throw new ApplicationException("Failed to get vertexes of house outline.");

            return vertexes;
        }

        private List<SocketBox> RightUp = new List<SocketBox>();
        private void SetRightUpLocation()
        {
            this.RightUp = this.SocketBoxes.FindAll(p => p.SPDirection == SocketPlanDirection.RightUp);
            if (this.RightUp.Count == 0)
                return;

            this.RightUp.Sort((p, q) => p.SymbolLocation.Y.CompareTo(q.SymbolLocation.Y));

            foreach (var drawing in this.drawings)
            {
                drawing.Focus();

                var currents = this.RightUp.FindAll(p => p.Floor == drawing.Floor && p.IsExistsDwg);
                if (currents.Count == 0)
                    continue;

                var houseMaxX = this.GetHouseX(drawing.Floor, LimitType.Max);
                var lengthX = houseMaxX - centerPoints[drawing.Floor].X;
                var distanceX = lengthX / currents.Count;
                var x = houseMaxX + 8000;

                var houseMaxY = this.GetHouseY(drawing.Floor, LimitType.Max);
                var lengthY = houseMaxY - centerPoints[drawing.Floor].Y;
                var distanceY = lengthY / currents.Count;
                if (distanceY < 800)
                    distanceY = 800;

                var y = centerPoints[drawing.Floor].Y + distanceY;

                foreach (var current in currents)
                {
                    current.ShownLocation = new PointD(x, y);
                    x -= distanceX;
                    y += distanceY + 400;
                }
            }
        }

        private List<SocketBox> LeftUp = new List<SocketBox>();
        private void SetLeftUpLocation()
        {
            this.LeftUp = this.SocketBoxes.FindAll(p => p.SPDirection == SocketPlanDirection.LeftUp);
            if (this.LeftUp.Count == 0)
                return;

            this.LeftUp.Sort((p, q) => p.SymbolLocation.Y.CompareTo(q.SymbolLocation.Y));

            foreach (var drawing in drawings)
            {
                var currents = this.LeftUp.FindAll(p => p.Floor == drawing.Floor && p.IsExistsDwg);
                if (currents.Count == 0)
                    continue;

                var houseMinX = this.GetHouseX(drawing.Floor, LimitType.Min);
                var lengthX = centerPoints[drawing.Floor].X - houseMinX;
                var distanceX = lengthX / currents.Count;
                var x = houseMinX - 8000;

                var houseMaxY = this.GetHouseY(drawing.Floor, LimitType.Max);
                var lengthY = houseMaxY - centerPoints[drawing.Floor].Y;
                var distanceY = lengthY / currents.Count;
                if (distanceY < 800)
                    distanceY = 800;

                var y = centerPoints[drawing.Floor].Y + distanceY;

                foreach (var current in currents)
                {
                    current.ShownLocation = new PointD(x, y);
                    x += distanceX;
                    y += distanceY + 400;
                }
            }
        }

        private List<SocketBox> LeftDown = new List<SocketBox>();
        private void SetLeftDownLocation()
        {
            this.LeftDown = this.SocketBoxes.FindAll(p => p.SPDirection == SocketPlanDirection.LeftDown);
            if (this.LeftDown.Count == 0)
                return;

            this.LeftDown.Sort((p, q) => q.SymbolLocation.Y.CompareTo(p.SymbolLocation.Y));

            foreach (var drawing in drawings)
            {
                var currents = this.LeftDown.FindAll(p => p.Floor == drawing.Floor && p.IsExistsDwg);
                if (currents.Count == 0)
                    continue;

                var houseMinX = this.GetHouseX(drawing.Floor, LimitType.Min);
                var lengthX = centerPoints[drawing.Floor].X - houseMinX;
                var distanceX = lengthX / currents.Count;
                var x = houseMinX - 8000;

                var houseMinY = this.GetHouseY(drawing.Floor, LimitType.Min);
                var lengthY = centerPoints[drawing.Floor].Y - houseMinY;
                var distanceY = lengthY / currents.Count;
                if (distanceY < 800)
                    distanceY = 800;

                var y = centerPoints[drawing.Floor].Y - distanceY;

                foreach (var current in currents)
                {
                    current.ShownLocation = new PointD(x, y);
                    x += distanceX;
                    y -= distanceY + 400;
                }
            }
        }

        private List<SocketBox> RightDown = new List<SocketBox>();
        private void SetRightDownLocation()
        {
            this.RightDown = this.SocketBoxes.FindAll(p => p.SPDirection == SocketPlanDirection.RightDown);
            if (this.RightDown.Count == 0)
                return;

            this.RightDown.Sort((p, q) => q.SymbolLocation.Y.CompareTo(p.SymbolLocation.Y));

            foreach (var drawing in drawings)
            {
                var currents = this.RightDown.FindAll(p => p.Floor == drawing.Floor && p.IsExistsDwg);
                if (currents.Count == 0)
                    continue;

                var houseMaxX = this.GetHouseX(drawing.Floor, LimitType.Max);
                var lengthX = houseMaxX - centerPoints[drawing.Floor].X;
                var distanceX = lengthX / currents.Count;
                var x = houseMaxX + 8000;

                var houseMinY = this.GetHouseY(drawing.Floor, LimitType.Min);
                var lengthY = centerPoints[drawing.Floor].Y - houseMinY;
                var distanceY = lengthY / currents.Count;
                if (distanceY < 800)
                    distanceY = 800;

                var y = centerPoints[drawing.Floor].Y - distanceY;

                foreach (var current in currents)
                {
                    current.ShownLocation = new PointD(x, y);
                    x -= distanceX;
                    y -= distanceY + 400;
                }
            }
        }

        #endregion

        [ProgressMethod("Deleting cache...")]
        private void DeleteCache()
        {
            foreach (var drawing in this.drawings)
            {
                drawing.Focus();
                this.PurgeBlocks();
            }
        }

        #region 描画処理

        private void PurgeBlocks()
        {
            WindowController2.BringAutoCadToTop();
            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();
            AutoCad.Command.PurgeBlocks();
        }

        private List<SocketBox> errorBoxes = new List<SocketBox>();
        [ProgressMethod("Drawing socket box...")]
        private void DrawSocketBoxes()
        {
            bool hideButtonFlag = true;
            var container = new CadObjectContainer(this.drawings, 
                                                   CadObjectTypes.Symbol |
                                                   CadObjectTypes.RoomOutline |
                                                   CadObjectTypes.Text);
            foreach (var drawing in this.drawings)
            {
                drawing.Focus();

                var currentFloorBoxes = this.SocketBoxes.FindAll(p => p.Floor == drawing.Floor);
                var currentFloorSpecificates = this.specificateBoxes.FindAll(p => p.Floor == drawing.Floor);
                SocketPlanCreator.HideUnnecessaryItems(drawing, container, hideButtonFlag);

                AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_SocketPlan, CadColor.BlackWhite, Const.LineWeight._0_15);
                AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan);

                this.DrawSocketBoxes(currentFloorBoxes);
                this.DrawLeadLines(currentFloorBoxes);
                if (currentFloorSpecificates.Count != 0)
                {
                    foreach (var item in currentFloorSpecificates)
                    {
                        item.IsExistsDwg = true;
                        using (var service = new SocketPlanService())
                        {
                           var path = service.GetBlockPath(item.SpecificId.Value);
                           var getSymbolLocation = service.GetLocation(Static.ConstructionCode, item.SocketObjectId.Value);
                           item.ShownLocation = getSymbolLocation.SymbolLocation;
                           item.patternName = service.GetBlockPath(item.SpecificId.Value).Serial;
                           item.DwgPath = path.BlockPath;
                           if (getSymbolLocation.BoxLeftLocation.X != 0.0 && getSymbolLocation.BoxLeftLocation.Y != 0.0 &&
                               getSymbolLocation.BoxRightLocation.X != 0.0 && getSymbolLocation.BoxRightLocation.Y != 0.0)
                           {
                               item.BoxLeftLocation = getSymbolLocation.BoxLeftLocation;
                               item.BoxRightLocation = getSymbolLocation.BoxRightLocation;
                               item.BoxRightLocation.X += 1500;
                           }
                        }
                    }

                    currentFloorSpecificates.RemoveAll(p => p.BoxLeftLocation.X == 0.0 && p.BoxLeftLocation.Y == 0.0 &&
                                                            p.BoxRightLocation.X == 0.0 && p.BoxRightLocation.Y == 0.0);

                        this.DrawOldSocketBoxes(currentFloorSpecificates);
                        //this.DrawOldLeadLines(currentFloorSpecificates);
                    
                }
            }

            var errorText = string.Empty;
            if (this.errorBoxes.Count > 0)
            {
                errorText = "Failed to put below socket box DWG." + Environment.NewLine;
                foreach (var box in errorBoxes)
                {
                    errorText += box.Floor + "F : " + box.patternName + " " + box.SymbolLocation.ToString() + Environment.NewLine;
                }
                MessageDialog.ShowWarning(errorText);
            }

            if (this.SocketPlanType == SocketPlanType.Individual)
            {
                if (this.container.Symbols.Exists(p => (p.IsSwitch || p.IsOutlet) && p.OtherAttributes.Exists(q => q.Value.StartsWith("DEN-"))))
                {
                    errorText = @"There's a DEN comment in this plan.
Please encode socket plan material manually.";

                    MessageDialog.ShowWarning(errorText);
                }
            }
        }

        private Dictionary<string, int> nameCount = new Dictionary<string, int>();
        private int previousId = 0;
        private void DrawSocketBoxes(List<SocketBox> boxes)
        {
            foreach (var box in boxes)
            {
                if (!box.IsExistsDwg)
                {
                    this.errorBoxes.Add(box);
                    continue;
                }

                this.PurgeBlocks();

                try
                {
                    box.ObjectId = AutoCad.Db.BlockReference.Insert(box.DwgPath, box.ShownLocation);
                }
                catch (Exception ex) 
                {
                    throw new ApplicationException("The block file of this pattern is abnormal. please check block file.\n\nPattern name : " + box.patternName);
                }
                
                box.SocketObjectId = box.ObjectId;

                // Insertに失敗したときは１つ前のループのIDが取れて例外は出ない
                if (box.ObjectId == this.previousId)
                {
                    this.errorBoxes.Add(box);
                    continue;
                }

                AutoCad.Db.BlockReference.SetScaleFactor(box.ObjectId, SocketBoxObject.BOX_SCALE);

                // 左側に表示する場合は、Boxの長さ分だけさらに左にずらす
                if (box.SPDirection == SocketPlanDirection.LeftUp ||
                    box.SPDirection == SocketPlanDirection.LeftDown)
                {
                    var size = AutoCad.Db.BlockReference.GetSize(box.ObjectId);
                    box.ShownLocation = box.ShownLocation.MinusX(size.X * SocketBoxObject.BOX_SCALE);

                    AutoCad.Command.EraseLast();

                    box.ObjectId = AutoCad.Db.BlockReference.Insert(box.DwgPath, box.ShownLocation);
                    AutoCad.Db.BlockReference.SetScaleFactor(box.ObjectId, SocketBoxObject.BOX_SCALE);
                }

                //Seqを埋め込む
                var attId = Attribute.Make(box.ObjectId, "seq", box.Seq.ToString(), new PointD(0, 0), true);
                AutoCad.Db.Attribute.SetVisible(attId, false);

                // Insertに失敗したとき用コード
                this.previousId = box.ObjectId;

                // 同じ名前で置かれた場合対策でRenameしておく
                var i = 1;
                if (this.nameCount.ContainsKey(box.patternName))
                {
                    i = nameCount[box.patternName] + 1;
                    this.nameCount[box.patternName] = i;
                }
                else
                {
                    this.nameCount.Add(box.patternName, 1);
                }

                var boxName = AutoCad.Db.BlockReference.GetBlockName(box.ObjectId);
                var newName = box.patternName + "_" + i;
                Utilities.Rename(boxName, newName);
                this.RenameChildren(newName);
            }
        }

        private void DrawOldSocketBoxes(List<SocketBox> boxes)
        {
            var basePosition = new PointD();
            var beforeSeq = 0;

            foreach (var box in boxes)
            {
                if(box.Seq != beforeSeq)
                {
                    var familyBoxes = boxes.FindAll(p => p.Seq == box.Seq);
                    List<int> drawnIds = new List<int>();
                    foreach(var familyBox in familyBoxes)
                    {

                        if (!familyBox.IsExistsDwg)
                        {
                            this.errorBoxes.Add(familyBox);
                            continue;
                        }

                        this.PurgeBlocks();

                        try
                        {
                            if (familyBox.SetSeq == 1)
                            {
                                basePosition.X = familyBox.BoxLeftLocation.X;
                                basePosition.Y = familyBox.BoxLeftLocation.Y - 50;
                                AutoCad.Command.InsertBlock(familyBox.DwgPath, basePosition);
                                AutoCad.Status.WaitFinish();
                            }
                            else
                            {
                                basePosition.X = 1490*(familyBox.SetSeq-1)+familyBox.BoxLeftLocation.X;
                                basePosition.Y = familyBox.BoxLeftLocation.Y - 50;
                                AutoCad.Db.BlockReference.Insert(familyBox.DwgPath, basePosition);
                            }
                            var id = AutoCad.Selection.GetLastObjectId();
                            familyBox.SocketObjectId = id;

                            var attId = Attribute.Make((int)familyBox.SocketObjectId, "seq", familyBox.Seq.ToString(), new PointD(0, 0), true);
                            AutoCad.Db.Attribute.SetVisible(attId, false);
                            
                            drawnIds.Add((int)id);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("The block file of this pattern is abnormal. please check block file.\n\nPattern name : " + box.patternName);
                        }
                    }
                    //枠を置く
                    var rectId = AutoCad.Db.Polyline.MakeRectangle(box.BoxLeftLocation, box.BoxRightLocation);
                    AutoCad.Db.Entity.SetColor(rectId, CadColor.Orange);

                    //チェックボックス追加
                    var lineId = SymbolDrawer.DrawCheckBox(box.BoxLeftLocation, box.BoxRightLocation);
                    AutoCad.Db.Entity.SetColor(lineId, CadColor.Orange);

                    //グループ化
                    var groupingIds = new List<int>();
                    groupingIds.AddRange(drawnIds);
                    groupingIds.Add(rectId);
                    groupingIds.Add(lineId);
                    AutoCad.Db.Group.Make("SocketSpecific_" + DateTime.Now.ToString("yyyyMMddhhmmssfff"), groupingIds);
                    AutoCad.Status.WaitFinish();

                    //線を引く
                    AutoCad.Command.ZoomAll();
                    var center = AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout();
                    var direction = Utilities.GetSocketPlanDirection(basePosition, center);
                    var points = new List<PointD>();

                    if (direction == SocketPlanDirection.LeftDown ||
                        direction == SocketPlanDirection.LeftUp)
                        points.Add(new PointD(box.BoxRightLocation.X, box.BoxLeftLocation.Y));
                    else
                        points.Add(box.BoxLeftLocation);

                    points.Add(box.SymbolLocation);
                    points.Reverse();

                    int leaderId = AutoCad.Db.Leader.Make(points, null);
                    AutoCad.Db.Leader.SetColor(leaderId, CadColor.BlackWhite);
                    AutoCad.Db.Leader.SetLineWeight(leaderId, Const.LineWeight._0_15);

                    beforeSeq = box.Seq;
                }
            }
            this.SocketBoxes.AddRange(boxes);
        }

        /// <summary>Box名, 数</summary>
        private void RenameChildren(string boxName)
        {
            var childNo = 1;

            var ids = AutoCad.Db.BlockTable.GetIds();
            foreach (var id in ids)
            {
                var name = AutoCad.Db.BlockTableRecord.GetBlockName(id);
                if (!name.Contains("FcPack"))
                    continue;

                Utilities.Rename(name, boxName + "_" + childNo);
                childNo++;
            }
        }

        private void DrawLeadLines(List<SocketBox> boxes)
        {
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan);

            foreach (var box in boxes)
            {
                if (this.errorBoxes.Contains(box))
                    continue;

                var points = new List<PointD>();
                points.Add(box.SymbolLocation);

                var left = box.ShownLocation;

                Console.WriteLine(box.ObjectId);

                var size = AutoCad.Db.BlockReference.GetSize(box.ObjectId);
                var right = box.ShownLocation.PlusX(size.X);

                if (box.IsLeftSide)
                    points.Add(right);
                else
                    points.Add(left);

                int leaderId = AutoCad.Db.Leader.Make(points, null);
                AutoCad.Db.Leader.SetColor(leaderId, CadColor.BlackWhite);
                AutoCad.Db.Leader.SetLineWeight(leaderId, Const.LineWeight._0_00);
                AutoCad.Command.RefreshExEx();
            }
        }

        private void DrawOldLeadLines(List<SocketBox> boxes)
        {
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_SocketPlan);

            foreach (var box in boxes)
            {
                if (this.errorBoxes.Contains(box))
                    continue;

                var points = new List<PointD>();
                points.Add(box.SymbolLocation);

                var left = box.BoxLeftLocation;

                Console.WriteLine(box.ObjectId);

                var right = box.BoxRightLocation;

                if (box.IsLeftSide)
                    points.Add(right);
                else
                    points.Add(left);

                int leaderId = AutoCad.Db.Leader.Make(points, null);
                AutoCad.Db.Leader.SetColor(leaderId, CadColor.BlackWhite);
                AutoCad.Db.Leader.SetLineWeight(leaderId, Const.LineWeight._0_00);
                AutoCad.Command.RefreshExEx();
            }
        }

        #endregion

        [ProgressMethod("Registering socket plan data to DB...")]
        private void RegisterToDb()
        {
            using (var service = new SocketPlanService())
            {
                service.RegisterSocketBoxes(
                    Static.ConstructionCode,
                    Static.Drawing.PlanNo,
                    Static.Drawing.RevisionNo,
                    this.SocketBoxes.ToArray());
            }
        }

        [ProgressMethod("Finalizing...")]
        private void FinalizeCad()
        {
            foreach (var drawing in this.drawings)
            {
                drawing.Focus();
                AutoCad.Command.ZoomAll();
            }

            if (drawings.Count > 0)
                drawings[0].Focus();

            AutoCad.FinalizeAutoProcess();
        }

        /// <summary>不要アイテムの表示/非表示</summary>
        public static　void HideUnnecessaryItems(Drawing dwg, CadObjectContainer container, bool hideButton)
        {
            AutoCad.Command.SetCurrentLayoutToModel();
            dwg.Focus();
            AutoCad.Command.ZoomAll();
            var frames = container.Symbols.FindAll(p => p.Floor == dwg.Floor && p.BlockName.ToLower().StartsWith("frame"));

            //Leaderははみ出るので別に探す
            var leaderids = Filters.GetLeaderIds();

            var ignoreSymbols = container.IgnoreSymbols.FindAll(p => p.Floor == dwg.Floor);
            ignoreSymbols.ForEach(p => AutoCad.Db.BlockReference.Erase(p.ObjectId));

            var texts = container.Texts.FindAll(p => p.Floor == dwg.Floor);

            double searchRange = 300d;
            double margin = 10d;
            bool isHidden = false;
            foreach (var frame in frames)
            {
                var visible = AutoCad.Db.Entity.GetEntityVisible(frame.ObjectId);
                if (!visible && !hideButton)
                {
                    isHidden = true;
                    break;
                }

                double left = frame.PositionLeft.X - margin;
                double right = frame.PositionRight.X + margin;
                double top = frame.PositionTop.Y + margin;
                double bottom = frame.PositionBottom.Y - margin;

                var cmd = "HIDEOBJECT W " + left + "," + bottom + ",0 " + right + "," + top + ",0 ";
                AutoCad.Command.SendLine(cmd);

                foreach (var leaderId in leaderids)
                {
                    var layerName = AutoCad.Db.Leader.GetLayerName(leaderId);
                    if (layerName == Const.Layer.電気_SocketPlan || layerName == Const.Layer.電気_SocketPlan_Specific)
                        continue;
                    List<PointD> points = new List<PointD>();

                    if (layerName == Const.Layer.電気_電気図面配線)
                        continue;

                    
                    points.Add(AutoCad.Db.Leader.GetEndPoint(leaderId));
                    points.Add(AutoCad.Db.Leader.GetStartPoint(leaderId));

                    foreach (var p in points) 
                    {
                        var isX = left < p.X && p.X < right;
                        var isY = top > p.Y && p.Y > bottom;
                        if (isX && isY)
                        {
                            AutoCad.Db.Entity.SetVisible(leaderId, false);
                            break;
                        }
                    }
                }
                
                var textBottom = frame.PositionTop.Y - margin;
                var textTop = textBottom + searchRange;
                foreach (var text in texts)
                {
                    var textLayerName = AutoCad.Db.Entity.GetLayerName(text.ObjectId);
                    if (textLayerName == Const.Layer._2_4_雑仕上 ||
                        textLayerName == Const.Layer._2_8_雑線 ||
                        textLayerName == Const.Layer._2_E_建具 ||
                        textLayerName == Const.Layer._2_E_文字)
                        continue;

                    var isX = left < text.Position.X && text.Position.X < right;
                    var isY = textTop > text.Position.Y && text.Position.Y > textBottom;
                    if (isX && isY)
                    {
                        AutoCad.Db.Entity.SetVisible(text.ObjectId, false);
                    }
                }
            }

            //レイヤーの消去
            AutoCad.Command.SetCurrentLayer(Const.Layer.Zero);
            var layerIds = AutoCad.Db.LayerTable.GetLayerIds();
            foreach (var layerId in layerIds)
            {
                var layerName = AutoCad.Db.LayerTableRecord.GetLayerName(layerId);
                if (layerName == Const.Layer.電気_DistancePattern ||
                    layerName == Const.Layer.電気_部屋 ||
                    layerName == Const.Layer.電気_部屋_WithJyou ||
                    layerName == Const.Layer.電気_部屋_WithoutJyou ||
                    layerName == Const.Layer.電気_外周)
                {

                    if (!isHidden)
                        AutoCad.Db.LayerTableRecord.SetFrozen(layerId, true);
                    else
                        AutoCad.Db.LayerTableRecord.SetFrozen(layerId, false);
                }
            }

            if (isHidden)
            {
                AutoCad.Command.SendLineEsc("UNISOLATE");
                leaderids.ForEach(p => AutoCad.Db.Entity.SetVisible(p, true));
                foreach(var text in texts)
                    AutoCad.Db.Entity.SetVisible(text.ObjectId, true);

            }

            AutoCad.Command.RefreshExEx();
        }

    }
}
