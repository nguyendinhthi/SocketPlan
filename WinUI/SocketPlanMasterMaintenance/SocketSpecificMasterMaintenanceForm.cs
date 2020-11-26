using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;
using Edsa.CustomControl;
using SocketPlan.WinUI;
using SocketPlan.WinUI.Properties;
using System.IO;

namespace SocketPlan.WinUI
{
    public partial class SocketSpecificMasterMaintenanceForm : Form
    {
        private enum SpecifiBoxSize { None = 0, Single, Double, Triple, Quadruple }

        private int specificId;

        public SocketSpecificMasterMaintenanceForm()
        {
            InitializeComponent();

            this.dataGridView.AutoGenerateColumns = false;

            this.UpdateTree(true);
            this.UpdateCategoryCombo();
            this.UpdateSizeCombo();
        }

        private void UpdateCategoryCombo()
        {
            var categories = new List<SocketBoxSpecificCategory>();
            using (var service = new SocketPlanService())
            {
                categories.AddRange(service.GetAllSocketBoxSpecificCategories());
            }

            this.categoryCombo.ValueMember = "Id";
            this.categoryCombo.DisplayMember = "Name";
            this.categoryCombo.DataSource = categories;
        }

        private void UpdateSizeCombo()
        {
            this.sizeCombo.DataSource = Enum.GetValues(typeof(SpecifiBoxSize));
        }

        private void UpdateTree(bool fromDb)
        {
            this.treeView.Nodes.Clear();

            var categories = UnitWiring.Masters.SocketBoxSpecificCategories;
            if (fromDb)
            {
                categories.Clear();
                using (var service = new SocketPlanService())
                {
                    categories.AddRange(service.GetAllSocketBoxSpecificCategories());
                }
            }

            foreach (var category in categories)
            {
                var node = new TreeNode(category.Name);
                node.Tag = category;

                foreach (var specific in category.Specifics)
                {
                    var child = new TreeNode(specific.Serial);
                    child.Tag = specific;
                    node.Nodes.Add(child);
                }

                this.treeView.Nodes.Add(node);
            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2 && this.treeView.SelectedNode.Level == 0)
                {
                    this.treeView.SelectedNode.BeginEdit();
                    return;
                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (MessageDialog.ShowOkCancel("Node will be deleted. Are you sure?") != DialogResult.OK)
                        return;

                    using(var service = new SocketPlanService())
                    {
                        if (this.treeView.SelectedNode.Level == 0)
                        {
                            var id = (this.treeView.SelectedNode.Tag as SocketBoxSpecificCategory).Id;
                            service.DeleteSocketBoxSpecificCategory(id);
                        }
                        else
                        {
                            var id = (this.treeView.SelectedNode.Tag as SocketBoxSpecific).Id;
                            service.DeleteSocetBoxSpecific(id);
                        }
                    }

                    this.UpdateTree(true);

                    UnitWiring.Masters.UpdateSocketBoxSpecificCategories();

                    MessageDialog.ShowInformation(this, "Deleted successfully.");
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.treeView.SelectedNode.Level != 1)
                {
                    this.ClearPanel();
                    this.saveButton.Enabled = false;
                    return;
                }
                
                var specific = this.treeView.SelectedNode.Tag as SocketBoxSpecific;
                this.UpdatePanel(specific);
                this.saveButton.Enabled = true;

                this.specificId = specific.Id;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ClearPanel()
        {
            this.categoryCombo.SelectedIndex = 0;
            this.serialText.Text = string.Empty;
            this.imagePathText.Text = string.Empty;
            this.pictureBox.BackgroundImage = null;
            this.blockPathText.Text = string.Empty;
            this.sizeCombo.SelectedIndex = 0;
            this.depthCombo.SelectedIndex = 0;
            this.shapeCombo.SelectedIndex = 0;
            this.colorCombo.SelectedIndex = 0;
            this.SetGrid(new List<SocketBoxSpecific>());
        }

        private void UpdatePanel(SocketBoxSpecific specific)
        {
            this.categoryCombo.SelectedValue = specific.SpecificCategoryId;
            this.serialText.Text = specific.Serial;
            this.imagePathText.Text = specific.ImagePath;
            this.UpdatePicture();
            this.blockPathText.Text = specific.BlockPath;

            if(specific.SocketBoxSize.HasValue)
                this.sizeCombo.SelectedItem = (SpecifiBoxSize)specific.SocketBoxSize;
            else
                this.sizeCombo.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(specific.SocketBoxDepth))
                this.depthCombo.SelectedItem = specific.SocketBoxDepth;
            else
                this.depthCombo.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(specific.Color))
                this.colorCombo.SelectedItem = specific.Color;
            else
                this.colorCombo.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(specific.Shape))
                this.shapeCombo.SelectedItem = specific.Shape;
            else
                this.shapeCombo.SelectedIndex = 0;

            var relatedSpecifics = new List<SocketBoxSpecific>();
            foreach(var relation in specific.Relations)
            {
                var related = this.GetSpecific(relation.RelatedSpecificId);
                relatedSpecifics.Add(related);
            }

            this.SetGrid(new List<SocketBoxSpecific>(relatedSpecifics));
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

        private void SetGrid(List<SocketBoxSpecific> specifics)
        {
            this.dataGridView.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = specifics;
            this.dataGridView.DataSource = bs;
        }

        private void imagePathButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Select specific image file for socket plan";
                dialog.Filter = "Image Files (*.jpeg;*.jpg;*.gif;*.png)|*.jpeg;*.jpg;*.gif;*.png";
                dialog.InitialDirectory = Properties.Settings.Default.ImageDirectory;
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                this.imagePathText.Text = dialog.FileName;
                this.UpdatePicture();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void imagePathText_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.imagePathText.Text))
                    return;

                if (!File.Exists(this.imagePathText.Text))
                {
                    MessageDialog.ShowWarning("File not found.");
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void imagePathText_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.imagePathText.Text))
                    return;

                this.Cursor = Cursors.WaitCursor;
                this.UpdatePicture();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UpdatePicture()
        {
            if (string.IsNullOrEmpty(this.imagePathText.Text))
            {
                this.pictureBox.Image = null;
                return;
            }

            if (!File.Exists(this.imagePathText.Text))
                throw new ApplicationException("Invalid path. File not found.");

            this.pictureBox.BackgroundImage = Image.FromFile(this.imagePathText.Text);
            this.pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void blockPathButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Select specific DWG file for socket plan";
                dialog.Filter = "DWG Files (.dwg)|*.dwg";
                dialog.InitialDirectory = Path.Combine(Settings.Default.BlockDirectory, @"SocketBox\Specific");
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                this.blockPathText.Text = dialog.FileName;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new SocketBoxSpecificSelectForm();
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                var relatedSpecifics = (this.dataGridView.DataSource as BindingSource).DataSource as List<SocketBoxSpecific>;
                relatedSpecifics.Add(SocketBoxSpecificSelectForm.SelectedSpecific);

                this.SetGrid(relatedSpecifics);
            }
            catch(Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (!this.Validate())
                    return;

                var specific = new SocketBoxSpecific();
                specific.Id = this.specificId;
                specific.Serial = this.serialText.Text;
                specific.ImagePath = this.imagePathText.Text;
                specific.BlockPath = this.blockPathText.Text;

                if ((int)this.sizeCombo.SelectedValue == 0)
                    specific.SocketBoxSize = null;
                else
                    specific.SocketBoxSize = (int)this.sizeCombo.SelectedValue;

                specific.Color = this.colorCombo.SelectedItem.ToString();
                specific.Shape = this.shapeCombo.SelectedItem.ToString();
                specific.SocketBoxDepth = this.depthCombo.SelectedItem.ToString();
                specific.SpecificCategoryId = (int)this.categoryCombo.SelectedValue;
                specific.Relations = this.CreateRelations().ToArray();

                using (var service = new SocketPlanService())
                {
                    specific.Id = service.RegisterSocketBoxSpecific(specific);
                }

#if !DEBUG
                var userName = Properties.Settings.Default.ServerUserName;
                var password = Properties.Settings.Default.ServerPassword;
                var source = Paths.GetServerSystemDirectory();
                MasterFileLoader.Authorize(source, userName, password);
#endif

                this.CopyToServer(specific);
                this.IncrementMasterFileVersion();

                this.UpdateTree(true);
                this.SelectSpecificNode(specific.Id);

                UnitWiring.Masters.UpdateSocketBoxSpecificCategories();

                MessageDialog.ShowInformation(this, "Saved successfully.");
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private new bool Validate()
        {
            if (string.IsNullOrEmpty(this.serialText.Text))
            {
                MessageDialog.ShowWarning("Please input name.");
                return false;
            }

            return true;
        }

        private List<SocketBoxSpecificRelation> CreateRelations()
        {
            var relatedSpecifics = (this.dataGridView.DataSource as BindingSource).DataSource as List<SocketBoxSpecific>;
            if (relatedSpecifics == null)
                throw new ApplicationException("Failed to get related specifics");

            var relations = new List<SocketBoxSpecificRelation>();
            foreach (var related in relatedSpecifics)
            {
                var relation = new SocketBoxSpecificRelation();
                relation.SocketBoxSpecificId = this.specificId;
                relation.RelatedSpecificId = related.Id;
                relations.Add(relation);
            }

            return relations;
        }

        private void SelectCategoryNode(int categoryId)
        {
            foreach (TreeNode parent in this.treeView.Nodes)
            {
                var category = parent.Tag as SocketBoxCategory;
                if (category == null)
                    continue;

                if (category.Id == specificId)
                {
                    this.treeView.SelectedNode = parent;
                    return;
                }
            }
        }

        private void SelectSpecificNode(int specificId)
        {
            foreach (TreeNode parent in this.treeView.Nodes)
            {
                var category = parent.Tag as SocketBoxSpecificCategory;
                if (category == null)
                    continue;

                foreach (TreeNode child in parent.Nodes)
                {
                    var specific = child.Tag as SocketBoxSpecific;
                    if (specific == null)
                        continue;

                    if (specific.Id == specificId)
                    {
                        this.treeView.SelectedNode = child;
                        return;
                    }
                }
            }
        }

        private void CopyToServer(SocketBoxSpecific specific)
        {
            this.CopyToServer(specific.ImagePath);
            this.CopyToServer(specific.BlockPath);
        }

        private void CopyToServer(string localPath)
        {
            if (string.IsNullOrEmpty(localPath))
                return;

            if (!localPath.Contains(Settings.Default.LocalSystemDirectory))
                throw new ApplicationException("Please set dwg path contain '" + Settings.Default.LocalSystemDirectory + "'");

            var serverPath = localPath.Replace(Settings.Default.LocalSystemDirectory, Paths.GetServerSystemDirectory());
            if (!File.Exists(serverPath))
                Directory.CreateDirectory(serverPath.Replace(Path.GetFileName(serverPath), string.Empty));

            File.Copy(localPath, serverPath, true);
        }

        private void IncrementMasterFileVersion()
        {
            var directory = Paths.GetServerSystemDirectory();
            var filePath = directory + MasterFileLoader.VERSION_TEXT;
            var version = MasterFileLoader.GetMasterVersion(directory);
            version++;

            using(var writer = new StreamWriter(filePath, false, Encoding.GetEncoding("Shift_JIS")))
            {
                writer.Write(version.ToString());
            }
        }

        private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (e.Node.Level != 0)
                    e.CancelEdit = true;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                this.treeView.AfterLabelEdit -= treeView_AfterLabelEdit;

                if(e.Node.Level != 0)
                {
                    MessageDialog.ShowWarning("Cannot change serial on tree view.");
                    e.CancelEdit = true;
                    return;
                }

                var category = e.Node.Tag as SocketBoxSpecificCategory;
                if (category == null)
                {
                    category = new SocketBoxSpecificCategory();
                    category.Id = 0;
                }

                if (string.IsNullOrEmpty(e.Label))
                {
                    e.Node.Text = category.Name;
                    return;
                }

                category.Name = e.Label;

                using(var service = new SocketPlanService())
                {
                    category.Id = service.RegisterSocketBoxSpecificCategory(category);
                }

                this.UpdateCategoryCombo();

                UnitWiring.Masters.UpdateSocketBoxSpecificCategories();

                MessageDialog.ShowInformation(this, "Updated category successfully.");
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
            finally
            {
                this.treeView.AfterLabelEdit += treeView_AfterLabelEdit;
                this.Cursor = Cursors.Default;
            }
        }

        private void newCategoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                var node = new TreeNode("None");
                this.treeView.Nodes.Add(node);
                this.treeView.SelectedNode = node;
                node.BeginEdit();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void newSpecificButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearPanel();
                this.saveButton.Enabled = true;

                this.specificId = 0;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
