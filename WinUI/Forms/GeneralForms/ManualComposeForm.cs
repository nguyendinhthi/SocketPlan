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
    public partial class ManualComposeForm : Form
    {
        private static ManualComposeForm instance;
        public static ManualComposeForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new ManualComposeForm();

                return instance;
            }
        }

        public static void DisposeInstance()
        {
            if (instance == null)
                return;

            instance.Dispose();
            instance = null;
        }

        private Drawing currentDwg = new Drawing();
        private CadObjectContainer container;
        List<Button> categoryButtonList = new List<Button>();
        private string currentCategory = ""; 

        public ManualComposeForm()
        {
            InitializeComponent();
            var progress = new ProgressManager();
            progress.Processes += new CadEventHandler(this.LoadCadObjectContainer);
            progress.Processes += new CadEventHandler(this.CreateCategoryButtons);
            progress.Run();
        }

        [ProgressMethod("Now DB loading...")]
        private void CreateCategoryButtons()
        {
            System.Drawing.Point buttonPosition = new System.Drawing.Point(0, 0);
            float fontSize = 0f;
            foreach (var categoryItem in UnitWiring.Masters.SocketBoxSpecificCategories)
            {
                System.Windows.Forms.Button categoryButton = new Button();
                categoryButton.Name = categoryItem.Name + "Button";
                categoryButton.Text = categoryItem.Name;
                categoryButton.BackColor = System.Drawing.SystemColors.Control;
                categoryButton.Location = buttonPosition;
                categoryButton.Size = new System.Drawing.Size(140, 40);
                categoryButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                categoryButton.UseVisualStyleBackColor = false;
                categoryButton.TabIndex = UnitWiring.Masters.SocketBoxSpecificCategories.IndexOf(categoryItem);
                categoryButton.TabStop = true;

                categoryButton.Font = new System.Drawing.Font("MS UI Gothic", 1, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                fontSize = this.GetProperFontSize(categoryButton.Text, categoryButton.Font, 140, 40);
                categoryButton.Font = new System.Drawing.Font(categoryButton.Font.OriginalFontName, fontSize);
                categoryButton.Click += new System.EventHandler(this.categoryButton_Click);

                this.Controls.Add(categoryButton);
                this.categoryButtonList.Add(categoryButton);
                categoryButton.BringToFront();
                buttonPosition.Y += 40;
            }

            if (this.categoryButtonList.Count > 0)
            {
                this.categoryButtonList[0].BackColor = Color.Yellow;
                this.categoryButtonList[0].Focus();
                this.updateEquipmentView(this.categoryButtonList[0].Text);
                this.currentCategory = this.categoryButtonList[0].Text;
            }
        }

        [ProgressMethod("Now CAD loading...")]
        private void LoadCadObjectContainer()
        {
            WindowController2.BringAutoCadToTop();

            this.currentDwg = Drawing.GetCurrent();
            if (!this.currentDwg.FileName.Contains("Individual"))
                throw new ApplicationException("Please first make, and select the Individual plan.");

            AutoCad.Command.Prepare();
            AutoCad.Command.SetCurrentLayoutToModel();
            
            //こっちの方が多分楽なので
            this.container = new CadObjectContainer(new List<Drawing>() { this.currentDwg },
                CadObjectTypes.RoomOutline |
                CadObjectTypes.Symbol |
                CadObjectTypes.HouseOutline |
                CadObjectTypes.Clip
            );

            this.container.FillSymbolRooms();
            this.container.FillIsOutsideAndIsOutdoor();
        }

        /// <summary>
        /// WidthにTextsが収まるフォントサイズを返す
        /// </summary>
        /// <param name="texts"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private float GetProperFontSize(string texts, System.Drawing.Font font, uint width, uint height)
        {
            float size = 1f;
            for (size = 1; size <= 16; size++)
            {
                font = new System.Drawing.Font(font.OriginalFontName, size);
                var s = TextRenderer.MeasureText(texts, font);
                if (s.Width >= width - 20 || s.Height >= height - 5)//マージン 
                    break;
            }
            return size;
        }

        private SocketBoxSpecific GetSpecific(int specificId)
        {
            foreach (var category in UnitWiring.Masters.SocketBoxSpecificCategories)
            {
                var specific = Array.Find(category.Specifics, p => p.Id == specificId);
                if (specific != null)
                    return specific;
            }

            return null;
        }

        private void updateEquipmentView(string categoryName)
        {
            try
            {
                var category = UnitWiring.Masters.SocketBoxSpecificCategories.Find(p => p.Name == categoryName);
                if (category == null)
                    return;

                this.updateEquipmentListView(category.Specifics);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void categoryListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.equipmentListView.Focus();
            }
        }

        private void equipmentListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnSelectEquipment();
        }

        private void equipmentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectEquipment();
            }
        }

        private void equipmentListView_Enter(object sender, EventArgs e)
        {
            if (this.equipmentListView.FocusedItem == null)
            {
                if (this.equipmentListView.Items.Count != 0)
                    this.equipmentListView.Items[0].Selected = true;
            }
            else
            {
                this.equipmentListView.FocusedItem.Selected = true;
            }
        }

        private void equipmentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var items = this.equipmentListView.SelectedItems;
                if (items.Count == 0)
                    return;

                this.relatedEquipmentListView.Items.Clear();

                var specific = items[0].Tag as SocketBoxSpecific;
                foreach (var related in specific.Relations)
                {
                    var relatedSpecific = this.GetSpecific(related.RelatedSpecificId);
                    if (relatedSpecific == null)
                        continue;

                    var imagePath = Paths.GetImageFullPath(relatedSpecific.ImagePath);

                    ListViewItem item = new ListViewItem();
                    item.Text = relatedSpecific.Serial;
                    item.Tag = relatedSpecific;
                    item.ImageKey = imagePath;
                    this.relatedEquipmentListView.Items.Add(item);

                    if (!this.equipmentImageList.Images.ContainsKey(imagePath))
                        this.equipmentImageList.Images.Add(imagePath, Image.FromFile(imagePath));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void relatedEquipmentListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnSelectRelatedEquipment();
        }

        private void relatedEquipmentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectRelatedEquipment();
            }
        }

        private void relatedEquipmentListView_Enter(object sender, EventArgs e)
        {
            if (this.relatedEquipmentListView.FocusedItem == null)
            {
                if (this.relatedEquipmentListView.Items.Count != 0)
                    this.relatedEquipmentListView.Items[0].Selected = true;
            }
            else
                this.relatedEquipmentListView.FocusedItem.Selected = true;
        }

        private void OnSelectEquipment()
        {
            var items = this.equipmentListView.SelectedItems;
            if (items.Count == 0)
                return;

            this.updateResultListView(items[0]);
        }

        private void OnSelectRelatedEquipment()
        {
            var items = this.relatedEquipmentListView.SelectedItems;
            if (items.Count == 0)
                return;

            this.updateResultListView(items[0]);
        }

        //カテゴリボタン共通
        private void categoryButton_Click(object sender, EventArgs e)
        {
            Button pressedbutton = sender as Button;
            foreach (var button in this.categoryButtonList)
            {
                if (button == pressedbutton)
                {
                    button.BackColor = Color.Yellow;
                    this.updateEquipmentView(pressedbutton.Text);
                    this.currentCategory = pressedbutton.Text;
                }
                else
                {
                    button.BackColor = System.Drawing.SystemColors.Control;
                }
            }
            this.searchText.Text = string.Empty;
        }

        private void updateResultListView(ListViewItem item)
        {
            if (item == null)
                return;

            ListViewItem newItem = new ListViewItem();
            newItem.Text = item.Text;
            newItem.Tag = item.Tag;
            newItem.ImageKey = item.ImageKey;
            this.resultListView.Items.Add(newItem);
        }

        private void deleteResultListView(ListViewItem item)
        {
            if (item == null)
                return;

            this.resultListView.Items.Remove(item);
            this.resultListView.Refresh();
        }

        private void resultListView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    var items = this.resultListView.SelectedItems;
                    if (items.Count == 0)
                        return;

                    this.deleteResultListView(items[0]);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.searchText.Text = string.Empty;
                this.resultListView.Items.Clear();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            try
            {
                //念のためCurrentをチェックする
                WindowController2.BringAutoCadToTop();
                var current = Drawing.GetCurrent();
                if (current.Name != this.currentDwg.Name) 
                {
                    this.ShowErrorMessage("target drawing is moved. please close this form once.");
                    return;
                }

                this.DrawSpecificItmes();

                this.Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void DrawSpecificItmes()
        {
            var items = this.resultListView.Items;
            if (items.Count == 0)
            {
                this.ShowErrorMessage("please select specific items.");
                return;
            }

            this.Hide();

            int seq = 0;
            using (var service = new SocketPlanService())
                seq = service.GetNextSocketBoxSeq(Static.ConstructionCode);
            
            var drawItems = new List<SocketBoxSpecific>();
            foreach (ListViewItem item in items)
                drawItems.Add((SocketBoxSpecific)item.Tag);

            SymbolDrawer.DrawSpecifics(ref drawItems, seq);

            //シンボルの詳細を探す
            this.GetSymbolDetails(ref drawItems);

            //DB書き込み
            this.RegisterSocketBoxData(drawItems);
        }

        private void GetSymbolDetails(ref List<SocketBoxSpecific> specifics)
        {
            var symbols = this.container.Symbols;
            if (symbols.Count == 0)
                return;

            foreach (var specific in specifics)
                specific.Symbol = symbols.Find(p => p.ObjectId == specific.SymbolObjectId || p.ClippedObjectId == specific.SymbolObjectId);//同時に一致することはないはず
        }

        private void RegisterSocketBoxData(List<SocketBoxSpecific> specifics)
        {
            var entities = this.CreateSocketBoxEntities(specifics);

            using (var service = new SocketPlanService()) 
            {
                service.RegisterSocketBoxBySpecific(Static.ConstructionCode, entities.ToArray());
            }
        }

        private List<SocketBox> CreateSocketBoxEntities(List<SocketBoxSpecific> specifics)
        {
            var result = new List<SocketBox>();

            foreach (var specific in specifics) 
                result.Add(this.CreateSocketBoxEntity(specific));

            return result;
        }

        private SocketBox CreateSocketBoxEntity(SocketBoxSpecific specific)
        {
            var box = new SocketBox();

            box.ConstructionCode = Static.ConstructionCode;
            box.PlanNo = Static.Drawing.PlanNo;
            box.ZumenNo = Static.Drawing.RevisionNo;

            box.Seq = 0;    //あとで入れる
            box.SetSeq = 0; //あとで入れる

            box.Floor = this.currentDwg.Floor;
            if (specific.Symbol.Room != null)
                box.RoomCode = specific.Symbol.Room.CodeInSiyo;
            box.RoomName = specific.Symbol.RoomName;

            box.Color = specific.Color;
            box.Shape = specific.Shape;
            box.PatternId = -1; //固定
            box.Size = specific.SocketBoxSize == null ? 0 : (int)specific.SocketBoxSize;
            box.Height = Convert.ToInt32(specific.Symbol.Height);
            box.Direction = specific.Symbol.GetRotate();
            box.DwgPath = specific.BlockPath;
            box.patternName = specific.Serial;
            box.SpecificId = specific.Id;
            box.SocketObjectId = specific.SocketBlockId;

            if (specific.Symbol.Clipped)
                box.SymbolLocation = specific.Symbol.ClippedPosition;
            else
                box.SymbolLocation = specific.Symbol.Position;

            box.ActualSymbolLocation = specific.Symbol.ActualPosition;
            box.BoxLeftLocation = SymbolDrawer.LeftBottom;
            box.BoxRightLocation = SymbolDrawer.RightTop;

            return box;
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var category = UnitWiring.Masters.SocketBoxSpecificCategories.Find(p => p.Name == this.currentCategory);

                var lowerText = this.searchText.Text.ToLower();
                var specifics = Array.FindAll(category.Specifics, p => p.Serial.ToLower().Contains(lowerText));

                this.updateEquipmentListView(specifics);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void ShowErrorMessage(string msg) 
        {
            this.Hide();
            MessageDialog.ShowError(msg);
            this.Show();
        }

        private void updateEquipmentListView(SocketBoxSpecific[] specifics) 
        {
            this.equipmentListView.Items.Clear();
            foreach (var equipment in specifics)
            {
                var imagePath = Paths.GetImageFullPath(equipment.ImagePath);

                ListViewItem item = new ListViewItem();
                item.Text = equipment.Serial;
                item.Tag = equipment;
                item.ImageKey = imagePath;
                this.equipmentListView.Items.Add(item);

                if (!this.equipmentImageList.Images.ContainsKey(imagePath))
                    this.equipmentImageList.Images.Add(imagePath, Image.FromFile(imagePath));
            }
        }
    }
}
