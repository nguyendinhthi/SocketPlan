using System.Collections.Generic;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.Entities.CADEntity;

//// ■■■初めて触る人は必ず読むこと■■■
//// ■エラーメッセージのフォーマットは以下のように揃える。既存メッセージを参考にすること。
//// 英語メッセージ
//// 日本語メッセージ
//// 空行
//// === Information ===
//// プロパティ名 : プロパティ値
//// 以下繰り返し

//// ■メッセージを切り出す理由
//// メッセージが重複することは別にかまわない。どうでもいい。
//// ここにメッセージをまとめることで、他のメッセージを自然と参考にする。
//// それによって、上記のフォーマットが守られる。そこに価値がある。

namespace SocketPlan.WinUI
{
    public class Messages
    {
        public static string Quit()
        {
            var message = new Message();
            message.EnglishText = "Quit?";
            message.JapaneseText = "終了してもよろしいですか？";

            return message.ToString();
        }

        /// <summary>あり得ない分岐に行った時のメッセージ</summary>
        public static string UnexpectedProcessCalled(string methodName)
        {
            var message = new Message();
            message.EnglishText = "Unexpected process was called. Please contact system administrator.";
            message.JapaneseText = "予期せぬ処理が実行されました。システム担当にお知らせください。";
            message.AddInfo("FunctionName", methodName);

            return message.ToString();
        }

        public static string PleaseSelect1Block()
        {
            var message = new Message();
            message.EnglishText = "Please select just a block. Then, press the button.";
            message.JapaneseText = "ブロックを1つだけ選択してから属性編集ボタンを押してください。";

            return message.ToString();
        }

        public static string PleaseSelect2Block()
        {
            var message = new Message();
            message.EnglishText = "Please select just 2 block. Then, press the button.";
            message.JapaneseText = "ブロックを2つだけ選択してからボタンを押してください。";

            return message.ToString();
        }

        public static string PleaseSelect1Text()
        {
            var message = new Message();
            message.EnglishText = "Please select just a text.";
            message.JapaneseText = "テキストを1つだけ選択してください。";

            return message.ToString();
        }

        public static string PleaseSelect2Lines()
        {
            var message = new Message();
            message.EnglishText = "Please select a vertical grid line and a horizontal one.";
            message.JapaneseText = "基点とするグリッド線を2つ選択してください。";

            return message.ToString();
        }

        public static string PleaseRoomNameText()
        {
            var message = new Message();
            message.EnglishText = "Please select room name text(s).";
            message.JapaneseText = "部屋名を選択してください。";

            return message.ToString();
        }

        public static string UnregisteredSymbol()
        {
            var message = new Message();
            message.EnglishText = "The symbol is not registered in the master.";
            message.JapaneseText = "マスタに登録されていないシンボルです。";

            return message.ToString();
        }

        public static string NotFoundServerDrawingDirectory(string constructionCode)
        {
            var message = new Message();
            message.EnglishText = "Not found drawing.";
            message.JapaneseText = "指定したお客様コードの図面が見つかりませんでした。";
            message.AddInfo("Construction Code", constructionCode);

            return message.ToString();
        }

        public static string FileOpened(int fileCount)
        {
            var message = new Message();
            message.EnglishText = fileCount + " drawings opened.";
            message.JapaneseText = fileCount + "枚の図面を開きました。";

            message.JapaneseText += "\n\n===========================================";
            message.JapaneseText += "\nIf AutoCAD 2013 is jerky, please press Ctrl+9 to close command line.";
            message.JapaneseText += "\n===========================================";

            return message.ToString();
        }

        public static string FileAlreadyOpened()
        {
            var message = new Message();
            message.EnglishText = "The drawings are already opened.";
            message.JapaneseText = "指定した図面は既に開かれています。";

            message.JapaneseText += "\n\n===========================================";
            message.JapaneseText += "\nIf AutoCAD 2013 is jerky, please press Ctrl+9 to close command line.";
            message.JapaneseText += "\n===========================================";

            return message.ToString();
        }

        public static string FileSaved(int fileCount)
        {
            var message = new Message();
            message.EnglishText = fileCount + " drawings saved.";
            message.JapaneseText = fileCount + "枚の図面を保存しました。";

            return message.ToString();
        }

        public static string Finished()
        {
            var message = new Message();
            message.EnglishText = "Finished!";
            message.JapaneseText = "完了しました!";

            return message.ToString();
        }

        public static string FileClosing()
        {
            var message = new Message();
            message.EnglishText = "Unsaved changes will be lost. Do you want to save?";
            message.JapaneseText = "変更を保存しますか？";

            return message.ToString();
        }

        public static string InvalidFloorArrow2(Symbol floorArrow)
        {
            var message = new Message();
            message.EnglishText = "Please redraw an arrow symbol.";
            message.AddInfo(floorArrow);

            return message.ToString();
        }

        public static string InvalidFloorArrow(Symbol floorArrow)
        {
            var message = new Message();
            message.EnglishText = "Please connect a wire to an arrow symbol.";
            message.JapaneseText = "別階への矢印シンボルが、配線に繋がっていないか、複数の配線に繋がっています。";
            message.AddInfo(floorArrow);

            return message.ToString();
        }

        public static string LinkCodeDupulicate(string linkCode, List<Symbol> arrows)
        {
            var message = new Message();
            message.EnglishText = "LinkCode of wire is duplicated.";
            message.JapaneseText = "LinkCodeのペアが重複しています。";
            message.AddInfo("LinkCode", linkCode);
            arrows.ForEach(p => message.AddInfo(p));

            return message.ToString();
        }

        public static string NotPairFloorArrow(string linkCode, List<Symbol> arrows)
        {
            var message = new Message();
            message.EnglishText = "Please make an arrow symbol to pair with the following symbol.";
            message.JapaneseText = "別階への矢印シンボルは、WireLinkCodeを揃えてペアで使ってください。";
            message.AddInfo("LinkCode", linkCode);
            arrows.ForEach(p => message.AddInfo(p));

            return message.ToString();
        }

        public static string FailedToGetMasterVersion(string masterVersionPath)
        {
            var message = new Message();
            message.EnglishText = "Failed to get master version.";
            message.JapaneseText = "マスタバージョンを取得できませんでした。";
            message.AddInfo("MasterVersionFilePath", masterVersionPath);

            return message.ToString();
        }

        public static string UnconnectedWiresFoundNotGenkan(List<Wire> wires)
        {
            var message = new Message();
            message.EnglishText = "Unconnected wire found.(not in 玄関 or トイレ or IUB)";
            message.JapaneseText = "シンボルに繋がっていない配線があります。";
            message.AddInfo("---Wire---");
            message.AddInfo("Floor / StartPoint / EndPoint");

            foreach (var wire in wires)
            {
                var infoWires = wire.GetAllChildrenWire();
                foreach (var infoWire in infoWires)
                {
                    message.AddInfo(infoWire.Floor.ToString() + " / " + infoWire.StartPoint.ToString() + "  / " + infoWire.EndPoint.ToString());
                }
            }
            return message.ToString();
        }

        public static string UnconnectedWiresFoundGenkan(List<Wire> wires)
        {
            var message = new Message();
            message.EnglishText = "Unconnected wire found.(in 玄関 or トイレ or IUB)";
            message.JapaneseText = "シンボルに繋がっていない配線があります。";
            message.AddInfo("---Wire---");
            message.AddInfo("Floor / StartPoint / EndPoint");

            foreach (var wire in wires)
            {
                var infoWires = wire.GetAllChildrenWire();
                foreach (var infoWire in infoWires)
                {
                    message.AddInfo(infoWire.Floor.ToString() + " / " + infoWire.StartPoint.ToString() + "  / " + infoWire.EndPoint.ToString());
                }
            }
            return message.ToString();
        }

        public static string NotFoundBreaker()
        {
            var message = new Message();
            message.EnglishText = "Not found any breaker.";
            message.JapaneseText = "分電盤が見つかりませんでした。";

            return message.ToString();
        }

        public static string NotFoundCeilingPanel()
        {
            var message = new Message();
            message.EnglishText = "Not found any ceiling panel.";
            message.JapaneseText = "天井パネルが見つかりませんでした。";

            return message.ToString();
        }

        public static string NotFoundRoomOutline()
        {
            var message = new Message();
            message.EnglishText = "Not found any room outline.";
            message.JapaneseText = "部屋外周線が見つかりませんでした。";

            return message.ToString();
        }

        public static string NotFoundHouseOutline()
        {
            var message = new Message();
            message.EnglishText = "Not found any house outline in all floor plan.";
            message.JapaneseText = "全ての階の家外周線が見つかりませんでした。";

            return message.ToString();
        }

        public static string UnexpectedSymbol(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "A wire is connected to unexpected symbol.";
            message.JapaneseText = "予期しないシンボルに配線が接続されています。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NotFoundViewport()
        {
            var message = new Message();
            message.EnglishText = "Not found viewport.";
            message.JapaneseText = "ビューポートが見つかりませんでした。";

            return message.ToString();
        }

        public static string Invalid4WaySwitch()
        {
            var message = new Message();
            message.EnglishText = "Invalid 4 way switch.";
            message.JapaneseText = "4路スイッチの配線組み合わせが不正です。";

            return message.ToString();
        }

        public static string Invalid3Wayto4WaySwitch()
        {
            var message = new Message();
            message.EnglishText = "Wrong switch use, more than two 3way or five 4way Is connected to light.";
            message.JapaneseText = "3路および4路スイッチの配線組み合わせが不正です。";

            return message.ToString();
        }

        public static string PleaseLaunchAutoCAD()
        {
            var message = new Message();
            message.EnglishText = "Please launch AutoCAD.";
            message.JapaneseText = "AutoCADを起動してください。";

            return message.ToString();
        }

        public static string CannotOpenFileWithSpace()
        {
            var message = new Message();
            message.EnglishText = "Cannot open file. Because file name has space.";
            message.JapaneseText = "ファイル名にスペースが含まれているため、図面を開くことができません。";

            return message.ToString();
        }

        public static string HeightNotSet(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Symbol height is not set. Please set height to following symbol.";
            message.JapaneseText = "シンボルに高さが設定されていません。Height属性に「H=123」の形式で高さを設定してください。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string HeightNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set the height info.\n\n◆◆◆Please remove and redraw the last symbol.◆◆◆\n";
            message.JapaneseText = "シンボルに高さ情報を設定することができませんでした。\n恐れ入りますが、最後に置いたシンボルを削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string UnderfloorNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set the underfloor info.\n\n◆◆◆Please remove and redraw the last line.◆◆◆\n";
            message.JapaneseText = "配線に床下情報を設定することができませんでした。\n恐れ入りますが、最後に置いた配線を削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string ArrowHeadNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set the floor info.\n\n◆◆◆Please remove and redraw the last arrow head.◆◆◆\n";
            message.JapaneseText = "矢印に階情報を設定することができませんでした。\n恐れ入りますが、最後に置いた矢印を削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string ClipNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set the clip info.\n\n◆◆◆Please remove and redraw the last clip symbol.◆◆◆\n";
            message.JapaneseText = "抜き出しシンボルの情報を取得できませんでした。\n恐れ入りますが、最後に置いた抜き出しシンボルを削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string RoomNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set room info.\n\n◆◆◆Please remove and redraw the last room line.◆◆◆\n";
            message.JapaneseText = "線に部屋情報を設定することができませんでした。\n恐れ入りますが、最後に置いた部屋枠を削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string CeilingPanelNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set ceiling panel info.\n\n◆◆◆Please remove and redraw the last ceiling panel line.◆◆◆\n";
            message.JapaneseText = "線に天井パネル情報を設定することができませんでした。\n恐れ入りますが、最後に置いた天井パネルを削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string CeilingReceiverNotSet()
        {
            var message = new Message();
            message.EnglishText = "Failed to set ceiling receiver info.\n\n◆◆◆Please remove and redraw the last ceiling receiver line.◆◆◆\n";
            message.JapaneseText = "線に天井受け情報を設定することができませんでした。\n恐れ入りますが、最後に置いた天井パネル受けを削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string FailedToGetPoint()
        {
            var message = new Message();
            message.EnglishText = "Failed to get selected point.\n\n◆◆◆Please retry.◆◆◆\n";
            message.JapaneseText = "どちら側を選択したか判断できませんでした。\n恐れ入りますが、再度実行して下さい。";

            return message.ToString();
        }

        public static string FailedToGetDrawings()
        {
            var message = new Message();
            message.EnglishText = "Failed to get drawings.";
            message.JapaneseText = "図面が見つかりませんでした。";

            return message.ToString();
        }

        public static string WireIndicationPlanNotSelected()
        {
            var message = new Message();
            message.EnglishText = "Please select any of the wire indication plan.";
            message.JapaneseText = "パネル別配線取付指示書を選択して下さい。";

            return message.ToString();
        }

        public static string FailedToGetProposalDenkiScale()
        {
            var message = new Message();
            message.EnglishText = "Failed to get Proposal Denki Plan Scale.";
            message.JapaneseText = "提案電気図面のスケールの取得に失敗しました。";

            return message.ToString();
        }

        public static string NotFoundColor(int colorId)
        {
            var message = new Message();
            message.EnglishText = "Not found color.";
            message.JapaneseText = "色が定義されていません。";
            message.AddInfo("Color Code", colorId.ToString());

            return message.ToString();
        }

        public static string FailedAbnormalConnection()
        {
            var message = new Message();
            message.EnglishText = "There is an abnormal connection pattern.";
            message.JapaneseText = "異常な接続パターンがあります。";

            return message.ToString();
        }

        public static string NotSetSequenceNo(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Wire number is not set. Please use the function to draw number.";
            message.JapaneseText = "シンボルに配線番号が設定されていません。配線番号自動作図機能を先に実行してください。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string InvalidRoomline(Symbol symbol, PointD startPoint, PointD endPoint)
        {
            var message = new Message();
            message.EnglishText = "Failed to get room name. Please redraw room outline surrounding the following symbol.";
            message.JapaneseText = "部屋名を取得できませんでした。\n部屋外周線への部屋名設定に失敗しています。\n大変恐れ入りますが、下記シンボルを配置している部屋外周線を削除・再作図して下さい。";
            message.AddInfo(symbol);
            message.AddInfo(startPoint, endPoint);

            return message.ToString();
        }

        public static string CannotCountPowerConditioner()
        {
            var message = new Message();
            message.EnglishText = "Cannot count power conditioners at Shiyousho system.";
            message.JapaneseText = "仕様書からパワコンの台数を取得できませんでした。";

            return message.ToString();
        }

        public static string NotFoundEquipmentDefinition(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Not found equipment definition.";
            message.JapaneseText = "以下のシンボルの設備定義が見つかりませんでした。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NotFoundChildSymbol(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Breaker or joint box have no branched wire.";
            message.JapaneseText = "分電盤orジョイントボックスの下に配線が見つかりませんでした。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NotFoundChildSymbol(List<Symbol> symbols)
        {
            var message = new Message();
            message.EnglishText = "Symbol has no branched wire.";
            message.JapaneseText = "シンボルの下に配線が見つかりませんでした。";
            symbols.ForEach(p => message.AddInfo(p));

            return message.ToString();
        }

        public static string NotFoundConnectorCheckBox()
        {
            var message = new Message();
            message.EnglishText = "Not found any connector checkbox.";
            message.JapaneseText = "コネクターのチェックボックスが見つかりませんでした。";

            return message.ToString();
        }

        public static string ComExceptionOccur(string detail)
        {
            var message = new Message();
            message.EnglishText = "A connection with the AutoCAD was lost. Please retry.";
            message.JapaneseText = "AutoCADとの接続が切れました。再接続しましたので、もう一度実行してください。";
            message.AddInfo(detail);

            return message.ToString();
        }

        public static string WireNotOnCeilingPanel(Wire wire)
        {
            var message = new Message();
            message.EnglishText = "This wire is not passing on the ceiling panel.";
            message.JapaneseText = "配線が天井パネル上を通っていません。";
            message.AddInfo(wire);
            return message.ToString();
        }

        public static string WireNotOnCeilingPanel(List<Wire> wires)
        {
            var message = new Message();
            message.EnglishText = "This wire is not passing on the ceiling panel.";
            message.JapaneseText = "配線が天井パネル上を通っていません。";
            wires.ForEach(a => message.AddInfo(a));
            return message.ToString();

        }

        public static string WireNotOnCeilingPanel(List<Symbol> symbols)
        {
            var message = new Message();
            message.EnglishText = "This wire is not passing on the ceiling panel.";
            message.JapaneseText = "配線が天井パネル上を通っていません。";
            symbols.ForEach(p => message.AddInfo(p.Wire));

            return message.ToString();
        }

        public static string DuplicatedSymbolFound(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Duplicated symbols found.";
            message.JapaneseText = "同じ位置に同じ部材があります。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string DuplicatedSymbolComment(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Duplicated symbol's comment found.";
            message.JapaneseText = "同じコメントが設定されている部材があります。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string OverVALimit(List<Symbol> symbols, int limit)
        {
            var message = new Message();
            message.EnglishText = "Ordinary kairo is over " + limit + "VA.";
            message.JapaneseText = limit + "VAを超えている一般回路があります。";
            symbols.ForEach(symbol => message.AddInfo("CircuitNo", symbol.SequenceNo.ToString()));

            return message.ToString();
        }

        public static string NotFoundBlock(string blockPath)
        {
            var message = new Message();
            message.EnglishText = "Not found block.";
            message.JapaneseText = "ブロックが見つかりませんでした。";
            message.AddInfo("BlockPath", blockPath);

            return message.ToString();
        }

        public static string ComOkashii()
        {
            var message = new Message();
            message.EnglishText = "Cannot load LT VB-COM normally.\nI'm sorry but please restart AutoCAD and UnitWiringSystem.";
            message.JapaneseText = "LT VB-COMを正常に読み取れませんでした。\n大変恐れ入りますが、AutoCADとUnitWiringシステムを再起動してください。";

            return message.ToString();
        }

        public static string InvalidKanabakari(string kanabakari)
        {
            var message = new Message();
            message.EnglishText = "Invalid kanabakari.";
            message.JapaneseText = "矩計の値が不正です。";
            message.AddInfo("Kanabakari", kanabakari);

            return message.ToString();
        }

        public static string SelectPartsColor(string roomName)
        {
            var message = new Message();
            message.EnglishText = "Please select parts color.";
            message.JapaneseText = "電気部品色を選択してください。";
            message.AddInfo("RoomName", roomName);

            return message.ToString();
        }

        public static string SelectJboxColor(string roomName)
        {
            var message = new Message();
            message.EnglishText = "Please select jbox color.";
            message.JapaneseText = "JBOXの色を選択してください。";
            message.AddInfo("RoomName", roomName);

            return message.ToString();
        }

        public static string InvalidElectricContract(string contractCode)
        {
            var message = new Message();
            message.EnglishText = "Please entry correct electric contract code.";
            message.JapaneseText = "正しい電力契約コードを入力してください。";
            message.AddInfo("Electric Contract", contractCode);

            return message.ToString();
        }

        public static string LostComboItem(string itemName, string value)
        {
            var message = new Message();
            message.EnglishText = "The following entry was cleared. The entry is not in the options.";
            message.JapaneseText = "以前入力したデータが選択リスト内に見つからなかった為、下記の入力がクリアされました。";
            message.AddInfo("Item", itemName);
            message.AddInfo("Value", value);

            return message.ToString();
        }

        public static string BreakerNotOnCeilingPanel()
        {
            var message = new Message();
            message.EnglishText = "Bundenban is not on the ceiling panel.\nPlease check the position of the ceiling panel.";
            message.JapaneseText = "分電盤が天井パネル上にありません。\n天井パネルの位置が正しいか確認してください。";

            return message.ToString();
        }

        public static string DeleteConfirm(string target)
        {
            var message = new Message();
            message.EnglishText = "Are you sure you want to delete " + target + " ?";
            message.JapaneseText = target + "を削除してもよろしいですか？";

            return message.ToString();
        }

        public static string EditClosing()
        {
            var message = new Message();
            message.EnglishText = "Unsaved changes will be lost. Do you want to continue?";
            message.JapaneseText = "保存していない変更は破棄されます。続行しますか？";

            return message.ToString();
        }

        public static string WithExplode()
        {
            var message = new Message();
            message.EnglishText = "Cannot change double outlets item.(e.g. 家電収納)\nPlease request to change it to system administrator.";
            message.JapaneseText = "\nコンセントを複数含むシンボルは、特別な設定が必要な為、\nこの画面で変更することができません。\n変更したい場合はシステム担当にご依頼下さい。";

            return message.ToString();
        }

        public static string PleaseDeleteEquipFirst()
        {
            var message = new Message();
            message.EnglishText = "Please delete all symbols in this category first.";
            message.JapaneseText = "カテゴリに属するシンボルを先に全て削除して下さい。";

            return message.ToString();
        }

        public static string AlreadyRegistered()
        {
            var message = new Message();
            message.EnglishText = "Already registered.";
            message.JapaneseText = "登録済みです。";

            return message.ToString();
        }

        public static string TooManyComment()
        {
            var message = new Message();
            message.EnglishText = "Too many comments. maximum 5 comments.";
            message.JapaneseText = "コメントは5つまでしか登録できません。";

            return message.ToString();
        }

        public static string InvalidImagePath()
        {
            var message = new Message();
            message.EnglishText = "Please put the image file in 'Images' folder.";
            message.JapaneseText = "画像ファイルはImagesフォルダ内に置いてください";
            message.AddInfo("'Images' folder", Properties.Settings.Default.ImageDirectory);

            return message.ToString();
        }

        public static string InvalidBlockPath()
        {
            var message = new Message();
            message.EnglishText = "Please put the dwg file in 'Blocks' folder.";
            message.JapaneseText = "DWGファイルはBlocksフォルダ内に置いてください";
            message.AddInfo("'Blocks' folder", Properties.Settings.Default.BlockDirectory);

            return message.ToString();
        }

        public static string IrregularFileName()
        {
            var message = new Message();
            message.EnglishText = @"The this file name cannot be used.
The blank is included in the end of the file name.
Please correct Name of the file.";
            message.JapaneseText = @"ファイル名の末尾にスペースが存在します。
            ファイル名を修正してください。";
            message.AddInfo("'Blocks' folder", Properties.Settings.Default.BlockDirectory);

            return message.ToString();
        }

        public static string PleaseSelect2OrMoreBlock()
        {
            var message = new Message();
            message.EnglishText = "Please select 2 or more symbols.";
            message.JapaneseText = "シンボルを2つ以上選択してください。";

            return message.ToString();
        }

        public static string InvalidEllipse()
        {
            var message = new Message();
            message.EnglishText = "Please select a plate by function 2-9.";
            message.JapaneseText = "機能2-9で描いたプレートを選択してください。";

            return message.ToString();
        }

        public static string PleaseSelectJoIndication()
        {
            var message = new Message();
            message.EnglishText = "Please select 帖 indication to read outline.";
            message.JapaneseText = "帖を選択してください。";

            return message.ToString();
        }

        public static string ShouldHaveJyou()
        {
            var message = new Message();
            message.EnglishText = "This room is supposed to have jyou indication.";
            message.JapaneseText = "選択した部屋は帖を指定することになっています。";

            return message.ToString();
        }

        public static string InvalidJyou(string jyou)
        {
            var message = new Message();
            message.EnglishText = "Invalid Jyou.";
            message.JapaneseText = "部屋に割り当てられた帖データが不正です。";
            message.AddInfo("Jyou", jyou);

            return message.ToString();
        }

        public static string InvalidValue()
        {
            var message = new Message();
            message.EnglishText = "Invalid value.";
            message.JapaneseText = "論理的に値が不正です。";

            return message.ToString();
        }

        public static string DuplicatedData()
        {
            var message = new Message();
            message.EnglishText = "Duplicated data.";
            message.JapaneseText = "データが重複しています。";

            return message.ToString();
        }

        public static string EmptyValue()
        {
            var message = new Message();
            message.EnglishText = "There is empty cell.";
            message.JapaneseText = "空の項目があります。";

            return message.ToString();
        }

        public static string NotFoundSankouYouryou()
        {
            var message = new Message();
            message.EnglishText = "Not found 参考容量.";
            message.JapaneseText = "総VAに対応する参考容量が見つかりませんでした。";

            return message.ToString();
        }

        public static string InvalidCell()
        {
            var message = new Message();
            message.EnglishText = "There is invalid value.";
            message.JapaneseText = "値が不正な項目があります。";

            return message.ToString();
        }

        public static string SamePrimaryKey(ProductVa va)
        {
            var message = new Message();
            message.EnglishText = "There is same value data.";
            message.JapaneseText = "同じ値のデータが既にあります。";
            message.AddInfo("Class1Code", va.Class1Code);
            message.AddInfo("Class2Code", va.Class2Code);
            message.AddInfo("ProductCode", va.ProductCode);
            message.AddInfo("SeqNo", va.SeqNo.ToString());

            return message.ToString();
        }

        public static string PleaseSelect1BlockAnd1Wire()
        {
            var message = new Message();
            message.EnglishText = "Please select 1 block and 1 wire.";
            message.JapaneseText = "ブロック1つと配線1つを選択して下さい。";

            return message.ToString();
        }

        public static string LostRelationOfTakeOutArrow(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Toridashi symbol's relation was lost.\nPlease redraw toridashi symbol.\nSorry for the inconvenience.";
            message.JapaneseText = "配線取出シンボル(矢印)に対応するシンボルを見つけられませんでした。\n恐れ入りますが、配線取出シンボルを再度配置して下さい。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string InvalidSmartSeries(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Cannot consider as smart series item. Need to check type of house.";
            message.JapaneseText = "i-smart以外では、防水コンセント以外にスマートシリーズ設定をすることはできません。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string InvalidRoomline(PointD startPoint, PointD endPoint)
        {
            var message = new Message();
            message.EnglishText = "Failed to get room name. Please redraw room outline.";
            message.JapaneseText = "部屋名を取得できませんでした。\n部屋外周線への部屋名設定に失敗しています。\n大変恐れ入りますが、下記の部屋外周線を削除・再作図して下さい。";
            message.AddInfo(startPoint, endPoint);

            return message.ToString();
        }

        public static string NoHinbanLight(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "There is a light that has no hinban.";
            message.JapaneseText = "品番の設定されていない照明があります。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NoLightNumber(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "There is a light that has no light number.";
            message.JapaneseText = "照明番号が設定されていない照明があります。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string InvalidHinban(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "Light serial is invalid.";
            message.JapaneseText = "品番が登録されていないか廃盤になっています。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NoLightNoLight(Symbol symbol)
        {
            var message = new Message();
            message.EnglishText = "No light number symbol found.";
            message.JapaneseText = "照明番号が設定されていない照明があります。";
            message.AddInfo(symbol);

            return message.ToString();
        }

        public static string NotFoundLightSerial(string serial, Symbol light)
        {
            var message = new Message();
            message.EnglishText = "Not found light serial. Serial:[" + serial + "]";
            message.JapaneseText = "登録されていない照明品番[" + serial + "]が見つかりました。";
            message.AddInfo(light);

            return message.ToString();
        }

        public static string InvalidRoomCode(string roomCode)
        {
            var message = new Message();
            message.EnglishText = "Invalid room code.";
            message.JapaneseText = "部屋コードが不正です。";
            message.AddInfo("RoomCode", roomCode);

            return message.ToString();
        }

        public static string InvalidRoomName(string roomName)
        {
            var message = new Message();
            message.EnglishText = "Invalid room name.";
            message.JapaneseText = "部屋名が不正です。";
            message.AddInfo("RoomName", roomName);

            return message.ToString();
        }

        public static string InvalidRoom(int floor, string roomName)
        {
            var message = new Message();
            message.EnglishText = "This room doesn't exist in Shiyousho.";
            message.JapaneseText = "仕様書に存在しない部屋があります。";
            message.AddInfo("Floor", floor + "F");
            message.AddInfo("RoomName", roomName);

            return message.ToString();
        }

        public static string DuplicatedRoomCode(string roomCode)
        {
            var message = new Message();
            message.EnglishText = "Duplicated room code.";
            message.JapaneseText = "部屋コードが重複しています。";
            message.AddInfo("RoomCode", roomCode);

            return message.ToString();
        }

        public static string EmptyPortion(string name)
        {
            var message = new Message();
            message.EnglishText = name + " is empty.";
            message.JapaneseText = name + "を入力してください。";

            return message.ToString();
        }

        public static string DuplicatedTextItemName(string text, string itemName)
        {
            var message = new Message();
            message.EnglishText = "Duplicated text.";
            message.JapaneseText = "Textが重複しています。";
            message.AddInfo("Text", text);
            message.AddInfo("ItemName", itemName);

            return message.ToString();
        }


        public static string DuplicatedItem(string class1Code, string class2Code, string productCode, string itemName)
        {
            var message = new Message();
            message.EnglishText = "Duplicated item.";
            message.JapaneseText = "Itemが重複しています。";
            message.AddInfo("Class1Code", class1Code);
            message.AddInfo("Class2code", class2Code);
            message.AddInfo("ProductCode", productCode);
            message.AddInfo("ItemName", itemName);

            return message.ToString();
        }

        public static string NotClosedRoomLine(int floor, PointD startPoint, PointD endPoint)
        {
            var message = new Message();
            message.EnglishText = "There is unclosed roomline.";
            message.JapaneseText = "始点と終点がつながっていない部屋枠線があります。";
            message.AddInfo("Floor", floor.ToString());
            message.AddInfo(startPoint, endPoint);

            return message.ToString();
        }

        public static string PlateRelationWasLost()
        {
            var message = new Message();
            message.EnglishText = "The relation between plate and symbol was lost.\nPlease remove and redraw the plate.";
            message.JapaneseText = "プレートとシンボルの関連が失われています。\n恐れ入りますが、プレートを削除し、再度配置して下さい。";

            return message.ToString();
        }

        public static string PlateRelationWasLost(Plate plate)
        {
            var message = new Message();
            message.EnglishText = "The relation between plate and symbol was lost.\nPlease remove and redraw the plate.";
            message.JapaneseText = "プレートとシンボルの関連が失われています。\n恐れ入りますが、プレートを削除し、再度配置して下さい。";
            message.AddInfo("Floor", plate.Floor.ToString());
            message.AddInfo("Location", AutoCad.Db.Ellipse.GetStartPoint(plate.ObjectId).ToString());


            return message.ToString();
        }

        public static string InvalidTarekabeJyou(string value)
        {
            var message = new Message();
            message.EnglishText = "Invalid タレ壁 帖.";
            message.JapaneseText = "タレ壁の帖数が不正です。";
            message.AddInfo("Value", value);
            return message.ToString();
        }

        public static string JointBoxOnProhibitedArea()
        {
            var message = new Message();
            message.EnglishText = "Please check location.\nJoint box was installed in prohibited area.";
            return message.ToString();
        }

        public static string HatchingFailed()
        {
            var message = new Message();
            message.EnglishText = "Failed to paint out area.\nPlease retry.";
            message.JapaneseText = "塗りつぶしに失敗しました。\n恐れ入りますが、再度実行して下さい。";

            return message.ToString();
        }

        public static string InvalidOutlineShape(PointD start, PointD end)
        {
            var message = new Message();
            message.EnglishText = "Invalid outline shape.\nPlease check.";
            message.AddInfo(start, end);

            return message.ToString();
        }

        public static string CanNotUseAutoCADPlot()
        {
            var message = new Message();
            message.EnglishText = "The print function of AutoCAD cannot be used. \nPlease use it after it opens drawing.. ";
            message.JapaneseText = "現在はAutoCADの印刷機能を使用できません。\n図面を開いた後、使用してください。";

            return message.ToString();
        }

        /// <summary>
        /// Serial For Unit Wiring Onlyのコメントカテゴリーが見つからなかった時のエラーメッセージ。
        /// </summary>
        /// <returns></returns>
        public static string NotFoundSerialForUnitWiringOnlyCommentCategory()
        {
            Message message = new Message();
            message.JapaneseText = "Serial For Unit Wiring Onlyのコメントカテゴリが取得できませんでした。";
            message.EnglishText = "Serial For Unit Wiring Only Category Not Found. ";
            return message.ToString();
        }

        /// <summary>
        /// コメントカテゴリーが見つからなかった時のエラーメッセージ。
        /// </summary>
        /// <returns></returns>
        public static string NotFoundCommentCategory()
        {
            Message message = new Message();
            message.JapaneseText = "コメントカテゴリが取得できませんでした。";
            message.EnglishText = "Comment Category Not Found. ";
            return message.ToString();
        }
    }
}