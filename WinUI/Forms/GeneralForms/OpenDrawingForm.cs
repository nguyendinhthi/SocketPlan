using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public partial class OpenDrawingForm : Form
    {
        Dictionary<string, List<Drawing>> drawingDictionary;

        public OpenDrawingForm()
        {
            InitializeComponent();
            this.dataGridView.AutoGenerateColumns = false;
            this.Clear();
        }

        private void Clear()
        {
            this.openButton.Enabled = false;
            this.dataGridView.DataSource = null;
        }

        private void OpenDrawingForm_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void constructionCodeText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.constructionCodeText.Text.Length != 12)
                    return;

                if (e.KeyCode != Keys.Enter)
                    return;

                this.Clear();

                this.LoadDrawings();

                this.dataGridView.Focus();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void constructionCodeText_TextChanged(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //AutoCADが起動していない時AutoCADを起動する
                if (!WindowController2.ExistAutoCad())
                {
                    throw new ApplicationException(Messages.PleaseLaunchAutoCAD());
                    //WindowController.LaunchAutoCAD();プログラムから起動すると、なぜかFatalErrorが頻発するので、やめ。
                    //ユーザーに起動してもらう
                }

                var dataRow = this.dataGridView.SelectedRows[0].DataBoundItem as DataRowView;
                var drawings = dataRow["Drawings"] as List<Drawing>;
                var count = this.OpenFiles(drawings);

                //new AdjustGrid0point().Run();

                this.Activate();
                if (count > 0)
                    MessageDialog.ShowInformation(this, Messages.FileOpened(count));
                else
                    MessageDialog.ShowInformation(this, Messages.FileAlreadyOpened());

                this.Close();
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(ex);

                //途中でエラーになると、中途半端にStaticが更新されてしまう。
                //そうなった時は、ConstructionCodeをnullにしてコントロールのenableを切る。
                Static.ConstructionCode = null;

                this.DialogResult = DialogResult.None;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public int OpenFiles(List<Drawing> drawings)
        {
            if (drawings[0].FileName.Contains(" "))
            {
                throw new ApplicationException(Messages.CannotOpenFileWithSpace());
            }

            Static.ConstructionCode = drawings[0].ConstructionCode;
            Static.Drawing = drawings[0];
            Static.SymbolCache.Clear(); //図面によって、シンボル情報が微妙に異なることがあるので、図面を変えたらクリアする。
            this.GetConstructionInfomations(Static.ConstructionCode, drawings[0].PlanNo);
            this.LoadSpecs(drawings[0].PlanNo);
            UnitWiring.Masters.DisposeRooms();

            if (drawings.Count > 0)
            {
                drawings.Sort((p, q) => p.Floor - q.Floor);
                Static.HouseSpecs.MaxFloor = drawings[drawings.Count - 1].Floor;
            }

            var count = Drawing.OpenAll(drawings);

            AutoCad.vbcom.LoadComObject();
            UnitWiring.ClearApprovedHistory();

            Drawing.Bring1FDrawingToTop();

            return count;
        }

        public int OpenFilesForSocketPlan(List<Drawing> drawings)
        {
            if (drawings[0].FileName.Contains(" "))
            {
                throw new ApplicationException(Messages.CannotOpenFileWithSpace());
            }

            Static.ConstructionCode = drawings[0].ConstructionCode;
            Static.Drawing = drawings[0];
            Static.SymbolCache.Clear(); //図面によって、シンボル情報が微妙に異なることがあるので、図面を変えたらクリアする。
            this.GetConstructionInfomations(Static.ConstructionCode, drawings[0].PlanNo);
            this.LoadSpecs(drawings[0].PlanNo);
            UnitWiring.Masters.DisposeRooms();

            if (drawings.Count > 0)
            {
                drawings.Sort((p, q) => p.Floor - q.Floor);
                Static.HouseSpecs.MaxFloor = drawings[drawings.Count - 1].Floor;
            }

            var count = Drawing.OpenAllForSocketPlan(drawings);

            AutoCad.vbcom.LoadComObject();
            UnitWiring.ClearApprovedHistory();

            Drawing.Bring1FDrawingToTop();

            return count;
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                this.openButton.PerformClick();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            this.openButton.PerformClick();
        }

        private void LoadDrawings()
        {
            var drawings = new List<Drawing>();

            foreach (var directory in Paths.GetServerDrawingDirectories())
            {
                drawings.AddRange(Drawing.Search(this.constructionCodeText.Text, directory, "Server"));
            }

            var localPath = Properties.Settings.Default.DrawingDirectory;
            drawings.AddRange(Drawing.Search(this.constructionCodeText.Text, localPath, "Local"));

            if (drawings.Count == 0)
                throw new ApplicationException(Messages.NotFoundServerDrawingDirectory(this.constructionCodeText.Text));

            //RevisionNoとか毎に図面をまとめる
            this.drawingDictionary = new Dictionary<string, List<Drawing>>();
            foreach (var drawing in drawings)
            {
                var key = drawing.Directory + "/" + drawing.Location + "/" + drawing.Prefix + "/" + drawing.ConstructionCode + "/" + drawing.PlanNo + "/" + drawing.RevisionNo + "/" + drawing.Extension;
                if (!this.drawingDictionary.ContainsKey(key))
                    this.drawingDictionary.Add(key, new List<Drawing>());

                this.drawingDictionary[key].Add(drawing);
            }

            this.dataGridView.DataSource = this.CreateTable(this.drawingDictionary);
            this.openButton.Enabled = true;
        }

        //Floor列を表示する為にわざわざDataTableにしました。
        private DataTable CreateTable(Dictionary<string, List<Drawing>> dic)
        {
            var table = new DataTable();
            table.Columns.Add("Location");
            table.Columns.Add("Prefix");
            table.Columns.Add("PlanNo");
            table.Columns.Add("RevisionNo");
            table.Columns.Add("Floor");
            table.Columns.Add("UpdatedDate", typeof(DateTime));
            table.Columns.Add("Drawings", typeof(List<Drawing>));

            foreach (var key in this.drawingDictionary.Keys)
            {
                var drawings = dic[key];

                var row = table.NewRow();
                table.Rows.Add(row);

                row["Location"] = drawings[0].Location;
                row["Prefix"] = drawings[0].Prefix;
                row["PlanNo"] = drawings[0].PlanNo;
                row["RevisionNo"] = drawings[0].RevisionNo;
                row["UpdatedDate"] = drawings[0].UpdatedDate;
                row["Drawings"] = drawings;

                row["Floor"] = string.Empty;
                foreach (var drawing in drawings)
                {
                    row["Floor"] += drawing.FloorCode + ",";
                }
                row["Floor"] = row["Floor"].ToString().TrimEnd(',');
            }

            var sortedTable = table.Clone();
            foreach (var row in table.Select("", "UpdatedDate DESC"))
            {
                sortedTable.ImportRow(row);
            }

            return sortedTable;
        }

        private void LoadSpecs(string planNo)
        {
            var specs = new HouseSpecs();

            List<Setting> settings;
            using (var service = new SocketPlanServiceNoTimeout())
            {
                var siyoCode = service.GetSiyoCode(Static.ConstructionCode, Utilities.ConvertPlanNo(planNo));
                settings = new List<Setting>(service.GetSettings(Static.ConstructionCode, siyoCode));
                specs.Kanabakari = service.GetKanabakari(Static.ConstructionCode, siyoCode);
                if (specs.Kanabakari == Const.Kanabakari._240)
                {
                    specs.CeilingHeight_1F = Const.CeilingHeight._2400;
                    specs.CeilingHeight_2F = Const.CeilingHeight._2400;
                }
                else if (specs.Kanabakari == Const.Kanabakari._260)
                {
                    specs.CeilingHeight_1F = Const.CeilingHeight._2600;
                    specs.CeilingHeight_2F = Const.CeilingHeight._2400;
                }
                else if (specs.Kanabakari == Const.Kanabakari._265)
                {
                    specs.CeilingHeight_1F = Const.CeilingHeight._2650;
                    specs.CeilingHeight_2F = Const.CeilingHeight._2500;
                }
                else
                    throw new ApplicationException(Messages.InvalidKanabakari(Static.HouseSpecs.Kanabakari));


                specs.HouseTypeCode = service.GetHouseTypeCode(Static.ConstructionCode, Utilities.ConvertPlanNo(planNo));
                var gropuDetail = service.GetHouseTypeGroupDetail(Static.ConstructionTypeCode);
                if (gropuDetail != null)
                    specs.HouseTypeGroupId = gropuDetail.HouseTypeGroupId;
                else
                    specs.HouseTypeGroupId = 0;

                if (!Static.IsBeforeKakouIrai)
                { //加工依頼後だったら、太陽光関連の情報を取得する。
                    //加工依頼前は、特に使わないので、デフォ値で問題なし。
                    specs.ExistSolar = service.ExistSolar(Static.ConstructionCode);
                    specs.PowerConditionerCount = service.GetPowerConditionerCount(Static.ConstructionCode);
                }
            }

            specs.ExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 1).Value);
            specs.CeilingReceiverExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 2).Value);
            specs.CeilingDepth1F = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 3).Value);
            specs.CeilingDepth2F = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 4).Value);
            specs.CeilingThickness1F = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 5).Value);
            specs.CeilingThickness2F = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 6).Value);
            specs.FloorThickness = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 7).Value);
            specs.ConnectorMaleExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 8).Value);
            specs.ConnectorFemaleExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 9).Value);
            specs.JBExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 10).Value);
            specs.BreakerExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 11).Value);
            specs.TerminalExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 12).Value);
            specs.DownLightExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 13).Value);
            specs.SolarSocketExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 15).Value);
            specs.PowerConExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 16).Value);
            specs.ConnectorHaikanMaleExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 17).Value);
            specs.ConnectorHaikanFemaleExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 18).Value);
            specs.HaikanTakeOutExtraLength = Convert.ToDecimal(settings.Find(p => p.SettingKindId == 19).Value);

            Static.HouseSpecs = specs;
        }

        //工事コードで決まる情報の取得（増えてきたのでまとめた）
        private void GetConstructionInfomations(string constructionCode, string planNo)
        {
            using (var service = new SocketPlanServiceNoTimeout())
            {
                Static.IsBeforeKakouIrai = service.IsBeforeProcessRequest(constructionCode);
                Static.IsTenjijyo = service.IsTenjijyoConstruction(constructionCode);
                Static.Schedule = service.GetSchedule(constructionCode);
                Static.ConstructionTypeCode = service.GetConstructionTypeCode(constructionCode, Utilities.ConvertPlanNo(planNo));
            }
        }
    }
}
