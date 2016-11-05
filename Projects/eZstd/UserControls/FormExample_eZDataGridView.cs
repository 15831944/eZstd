using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace eZstd.UserControls
{
    /// <summary>
    ///  表格 eZDataGridView 的应用模板：全手动创建每一个数据列
    /// </summary>
    internal partial class Example_eZDataGridView : Form
    {
        private eZDataGridView _eZdgv;

        public Example_eZDataGridView()
        {
            InitializeComponent();
            Size = new Size(800, 600);

            //
            InitializeeZDataGridView();
            // 添加其他控件以测试 eZDataGridView 控件
            InitializeOtherControls();
        }

        #region ---   eZDataGridView 配置

        /// <summary> 与 DataGridView 进行绑定的数据源 </summary>
        private BindingList<Person> _persons;

        private void InitializeeZDataGridView()
        {
            // 创建一个表格并添加到窗口中
            _eZdgv = new eZDataGridView()
            {
                //
                Size = new Size(650, 550),
                Location = new Point(100, 0),

                // 基本配置
                KeyDelete = true,
                ManipulateRows = true,
                ShowRowNumber = true,
                SupportPaste = true,
                //
                AllowUserToAddRows = true,
                AutoGenerateColumns = false,
            };
            this.Controls.Add(_eZdgv);

            // 创建与表格相绑定的数据源
            _persons = new BindingList<Person>
            {
                AllowNew = true
            };

            ConstructeZDataGridView(_eZdgv);
        }

        private void ConstructeZDataGridView(eZDataGridView eZdgv)
        {
            eZdgv.DataSource = _persons;

            // 创建数据列并绑定到数据源 ----------------------------------------------

            // -------------------------
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Id";
            column.Name = "序号（只读）";
            eZdgv.Columns.Add(column);

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "名称";
            eZdgv.Columns.Add(column);

            // -------------------------
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(Gender));
            combo.DataPropertyName = "Gender";
            combo.Name = "性别";
            eZdgv.Columns.Add(combo);
            // 如果要设置对应单元格的值为某枚举项：combo.Item(combo.Index,行号).Value = Gender.Male;

            // -------------------------
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Age";
            column.Name = "年龄";
            column.ValueType = typeof(int); // 限定此列的数据类型必须为整数值
            eZdgv.Columns.Add(column);

            // -------------------------
            // Initialize and add a check box column.
            DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn()
            {
                Name = "Dead",
                DataPropertyName = "Dead",
                HeaderText = "死亡",
            };

            eZdgv.Columns.Add(columnCheck);
            // 如果要设置对应单元格的值为某枚举项：combo.Item(combo.Index,行号).Value = true;

            // -------------------------
            // 为DataGridView控件中添加一列，此列与DataSource没有任何绑定关系
            // 注意，添加此列后，DataGridView.DataSource的值并不会发生改变。
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn()
            {
                Name = "Details",
                HeaderText = "详细",
                Text = "View Details",
                // Use the Text property for the button text for all cells rather
                // than using each cell's value as the text for its own button.
                UseColumnTextForButtonValue = true,
            };
            eZdgv.Columns.Insert(0, buttonColumn);


            // 事件绑定 -------------------------------------------------------------
            _eZdgv.DataError += EZdgvOnDataError; // 响应表格中的数据类型不匹配等出错的情况
            _eZdgv.CellContentClick += EZdgvOnCellContentClick;  // 响应表格中的按钮按下事件
            _eZdgv.CurrentCellDirtyStateChanged += EZdgvOnCurrentCellDirtyStateChanged; // 在表格中Checkbox的值发生改变时立即作出响应

            // 如果数据行所对应的实例对象有无参数的构造函数，则AddingNew事件不是必须的。
            _persons.AddingNew += PersonsOnAddingNew;
        }
        
        #endregion

        #region ---   与 eZDataGridView 相关的事件处理

        private void EZdgvOnCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // 表头行的行号为 -1
            {
                string columnName = _eZdgv.Columns[e.ColumnIndex].Name;

                if (columnName == "Details") // 显示选中的行的数据信息
                {
                    DataGridViewRow r = _eZdgv.Rows[e.RowIndex];
                    Person p = r.DataBoundItem as Person;
                    if (p != null)
                    {
                        MessageBox.Show(p.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 在表格中Checkbox的值发生改变时立即作出响应.
        /// 如果你想要在用户点击复选框单元格时立即响应，你可以处理CellContentClick 事件，但是此事件会在单元格的值更新之前触发。
        /// </summary>
        private void EZdgvOnCurrentCellDirtyStateChanged(object sender, EventArgs eventArgs)
        {
            // IsCurrentCellDirty属性：Gets a value indicating whether the current cell has uncommitted changes.
            if (_eZdgv.IsCurrentCellDirty)
            {
                _eZdgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private int _id;
        private void PersonsOnAddingNew(object sender, AddingNewEventArgs e)
        {
            Person p = new Person(_id);
            p.Name = "输入名称";
            e.NewObject = p;
            _id += 1;
        }

        private void EZdgvOnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if ((e.Context & DataGridViewDataErrorContexts.Parsing) != 0)
            {
                MessageBox.Show("输入的数据不能转换为指定的数据类型！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                e.ThrowException = false;
            }
        }

        #endregion

        #region ---   与 eZDataGridView 的数据源相关的类型

        /// <summary> DataGridView中每一行数据所对应的类 </summary>
        private class Person
        {
            public int Id { get; }
            public string Name { get; set; }
            public int Age { get; set; }
            public Gender Gender { get; set; }
            private bool _dead;

            public bool Dead
            {
                get { return _dead; }
                set
                {
                    _dead = value;
                    MessageBox.Show($"死亡情况 : {value.ToString()} \n\r\n\r" +
                        "此事件在表格中数据列所绑定的属性中set方法中执行。 \n\r\n\r" +
                        "如果你想要在用户点击复选框单元格时立即响应，可以在Datagridview.CurrentCellDirtyStateChanged事件中将更改进行dataGridView1.CommitEdit提交（此时绑定的属性值就已经更新了），但是不响应CellValueChanged 事件，而是在此DataGridViewCheckBoxCell绑定的属性Property的Set方法中去进行操作。这会更符合类的封装设计。注意如果在Set方法中设置了其他绑定到Datagridview的属性的值，记得用Datagridview.Refresh()方法刷新UI界面。");
                }
            }

            // 即使在DataGridView中为此属性添加了一个数据列，此列中也不会显示出任何数据。但是实际的数据还是保存在其DataSource中的。
            [Browsable(false)]
            public DateTime Birthday { get; set; }


            /// <summary> 这里Id值的设计并不严谨，只是为了说明如果在表格中应用只读属性 </summary>
            /// <param name="id"></param>
            public Person(int id)
            {
                Id = id;
            }

            public override string ToString()
            {
                return $"名称:{Name}\r\n年龄:{Age}\r\n性别:{Gender}";
            }
        }

        private enum Gender
        {
            Male,
            Female,
            Unknown,
        }

        #endregion

        #region ---   测试 eZDataGridView 的性能

        private void InitializeOtherControls()
        {
            // 添加行
            Button btn = new Button()
            {
                Location = new Point(0, 0),
                Text = "添加行",

            };
            Controls.Add(btn);
            btn.Click += TestAddRows;
            // 检测数据
            btn = new Button()
            {
                Location = new Point(0, 30),
                Text = "表格信息",
            };
            Controls.Add(btn);
            btn.Click += TestTableInfos;
        }

        private void TestAddRows(object sender, EventArgs eventArgs)
        {
            _persons.AddNew();  // 此方法通过触发 AddinNew事件来进行实例的添加
        }

        private void TestTableInfos(object sender, EventArgs eventArgs)
        {
            string info = $"界面表格中共有数据{_eZdgv.Rows.Count}行;\n\r" +
                          $"与之绑定的BindingList集合中共有元素{_persons.Count}个";
            MessageBox.Show(info);
        }
        #endregion
    }
}