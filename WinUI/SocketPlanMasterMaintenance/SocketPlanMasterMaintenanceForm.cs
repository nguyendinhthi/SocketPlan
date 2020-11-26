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
    public enum BoxSize
    {
        None = 0,
        Single,
        Double,
        Triple,
        Quadruple
    }

    public partial class SocketPlanMasterMaintenanceForm : Form
    {
        private int patternId;

        public SocketPlanMasterMaintenanceForm()
        {
            InitializeComponent();

            this.colorGrid.AutoGenerateColumns = false;
            this.detailGrid.AutoGenerateColumns = false;
            this.commentGrid.AutoGenerateColumns = false;

            this.UpdateTree();
            this.UpdateCategoryCombo();
            this.UpdateSizeCombo();
        }

        private void UpdateCategoryCombo()
        {
            var categories = new List<SocketBoxCategory>();
            using (var service = new SocketPlanService())
            {
                categories.AddRange(service.GetAllSocketBoxCategoriesSimple());
            }

            this.categoryCombo.ValueMember = "Id";
            this.categoryCombo.DisplayMember = "Name";
            this.categoryCombo.DataSource = categories;
        }

        private void UpdateSizeCombo()
        {
            this.sizeCombo.DataSource = Enum.GetValues(typeof(BoxSize));
        }

        private void UpdateTree()
        {
            this.treeView.Nodes.Clear();

            var categories = new List<SocketBoxCategory>();
            using (var service = new SocketPlanService())
            {
                categories.AddRange(service.GetAllSocketBoxCategories());
            }

            foreach (var category in categories)
            {
                var node = new TreeNode(category.DisplayName);
                node.Tag = category;

                foreach (var pattern in category.Patterns)
                {
                    var child = new TreeNode(pattern.NodeName);
                    child.Tag = pattern;
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

                    using (var service = new SocketPlanService())
                    {
                        if (this.treeView.SelectedNode.Level == 0)
                        {
                            var id = (this.treeView.SelectedNode.Tag as SocketBoxCategory).Id;
                            service.DeleteSocketBoxCategory(id);
                        }
                        else
                        {
                            var id = (this.treeView.SelectedNode.Tag as SocketBoxPattern).Id;
                            service.DeleteSocetBoxPattern(id);
                        }
                    }

                    this.UpdateTree();

                    UnitWiring.Masters.UpdateSocketBoxPatterns();

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
                this.commentGrid.RowsRemoved -= this.commentGrid_RowsRemoved;

                if (this.treeView.SelectedNode.Level != 1)
                {
                    this.ClearPanel();
                    this.saveButton.Enabled = false;
                    return;
                }

                var pattern = this.treeView.SelectedNode.Tag as SocketBoxPattern;

                var details = new List<SocketBoxPatternDetail>();
                var colors = new List<SocketBoxPatternColor>();
                using (var service = new SocketPlanService())
                {
                    details.AddRange(service.GetSocketBoxDetails(pattern.Id));
                    colors.AddRange(service.GetSocketBoxColors(pattern.Id));
                }

                pattern.Details = details.ToArray();
                pattern.Colors = colors.ToArray();
                this.UpdatePanel(pattern);
                this.saveButton.Enabled = true;

                this.patternId = pattern.Id;

                this.commentGrid.RowsRemoved += this.commentGrid_RowsRemoved;
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
            this.nameText.Text = string.Empty;
            this.individualWRText.Text = string.Empty;
            this.individualWSText.Text = string.Empty;
            this.individualBRText.Text = string.Empty;
            this.individualBSText.Text = string.Empty;
            this.patternWRText.Text = string.Empty;
            this.patternWSText.Text = string.Empty;
            this.patternBRText.Text = string.Empty;
            this.patternBSText.Text = string.Empty;
            this.sizeCombo.SelectedIndex = 0;
            this.depthCombo.SelectedIndex = 0;
            this.outputCheck.Checked = false;
            this.SetColors(new List<SocketBoxPatternColor>());
            this.SetDetails(new List<SocketBoxPatternDetail>());
            this.SetComments(new List<SocketBoxDetailComment>());
        }

        private void UpdatePanel(SocketBoxPattern pattern)
        {
            this.categoryCombo.SelectedValue = pattern.CategoryId;
            this.nameText.Text = pattern.Name;
            this.individualWRText.Text = pattern.IndividualWRDwgPath;
            this.individualWSText.Text = pattern.IndividualWSDwgPath;
            this.individualBRText.Text = pattern.IndividualBRDwgPath;
            this.individualBSText.Text = pattern.IndividualBSDwgPath;
            this.patternWRText.Text = pattern.PatternWRDwgPath;
            this.patternWSText.Text = pattern.PatternWSDwgPath;
            this.patternBRText.Text = pattern.PatternBRDwgPath;
            this.patternBSText.Text = pattern.PatternBSDwgPath;
            this.sizeCombo.SelectedItem = (BoxSize)pattern.SocketBoxSize;
            this.depthCombo.SelectedItem = pattern.SocketBoxDepth == null ? "NORMAL" : pattern.SocketBoxDepth;
            this.outputCheck.Checked = pattern.NeedCSV;
            this.SetDetails(pattern.DetailsList);
            this.SetColors(pattern.ColorsList);
        }

        private void SetDetails(List<SocketBoxPatternDetail> details)
        {
            this.detailGrid.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = details;
            this.detailGrid.DataSource = bs;

            if (details.Count > 0)
                this.detailGrid.Rows[0].Selected = true;
            else
                this.SetComments(new List<SocketBoxDetailComment>());
        }

        private void SetComments(List<SocketBoxDetailComment> comments)
        {
            this.commentGrid.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = comments;
            this.commentGrid.DataSource = bs;
        }

        private void SetColors(List<SocketBoxPatternColor> colors)
        {
            this.colorGrid.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = colors;
            this.colorGrid.DataSource = bs;
        }

        private void OnFileSelectButtonClick(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button == null)
                    throw new ApplicationException("Invalid button pushed.");

                var labelName = button.Name.Replace("Button", "Label");
                var label = this.mainPanel.Controls[labelName];

                var dialog = new OpenFileDialog();
                dialog.Title = "Select " + label.Text.Replace(":", "") + " dwg for pattern";
                dialog.Filter = "DWG Files (.dwg)|*.dwg";

                if (button.Name.Contains("individual"))
                    dialog.InitialDirectory = @"C:\UnitWiring\Blocks\SocketBox\Individual";
                else
                    dialog.InitialDirectory = @"C:\UnitWiring\Blocks\SocketBox\Pattern";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var textName = button.Name.Replace("Button", "Text");
                var text = this.mainPanel.Controls[textName];
                text.Text = dialog.FileName;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void addColorButton_Click(object sender, EventArgs e)
        {
            var colors = (this.colorGrid.DataSource as BindingSource).DataSource as List<SocketBoxPatternColor>;
            if (colors == null)
                colors = new List<SocketBoxPatternColor>();

            var dialog = new AddOtherColorForm(this.patternId, colors);
            if (dialog.ShowDialog() != DialogResult.OK)
                return;



            colors.Add(dialog.NewColor);
            this.SetColors(colors);
        }

        private void detailGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.commentGrid.RowsRemoved -= this.commentGrid_RowsRemoved;

                var detail = this.detailGrid.Rows[e.RowIndex].DataBoundItem as SocketBoxPatternDetail;
                this.SetComments(detail.CommentsList);

                this.commentGrid.RowsRemoved += this.commentGrid_RowsRemoved;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void addEquipmentButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                EquipmentSelectionDialog.Instance.ShowDialog(this);
                if (EquipmentSelectionDialog.SelectedEquipment == null)
                    return;

                var details = (this.detailGrid.DataSource as BindingSource).DataSource as List<SocketBoxPatternDetail>;
                if (details == null)
                    throw new ApplicationException("Failed to get socket box pattern details data of grid.");

                var detail = new SocketBoxPatternDetail();
                detail.PatternId = this.patternId;
                detail.EquipmentId = EquipmentSelectionDialog.SelectedEquipment.Id;
                detail.Equipment = EquipmentSelectionDialog.SelectedEquipment;
                details.Add(detail);

                this.SetDetails(details);
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

        private void detailGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (this.detailGrid.Rows.Count > 0)
                    return;

                this.SetComments(new List<SocketBoxDetailComment>());
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void addCommentButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                TextSelectionDialog.Instance.ShowDialog(this);
                if (TextSelectionDialog.SelectedComment == null)
                    return;

                var comments = (this.commentGrid.DataSource as BindingSource).DataSource as List<SocketBoxDetailComment>;
                if (comments == null)
                    throw new ApplicationException("Failed to get socket box pattern details data of grid.");

                var comment = new SocketBoxDetailComment();
                comment.PatternId = this.patternId;
                comment.CommentId = TextSelectionDialog.SelectedComment.Id;
                comment.Comment = this.ConvertToComment(TextSelectionDialog.SelectedComment);
                comments.Add(comment);

                this.SetComments(comments);

                if (this.detailGrid.SelectedRows.Count == 0)
                    throw new ApplicationException("Failed to get selected detail data.");

                var detail = this.detailGrid.SelectedRows[0].DataBoundItem as SocketBoxPatternDetail;
                if (detail == null)
                    throw new ApplicationException("Failed to get selected detail data.");

                detail.CommentsList = comments;
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

        private Comment ConvertToComment(Comment uiComment)
        {
            var comment = new Comment();
            comment.Id = uiComment.Id;
            comment.Text = uiComment.Text;
            return comment;
        }

        private void commentGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (this.detailGrid.SelectedRows.Count == 0)
                    return;

                var detail = this.detailGrid.SelectedRows[0].DataBoundItem as SocketBoxPatternDetail;
                if (detail == null)
                    throw new ApplicationException("Failed to get selected detail data.");

                if (this.commentGrid.DataSource == null)
                {
                    detail.CommentsList.Clear();
                    return;
                }

                var comments = (this.commentGrid.DataSource as BindingSource).DataSource as List<SocketBoxDetailComment>;
                if (comments == null)
                    throw new ApplicationException("Failed to get detail comment data.");

                detail.CommentsList = comments;
            }
            catch (Exception exp)
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

                var pattern = new SocketBoxPattern();
                pattern.Id = this.patternId;
                pattern.Name = this.nameText.Text;
                pattern.IndividualWRDwgPath = this.individualWRText.Text;
                pattern.IndividualWSDwgPath = this.individualWSText.Text;
                pattern.IndividualBRDwgPath = this.individualBRText.Text;
                pattern.IndividualBSDwgPath = this.individualBSText.Text;
                pattern.PatternWRDwgPath = this.patternWRText.Text;
                pattern.PatternWSDwgPath = this.patternWSText.Text;
                pattern.PatternBRDwgPath = this.patternBRText.Text;
                pattern.PatternBSDwgPath = this.patternBSText.Text;
                pattern.SocketBoxSize = (int)this.sizeCombo.SelectedValue;
                pattern.SocketBoxDepth = this.depthCombo.SelectedItem.ToString();
                pattern.NeedCSV = this.outputCheck.Checked;
                pattern.CategoryId = (int)this.categoryCombo.SelectedValue;

                if (this.detailGrid.Rows.Count == 0)
                    pattern.DetailsList = new List<SocketBoxPatternDetail>();
                else
                    pattern.DetailsList = (this.detailGrid.DataSource as BindingSource).DataSource as List<SocketBoxPatternDetail>;

                pattern.ColorsList = (this.colorGrid.DataSource as BindingSource).DataSource as List<SocketBoxPatternColor>;

                int id;
                using (var service = new SocketPlanService())
                {
                    id = service.RegisterSocketBoxPattern(pattern, Environment.MachineName);
                }

#if !DEBUG
                var userName = Properties.Settings.Default.ServerUserName;
                var password = Properties.Settings.Default.ServerPassword;
                var source = Paths.GetServerSystemDirectory();
                MasterFileLoader.Authorize(source, userName, password);
#endif

                this.CopyToServer(pattern);
                this.IncrementMasterFileVersion();

                this.UpdateTree();
                this.SelectPatternNode(id);

                UnitWiring.Masters.UpdateSocketBoxPatterns();

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
            if (string.IsNullOrEmpty(this.nameText.Text))
            {
                MessageDialog.ShowWarning("Please input name.");
                return false;
            }

            return true;
        }

        private void SelectCategoryNode(int categoryId)
        {
            foreach (TreeNode parent in this.treeView.Nodes)
            {
                var category = parent.Tag as SocketBoxCategory;
                if (category == null)
                    continue;

                if (category.Id == patternId)
                {
                    this.treeView.SelectedNode = parent;
                    return;
                }
            }
        }

        private void SelectPatternNode(int patternId)
        {
            foreach (TreeNode parent in this.treeView.Nodes)
            {
                var category = parent.Tag as SocketBoxCategory;
                if (category == null)
                    continue;

                foreach (TreeNode child in parent.Nodes)
                {
                    var pattern = child.Tag as SocketBoxPattern;
                    if (pattern == null)
                        continue;

                    if (pattern.Id == patternId)
                    {
                        this.treeView.SelectedNode = child;
                        return;
                    }
                }
            }
        }

        private void CopyToServer(SocketBoxPattern pattern)
        {
            this.CopyToServer(pattern.IndividualWRDwgPath);
            this.CopyToServer(pattern.IndividualWRDwgPath);
            this.CopyToServer(pattern.IndividualWSDwgPath);
            this.CopyToServer(pattern.IndividualBRDwgPath);
            this.CopyToServer(pattern.IndividualBSDwgPath);
            this.CopyToServer(pattern.PatternWRDwgPath);
            this.CopyToServer(pattern.PatternWSDwgPath);
            this.CopyToServer(pattern.PatternBRDwgPath);
            this.CopyToServer(pattern.PatternBSDwgPath);
        }

        private void CopyToServer(string localPath)
        {
            if (string.IsNullOrEmpty(localPath))
                return;

            if (!localPath.Contains(Settings.Default.LocalSystemDirectory))
                throw new ApplicationException("Please set dwg path contain '" + Settings.Default.LocalSystemDirectory + "'");

            var serverPath = localPath.Replace(Settings.Default.LocalSystemDirectory, Paths.GetServerSystemDirectory());
            File.Copy(localPath, serverPath, true);
        }

        private void IncrementMasterFileVersion()
        {
            var directory = Paths.GetServerSystemDirectory();
            var filePath = directory + MasterFileLoader.VERSION_TEXT;
            var version = MasterFileLoader.GetMasterVersion(directory);
            version++;

            using (var writer = new StreamWriter(filePath, false, Encoding.GetEncoding("Shift_JIS")))
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

                if (e.Node.Level != 0)
                {
                    MessageDialog.ShowWarning("Cannot change pattern name on tree view.");
                    e.CancelEdit = true;
                    return;
                }

                var category = e.Node.Tag as SocketBoxCategory;
                if (category == null)
                {
                    category = new SocketBoxCategory();
                    category.Id = 0;
                    category.SortOrder = this.treeView.Nodes.Count;
                }

                category.Name = e.Label;

                int id;
                using (var service = new SocketPlanService())
                {
                    id = service.RegisterSocketBoxCategory(category);
                }

                category.Id = id;
                this.UpdateCategoryCombo();

                UnitWiring.Masters.UpdateSocketBoxPatterns();

                MessageDialog.ShowInformation(this, "Updated category successfully.");

                this.treeView.AfterLabelEdit += treeView_AfterLabelEdit;
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

        private void newPatternButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearPanel();
                this.saveButton.Enabled = true;

                this.patternId = 0;
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                TextSelectionDialog.DisposeInstance();
                EquipmentSelectionDialog.DisposeInstance();
            }
            catch (Exception exp)
            {
                MessageDialog.ShowError(exp);
            }
        }
    }
}
