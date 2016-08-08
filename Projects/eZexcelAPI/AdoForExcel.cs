using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;

using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;


namespace eZexcelAPI
{
	
	/// <summary>
	/// 利用ADO.NET连接Excel数据库，并执行相应的操作：
	/// 创建表格，读取数据，写入数据，获取工作簿中的所有工作表名称。
	/// </summary>
	/// <remarks></remarks>
	public class AdoForExcel
	{
		
		/// <summary>
		/// 创建对Excel工作簿的连接
		/// </summary>
		/// <param name="ExcelWorkbookPath">要进行连接的Excel工作簿的路径</param>
		/// <returns>一个OleDataBase的Connection连接，此连接还没有Open。</returns>
		/// <remarks></remarks>
        public static OleDbConnection ConnectToExcel(string ExcelWorkbookPath)
		{
			string strConn = string.Empty;
			if (ExcelWorkbookPath.EndsWith("xls"))
			{
				
				strConn = "Provider=Microsoft.Jet.OLEDB.4.0; " +
					"Data Source=" + ExcelWorkbookPath + "; " +
					"Extended Properties=\'Excel 8.0;IMEX=1\'";
				
			}
			else if (ExcelWorkbookPath.EndsWith("xlsx") || ExcelWorkbookPath.EndsWith("xlsb"))
			{
				
				strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
					"Data Source=" + ExcelWorkbookPath + ";" +
					"Extended Properties=\"Excel 12.0;HDR=YES\"";
			}
            OleDbConnection conn = new OleDbConnection(strConn);
			return conn;
		}
		
		/// <summary>
		/// 从对于Excel的数据连接中获取Excel工作簿中的所有工作表
		/// </summary>
		/// <param name="conn"></param>
		/// <returns>如果此连接不是连接到Excel数据库，则返回Nothing</returns>
		/// <remarks></remarks>
		public static string[] GetSheetsName(OleDbConnection conn)
		{
			//如果连接已经关闭，则先打开连接
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			if (ConnectionSourceValidated(conn))
			{
				//获取工作簿连接中的每一个工作表，
				//注意下面的Rows属性返回的并不是Excel工作表中的每一行，而是Excel工作簿中的所有工作表。
				DataRowCollection Tables = conn.GetSchema("Tables").Rows;
				//
				string[] sheetNames = new string[Tables.Count - 1 + 1];
				for (int i = 0; i <= Tables.Count - 1; i++)
				{
					//注意这里的表格Table是以DataRow的形式出现的。
					DataRow Tb = Tables[i];
					object Tb_Name = Tb["TABLE_NAME"];
					sheetNames[i] = Tb_Name.ToString();
				}
				return sheetNames;
			}
			else
			{
				MessageBox.Show("未正确连接到Excel数据库!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}
		
		/// <summary>
		/// 创建一个新的Excel工作表，并向其中插入一条数据
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="TableName">要新创建的工作表名称</param>
		/// <remarks></remarks>
		public static void CreateNewTable(OleDbConnection conn, string TableName)
		{
			//如果连接已经关闭，则先打开连接
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			if (ConnectionSourceValidated(conn))
			{
				using (OleDbCommand ole_cmd = conn.CreateCommand())
				{
					
					//----- 生成Excel表格 --------------------
					//要新创建的表格不能是在Excel工作簿中已经存在的工作表。
					ole_cmd.CommandText = "CREATE TABLE CustomerInfo ([" + TableName + "] VarChar,[Customer] VarChar)";
					try
					{
						//在工作簿中创建新表格时，Excel工作簿不能处于打开状态
						ole_cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						MessageBox.Show("创建Excel文档 " + TableName + "失败，错误信息： " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				
			}
			else
			{
				MessageBox.Show("未正确连接到Excel数据库!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}
		
		/// <summary>
		/// 向Excel工作表中插入一条数据，此函数并不通用，不建议使用
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="TableName">要插入数据的工作表名称</param>
		/// <param name="FieldName">要插入到的字段</param>
		/// <param name="Value">实际插入的数据</param>
		/// <remarks></remarks>
		public static void InsertToTable(OleDbConnection conn, string TableName, string FieldName, object Value)
		{
			//如果连接已经关闭，则先打开连接
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			if (ConnectionSourceValidated(conn))
			{
				using (OleDbCommand ole_cmd = conn.CreateCommand())
				{
					//在插入数据时，字段名必须是数据表中已经有的字段名，插入的数据类型也要与字段下的数据类型相符。
					
					try
					{
						ole_cmd.CommandText = "insert into [" + TableName + ("$](" + FieldName) + ") values(\'" + System.Convert.ToString(Value) + "\')";
						//这种插入方式在Excel中的实时刷新的，也就是说插入时工作簿可以处于打开的状态，
						//而且这里插入后在Excel中会立即显示出插入的值。
						ole_cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						MessageBox.Show("数据插入失败，错误信息： " + ex.Message);
						return;
					}
				}
				
			}
			else
			{
				MessageBox.Show("未正确连接到Excel数据库!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}
		
		/// <summary>
		/// 读取Excel工作簿中的数据
		/// </summary>
		/// <param name="conn">OleDB的数据连接</param>
		/// <param name="SheetName">要读取的数据所在的工作表</param>
		/// <param name="FieldName">在读取的字段</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string[] GetDataFromExcel(OleDbConnection conn, string SheetName, string FieldName)
		{
			//如果连接已经关闭，则先打开连接
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			if (ConnectionSourceValidated(conn))
			{
				//创建向数据库发出的指令
				OleDbCommand olecmd = conn.CreateCommand();
				//类似SQL的查询语句这个[Sheet1$对应Excel文件中的一个工作表]
				//如果要提取Excel中的工作表中的某一个指定区域的数据，可以用："select * from [Sheet3$A1:C5]"
				olecmd.CommandText = "select * from [" + SheetName + "$]";
				
				//创建数据适配器——根据指定的数据库指令
				OleDbDataAdapter Adapter = new OleDbDataAdapter(olecmd);
				//创建一个数据集以保存数据
				DataSet dtSet = new DataSet();
				//将数据适配器按指令操作的数据填充到数据集中的某一工作表中（默认为“Table”工作表）
				Adapter.Fill(dtSet);
				//其中的数据都是由 "select * from [" + SheetName + "$]"得到的Excel中工作表SheetName中的数据。
				int intTablesCount = dtSet.Tables.Count;
				//索引数据集中的第一个工作表对象
				System.Data.DataTable DataTable = dtSet.Tables[0]; // conn.GetSchema("Tables")
				//工作表中的数据有8列9行(它的范围与用Worksheet.UsedRange所得到的范围相同。
				//不一定是写有数据的单元格才算进行，对单元格的格式，如底纹，字号等进行修改的单元格也在其中。)
				int intRowsInTable = DataTable.Rows.Count;
				int intColsInTable = DataTable.Columns.Count;
				//提取每一行数据中的“成绩”数据
				string[] Data = new string[intRowsInTable - 1 + 1];
				for (int i = 0; i <= intRowsInTable - 1; i++)
				{
					Data[i] = System.Convert.ToString(DataTable.Rows[i][FieldName].ToString());
				}
				return Data;
			}
			else
			{
				MessageBox.Show("未正确连接到Excel数据库!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}
		
		//私有函数
		/// <summary>
		/// 验证连接的数据源是否是Excel数据库
		/// </summary>
		/// <param name="conn"></param>
		/// <returns>如果是Excel数据库，则返回True，否则返回False。</returns>
		/// <remarks></remarks>
		private static bool ConnectionSourceValidated(OleDbConnection conn)
		{
			//考察连接是否是针对于Excel文档的。
			string strDtSource = conn.DataSource; //"C:\Users\Administrator\Desktop\测试Visio与Excel交互\数据.xlsx"
			string strExt = Path.GetExtension(strDtSource);
			if (strExt ==".xlsx" || strExt ==".xls" || strExt ==".xlsb")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
	} //AdoForExcel
}
