using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using eZstd.Miscellaneous;

namespace eZstd.UserControls
{
    /// <summary>
    /// 自定义控件：DataGridView，向其中增加了：插入行、删除行、显示行号等功能.
    /// 此控件不支持表格内容的复制粘贴，如果要用此功能，请用其派生类<see cref="eZDataGridViewPaste"/> 
    /// </summary>
    /// <remarks></remarks>
    public class eZDataGridView : DataGridView
    {
        #region   --- Properties

        /// <summary> 是否显示表格的行号 </summary>
        [Browsable(true), DefaultValue(true), Category("表格"),Description("是否显示表格的行号")]
        public bool ShowRowNumber { get; set; }

        /// <summary> 是否响应键盘的 delete 键，以在按下此键时将选择的单元格的内容删除 </summary>
        [Browsable(true), DefaultValue(true), Category("表格"),Description("是否响应键盘的 delete 键，以在按下此键时将选择的单元格的内容删除")]
        public bool KeyDelete { get; set; }

        /// <summary> 是否响应键盘的 Ctrl+ V 键，以在按下此组合键时将剪切板中的内容粘贴到表格中 </summary>
        [Browsable(true), DefaultValue(true), Category("表格"),Description("是否响应键盘的 Ctrl+ V 键，以在按下此组合键时将剪切板中的内容粘贴到表格中")]
        public bool SupportPaste { get; set; }

        /// <summary>  <see cref="ManipulateRows"/> 属性只能被设置一次，当多次设置时，忽略后面的设置操作  </summary>
        private bool _manipulateRowsBeenSet = false;
        private bool _manipulateRows = false;
        /// <summary> 在表格中右键时，弹出菜单，以进行数据行的删除、插入等操作 </summary>
        [Browsable(true), DefaultValue(true), Category("表格"),Description("在表格中右键时，弹出菜单，以进行数据行的删除、插入等操作")]
        public bool ManipulateRows
        {
            get { return _manipulateRows; }
            set
            {
                if (!_manipulateRowsBeenSet && value == true)
                {
                    InitializeUIMenu();
                    //
                    _manipulateRows = true;
                    _manipulateRowsBeenSet = true;
                }
            }
        }

        #endregion

        #region   --- 构造函数 与 控件的初始属性

        /// <summary>
        /// 构造函数
        /// </summary>
        public eZDataGridView()
        {
            InitializeComponent();
            // this.AllowUserToAddRows = true;
        }

        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            // 与 显示行号 相关的事件
            this.RowsAdded += new DataGridViewRowsAddedEventHandler(myDataGridView_RowsNumberChanged);
            this.RowsRemoved += new DataGridViewRowsRemovedEventHandler(myDataGridView_RowsNumberChanged);
            this.RowsAdded += new DataGridViewRowsAddedEventHandler(RowsResizable);
            this.RowStateChanged += new DataGridViewRowStateChangedEventHandler(myDataGridView_RowStateChanged);

            // 与 Delete 或 Ctrl+V 键盘按下相关的事件
            this.KeyDown += new KeyEventHandler(myDataGridView_KeyDown);

            ((ISupportInitialize)this).BeginInit();
            this.SuspendLayout();

            //
            //myDataGridView
            //
            this.ColumnHeadersHeight = 25;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowTemplate.Height = 23;
            this.RowTemplate.Resizable = DataGridViewTriState.False;
            this.Size = new Size(346, 110);

            ((ISupportInitialize)this).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        #region   ---  各种事件的响应

        /// <summary> 键盘按键按下 </summary>
        private void myDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDelete)
            {
                DeleteSelectedValue(e);
            }
            if (SupportPaste)
            {
                PasteFromClipBoard(e);
            }
        }

        #endregion

        // * -------------------------------------------------------------------

        #region   ---  显示行号 ： ShowRowNumber 属性

        /// <summary>
        /// 行数改变时的事件：显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void myDataGridView_RowsNumberChanged(object sender, dynamic e)
        {
            if (!ShowRowNumber) return;
            int longRow;
            for (longRow = e.RowIndex + e.RowCount - 1;
                longRow <= this.Rows.GetLastRow(DataGridViewElementStates.Displayed);
                longRow++)
            {
                this.Rows[longRow].HeaderCell.Value = (longRow + 1).ToString();
            }
        }

        /// <summary> 设置新添加的一行的Resizable属性为False </summary>
        /// <remarks></remarks>
        private void RowsResizable(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!ShowRowNumber) return;
            this.Rows[e.RowIndex].Resizable = DataGridViewTriState.False;
        }

        private void myDataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (!ShowRowNumber) return;
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        #endregion

        #region   ---  单元格数据的删除 ： KeyDelete 属性

        /// <summary> 如果按下 Delete 键，则将选择的单元格的内容删除 </summary>
        private void DeleteSelectedValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            { // 删除选择的单元格中的数据
                foreach (DataGridViewCell c in this.SelectedCells)
                {
                    c.Value = null;
                }
            }
        }

        #endregion

        #region   ---  右键菜单进行数据行的插入与删除 ： ManipulateRows 属性

        #region   --- 与右键菜单相关的控件

        private Container components;
        private ToolStripMenuItem ToolStripMenuItemInsert;
        private ToolStripMenuItem ToolStripMenuItemRemove;
        private ContextMenuStrip CMS_DeleteRows;
        private ToolStripMenuItem ToolStripMenuItemRemoveRows;
        private ContextMenuStrip CMS_RowHeader;

        #endregion

        /// <summary> 激活与右键菜单相关的控件，以及与数据行插入、删除相关的事件 </summary>
        private void InitializeUIMenu()
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

        #region   ---  数据行的插入与删除

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

        #endregion

        #region   ---  从剪切板粘贴数据 ： SupportPaste 属性

        /// <summary>
        /// 如下按下Ctrl+V，则将表格中的数据粘贴到DataGridView控件中
        /// </summary>
        /// <remarks>DataGridView表格的索引：行号：表头为-1，第一行为0，列号：表示行编号的列为-1，第一个数据列的列号为0.
        /// DataGridView.Rows.Count与DataGridView.Columns.Count均只计算数据区域，而不包含表头与列头。</remarks>
        private void PasteFromClipBoard(KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.V)
            {
                var a = this.SelectedCells;
                var count = a.Count;

                if (count != 1)
                {
                    MessageBox.Show("请选择某一个单元格，来作为粘贴的起始位置。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataGridViewCell startCell = this.SelectedCells[0];
                PasteFromTable(startCell.RowIndex, startCell.ColumnIndex);
            }
        }

        /// <summary> 将表格中的数据粘贴到DataGridView控件中（通过先添加全部行，再为添加的行赋值的方式） </summary>
        /// <param name="startRow">粘贴的起始单元格的行位置</param>
        /// <param name="startCol">粘贴的起始单元格的列位置</param>
        /// <remarks>DataGridView表格的索引：行号：表头为-1，第一行为0，列号：表示行编号的列为-1，第一个数据列的列号为0.
        /// DataGridView.Rows.Count与DataGridView.Columns.Count均只计算数据区域，而不包含表头与列头。总行数包括最后一行空数据行。</remarks>
        private void PasteFromTable(int startRow, int startCol)
        {
            string pastTest = Clipboard.GetText();

            if (string.IsNullOrEmpty(pastTest))
            { return; }

            // excel中是以"空格"和"换行"来当做字段和行，所以用"\r\n"来分隔，即"回车+换行"
            string[] lines = pastTest.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int writeRowsCount = lines.Length; //要写入多少行数据
            int writeColsCount = lines[0].Split('\t').Length; //要写入的每一行数据中有多少列
                                                              //
            int endRow = startRow + writeRowsCount - 1; // 要修改的最后一行的行号

            int rowsToAdd = endRow + 2 - this.Rows.Count;// 说明要额外添加这么多行才能放置要粘贴进来的数据

            if (rowsToAdd > 0)
            {
                if (DataSource is IBindingList)
                {
                    IBindingList ds = (IBindingList)this.DataSource;

                    // 对于 DataSource 绑定到 IBindingList 时，不能直接对DataGridView添加行，而是通过对于绑定的 IBindingList 进行添加来实现的。
                    // 因为 IBindingList 中每一个新添加的元素都要符合绑定的类的构造形式。
                    if (startRow == Rows.Count - 1)
                    {
                        // 当DataGridView的最后一行（AddNew的那一行）被选中时，执行BindingList.AddNew方法会出现报错。
                        // 所以这里进行判断，并且当其被选中时就先取消这一行的选择。
                        CurrentCell = null;
                    }

                    for (int i = 0; i < rowsToAdd; i++)
                    {
                        // BindingList.AddNew方法会触发其 AddingNew 事件，用户必须手动在此事件中定义要实例化的初始变量。
                        ds.AddNew();
                    }

                    CurrentCell = Rows[startRow].Cells[startCol];
                }
                else
                {  // 直接添加数据行
                    this.Rows.Add(rowsToAdd);
                }

            }
            int endCol = 0; // 要修改的最后面的那一列的列号
            endCol = startCol + writeColsCount <= this.Columns.Count
                ? startCol + writeColsCount - 1
                : this.Columns.Count - 1;


            //  每一列的要进行检测的数据类型
            Type tp;
            Type[] checkedTypes = new Type[endCol - startCol + 1];

            for (int c = startCol; c <= endCol; c++)
            {
                tp = Columns[c].ValueType;
                // 如果某列的ValueType为Nullable<>的话，则要对其所指定的泛型进行检测，
                // 因为在为Rows[r].Cells[c].Value赋值时，字符"1.2"不能转换为float，而会被转换为null，
                // 但实际上1.2是一个合法的float值。所以这里要通过Convert.ChangeType来进行显式检验。
                checkedTypes[c - startCol] = Utils.GetNullableGenericArgurment(tp) ?? tp;
            }

            // 当前操作的单元格的坐标
            int rowIndex = 0, colIndex = 0;
            object value;
            try
            {
                // 数据赋值与检测
                string strline = "";
                string[] strs = null;
                for (rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                {
                    // 一条记录中的数据
                    strline = lines[rowIndex - startRow];
                    strs = strline.Split('\t'); //在每一行的单元格间，作为单元格的分隔的字符为"\t",即水平换行符

                    for (colIndex = startCol; colIndex <= endCol; colIndex++)
                    {
                        // Convert.ChangeType 用来检查字符所对应的值是否可以转换为对应字段列的数据类型，如果不能转换，则会报错。
                        value = !string.IsNullOrEmpty(strs[colIndex - startCol])
                            ? Convert.ChangeType(strs[colIndex - startCol], checkedTypes[colIndex - startCol])
                            : null;

                        // 在修改单元格数据时，即使添加的数据不符合此列字段的数据格式，也不会报错，而是会直接取消对于此单元格的赋值，转而继续进行下一个单元格的赋值操作。
                        this.Rows[rowIndex].Cells[colIndex].Value = value;

                        SetSelectedCellCore(colIndex, rowIndex, true);  // 选中此单元格
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"粘贴数据出错,出错的单元格为第 {rowIndex + 1} 行,第 {colIndex + 1} 列）");
                // DebugUtils.ShowDebugCatch(ex, $"粘贴数据出错,出错的单元格为第 {rowIndex + 1} 行,第 {colIndex + 1} 列）");
            }
        }

        #endregion
    }
}