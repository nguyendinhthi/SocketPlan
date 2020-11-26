using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    /// <summary>
    /// スイッチシリアル選択フォーム。
    /// </summary>
    public partial class TextSelectForSwitchSerialFrom : TextSelectForAttributeForm
    {

        public TextSelectForSwitchSerialFrom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 左側のカテゴリーリストに表示するデータを設定する。
        /// </summary>
        protected override void LoadMasters()
        {
            CommentCategory category = UnitWiring.Masters.CommentCategories.Find(p => p.Id == Const.CommentCategoryId.SerialForUnitWiringOnly);
            if (category != null)
            {
                ListViewItem item = new ListViewItem();
                item.Text = category.Name;
                item.Tag = category;
                this.categoryListView.Items.Add(item);
            }
        }
    }
}
