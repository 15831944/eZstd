using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eZstd.UserControls
{
    /// <summary> 通过设计界面中的弹出菜单，来执行表格中记录行的插入、增加或删除等操作 </summary>
   public class eZDataGridViewUIAdd : eZDataGridView
    {

        #region   --- ControlFields

        private Container components;
        private ToolStripMenuItem ToolStripMenuItemInsert;
        private ToolStripMenuItem ToolStripMenuItemRemove;
        private ContextMenuStrip CMS_DeleteRows;
        private ToolStripMenuItem ToolStripMenuItemRemoveRows;
        private ContextMenuStrip CMS_RowHeader;

        #endregion

        #region   --- 构造函数 与 控件的初始属性

        /// <summary>
        /// 构造函数
        /// </summary>
        public eZDataGridViewUIAdd() : base()
        {
            InitializeComponent();
            // 
        }

        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            // 事件绑定
            this.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(myDataGridView_RowHeaderMouseClick);
            this.CellMouseClick += new DataGridViewCellMouseEventHandler(myDataGridView_CellMouseClick);
            
            //
            this.components = new Container();
            this.CMS_RowHeader = new ContextMenuStrip(this.components);
            this.ToolStripMenuItemInsert = new ToolStripMenuItem();
            this.ToolStripMenuItemInsert.Click += new EventHandler(this.InsertRow);
            this.ToolStripMenuItemRemove = new ToolStripMenuItem();
            this.ToolStripMenuItemRemove.Click += new EventHandler(RemoveOneRow);
            this.CMS_DeleteRows = new ContextMenuStrip(this.components);
            this.ToolStripMenuItemRemoveRows = new ToolStripMenuItem();
            this.ToolStripMenuItemRemoveRows.Click += new EventHandler(this.RemoveMultipleRows);
            this.CMS_RowHeader.SuspendLayout();
            this.CMS_DeleteRows.SuspendLayout();

            //
            //CMS_RowHeader
            //
            this.CMS_RowHeader.Items.AddRange(new ToolStripItem[]
            {this.ToolStripMenuItemInsert, this.ToolStripMenuItemRemove});
            this.CMS_RowHeader.Name = "CMS_RowHeader";
            this.CMS_RowHeader.ShowImageMargin = false;
            this.CMS_RowHeader.Size = new Size(76, 48);
            //
            //ToolStripMenuItemInsert
            //
            this.ToolStripMenuItemInsert.Name = "ToolStripMenuItemInsert";
            this.ToolStripMenuItemInsert.Size = new Size(75, 22);
            this.ToolStripMenuItemInsert.Text = "插入";
            //
            //ToolStripMenuItemRemove
            //
            this.ToolStripMenuItemRemove.Name = "ToolStripMenuItemRemove";
            this.ToolStripMenuItemRemove.Size = new Size(75, 22);
            this.ToolStripMenuItemRemove.Text = "移除";
            //
            //CMS_DeleteRows
            //
            this.CMS_DeleteRows.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItemRemoveRows });
            this.CMS_DeleteRows.Name = "CMS_RowHeader";
            this.CMS_DeleteRows.ShowImageMargin = false;
            this.CMS_DeleteRows.Size = new Size(112, 26);
            //
            //ToolStripMenuItemRemoveRows
            //
            this.ToolStripMenuItemRemoveRows.Name = "ToolStripMenuItemRemoveRows";
            this.ToolStripMenuItemRemoveRows.Size = new Size(111, 22);
            this.ToolStripMenuItemRemoveRows.Text = "删除所选行";
            //
            this.CMS_RowHeader.ResumeLayout(false);
            this.CMS_DeleteRows.ResumeLayout(false);

        }

        #endregion

        #region   ---  右键菜单的关联与显示

        private void myDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //如果是右击
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != this.Rows.Count - 1)  //  在选择中最后一行（用来新建一条数据的那一行）时，不弹出菜单。
                {
                    //如果行数只有一行
                    if (this.Rows.Count <= 1)
                    {
                        this.ToolStripMenuItemRemove.Enabled = false;
                    }
                    else
                    {
                        this.ToolStripMenuItemRemove.Enabled = true;
                    }


                    //选择右击项的那一行
                    this.ClearSelection();
                    this.Rows[e.RowIndex].Selected = true;
                    //显示菜单栏
                    CMS_RowHeader.Show();
                    CMS_RowHeader.Left = MousePosition.X;
                    CMS_RowHeader.Top = MousePosition.Y;
                }
            }
        }

        private void myDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // 如果是对单元格进行右击
            {
                int R = e.RowIndex;
                int C = e.ColumnIndex;
                if (R >= 0 & C >= 0)
                {
                    //显示菜单栏
                    if (this.SelectedRows.Count == 0 || this.Rows.Count < 2)
                    {
                        this.ToolStripMenuItemRemoveRows.Enabled = false;
                    }
                    else
                    {
                        this.ToolStripMenuItemRemoveRows.Enabled = true;
                    }
                    CMS_DeleteRows.Show();
                    CMS_DeleteRows.Left = MousePosition.X;
                    CMS_DeleteRows.Top = MousePosition.Y;
                }
            }
        }

        #endregion

        #region   ---  行的插入与删除

        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected void InsertRow(object sender, EventArgs e)
        {
            int SelectedIndex = this.SelectedRows[0].Index;

            // 对于 DataSource 绑定到 IBindingList 时，不能直接对DataGridView添加行，而是通过对于绑定的 IBindingList 进行添加来实现的。
            // 因为 IBindingList 中每一个新添加的元素都要符合绑定的类的构造形式。
            if (DataSource is IBindingList)
            {
                IBindingList ds = (IBindingList)this.DataSource;
                var n = ds.AddNew();  // 在末尾添加一个新的实例
                ds.Insert(SelectedIndex, n); // 将实例插入到集合指定位置
                ds.RemoveAt(ds.Count - 1);  // 将新添加到末尾的元素删除
            }
            else
            {
                this.Rows.Insert(SelectedIndex, 1);
            }
        }

        /// <summary>
        /// 移除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void RemoveOneRow(object sender, EventArgs e)
        {
            var Row = this.SelectedRows[0];
            if (Row.Index < this.Rows.Count - 1)
            {
                //当删除最后一行（不带数据，自动添加的行）时会报错：无法删除未提交的新行。
                this.Rows.Remove(Row);
            }
        }

        /// <summary>
        /// 移除多行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void RemoveMultipleRows(object sender, EventArgs e)
        {
            //下面的 For Each 是从下往上索引的，即前面的Row对象的index的值大于后面的Index的值
            foreach (DataGridViewRow Row in this.SelectedRows)
            {
                if (Row.Index < this.Rows.Count - 1)
                {
                    //当删除最后一行（不带数据，自动添加的行）时会报错：无法删除未提交的新行。
                    this.Rows.Remove(Row);
                }
            }
        }

        #endregion

    }
}
