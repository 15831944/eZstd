using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace eZstd.Drawing
{
    /// <summary>
    /// 与 GDI+ 绘图相关的测试
    /// </summary>
    public partial class DrawShapesTest : Form
    {
        public DrawShapesTest()
        {
            InitializeComponent();
        }

        #region ---   显示矢量图
        private Metafile metafile1;
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics.EnumerateMetafileProc metafileDelegate = MetafileCallback;
            metafile1 = new Metafile(@"F:\ProgrammingCases\GitHubProjects\1.emf");

            // 遍历矢量图中的各个图元并有选择性的显示在 pictureBox1 中， 显示的矢量图的左上角点的位置为 destPoint
            pictureBox1.CreateGraphics().EnumerateMetafile(metafile1, destPoint: new Point(0, 0), callback: metafileDelegate);
        }

        /// <summary>
        /// Provides a callback method for the EnumerateMetafile method.
        /// </summary>
        /// <param name="recordType">Member of the EmfPlusRecordType enumeration that specifies the type of metafile record.</param>
        /// <param name="flags">Set of flags that specify attributes of the record.</param>
        /// <param name="dataSize">Number of bytes in the record data.</param>
        /// <param name="data">Pointer to a buffer that contains the record data.</param>
        /// <param name="callbackData">Not used.</param>
        /// <returns></returns>
        private bool MetafileCallback(EmfPlusRecordType recordType,
           int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {
                // 将 data 所对应的内存指针所对应的数据从内存中复制出来
                // Copy the unmanaged record to a managed byte buffer  
                // that can be used by PlayRecord.
                dataArray = new byte[dataSize];
                // Copies data from a managed array to an unmanaged memory pointer, or from an unmanaged memory pointer to a managed array.
                Marshal.Copy(data, dataArray, 0, dataSize);
            }
            // 将 矢量图中的这一个小图元显示在 pictureBox1 的绘图面板 Graphics 中
            metafile1.PlayRecord(recordType, flags, dataSize, dataArray); // Plays an individual metafile record.
            return true;
        }
        #endregion
    }
}