using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using DigestLib.Data.Sql;
using DigestLib.Data;

namespace DigestLib.Data
{
	public class LibDbLayer
	{
		private LibDbLayer()
		{
		}
		public static DataTable GetTable(string tableName)
		{
			return LibDbLayer.GetTable(tableName, "");
		}
		public static DataTable GetTable(string select, string tableName, string singleColumn, string singleColValue, string orderBy)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect(select, tableName, singleColumn + "=" + LibSql.SqlText(singleColValue), orderBy);
			bool flag = singleColValue.Length == 0;
			if (flag)
			{
				sqlSelect = new LibSqlSelect(select, tableName, singleColumn, orderBy);
			}
			return LibSql.GetDataTable(sqlSelect);
		}
		public static DataTable GetTable(string strSelect, string tableName, string strWhereFormat, string[] strWhereArguments, string strOrderBy)
		{
			int arg_08_0 = 0;
			checked
			{
				int num = strWhereArguments.Length - 1;
				int num2 = arg_08_0;
				while (true)
				{
					int arg_3C_0 = num2;
					int num3 = num;
					if (arg_3C_0 > num3)
					{
						break;
					}
					strWhereFormat = strWhereFormat.Replace("{" + num2.ToString() + "}", LibSql.SqlText(strWhereArguments[num2]));
					num2++;
				}
				LibSqlSelect sqlSelect = new LibSqlSelect(strSelect, tableName, strWhereFormat, strOrderBy);
				return LibSql.GetDataTable(sqlSelect);
			}
		}
		public static DataTable GetTable(string strSelect, string tableName, string strWhereFormat, ArrayList arrWhereArguments, string strOrderBy)
		{
			int arg_0B_0 = 0;
			checked
			{
				int num = arrWhereArguments.Count - 1;
				int num2 = arg_0B_0;
				while (true)
				{
					int arg_52_0 = num2;
					int num3 = num;
					if (arg_52_0 > num3)
					{
						break;
					}
					strWhereFormat = strWhereFormat.Replace("{" + num2.ToString() + "}", LibSql.SqlText(arrWhereArguments[num2].ToString() + ""));
					num2++;
				}
				LibSqlSelect sqlSelect = new LibSqlSelect(strSelect, tableName, strWhereFormat, strOrderBy);
				return LibSql.GetDataTable(sqlSelect);
			}
		}
		public static DataTable GetTable(string select, string tableName, string singleColumn, string singleColValue)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect("*", tableName, singleColumn + "=" + LibSql.SqlText(singleColValue));
			return LibSql.GetDataTable(sqlSelect);
		}
		public static DataTable GetTable(string tableName, string where)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect("*", tableName, where);
			return LibSql.GetDataTable(sqlSelect);
		}
		public static DataTable GetTable(string tableName, int top)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect("*", tableName, top);
			return LibSql.GetDataTable(sqlSelect);
		}
		public static DataTable GetTable(string select, string tableName, string where)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect(select, tableName, where);
			return LibSql.GetDataTable(sqlSelect);
		}
		public static DataTable GetTable(string select, string tableName, int top)
		{
			LibSqlSelect sqlSelect = new LibSqlSelect(select, tableName, top);
			return LibSql.GetDataTable(sqlSelect);
		}
		public static string Insert(ControlCollection controlCollection, string tableName)
		{
			return LibDbLayer.Insert(false, controlCollection, tableName);
		}
		public static string Insert(bool checkSchema, ControlCollection controlCollection, string tableName)
		{
			LibFieldCollection wizFieldColl = new LibFieldCollection(controlCollection);
			return LibSql.GetInsertSQL(checkSchema, tableName, wizFieldColl);
		}
		public static string InsertSql(bool checkSchema, LibFieldCollection fieldColl, string tableName)
		{
			return LibSql.GetInsertSQL(checkSchema, tableName, fieldColl);
		}
		public static string Insert(bool checkSchema, LibFieldCollection fieldColl, string tableName)
		{
			return LibSql.Insert(LibSql.GetInsertSQL(checkSchema, tableName, fieldColl)).ToString();
		}
		public static string Update(bool checkSchema, LibFieldCollection fieldColl, string tableName, string where)
		{
			return LibSql.Update(LibSql.GetUpdateSQL(checkSchema, tableName, fieldColl, where)).ToString();
		}
		public static string InsertReturnID(bool checkSchema, LibFieldCollection fieldColl, string tableName)
		{
			return LibSql.InsertAndReturnIdentity(LibSql.GetInsertSQL(checkSchema, tableName, fieldColl));
		}
		public static string GetNextOrderBy(string colName, string tableName, string where)
		{
			string text = string.Concat(new string[]
			{
				"SELECT ",
				colName,
				" + 1 AS ",
				colName,
				" FROM ",
				tableName,
				" WHERE ",
				where
			});
			bool flag = where.Length == 0;
			if (flag)
			{
				text = string.Concat(new string[]
				{
					"SELECT ",
					colName,
					" + 1 AS ",
					colName,
					" FROM ",
					tableName
				});
			}
			text = LibSql.GetField(text);
			flag = (text == null || text.Length == 0);
			string result;
			if (flag)
			{
				result = "1";
			}
			else
			{
				result = text;
			}
			return result;
		}
		public static string SQLText(string inputText)
		{
			return LibSql.SqlText(inputText);
		}
		public static int Execute(string strUpdate)
		{
			return LibSql.ExecuteNonQuery(strUpdate);
		}
		public static int DeleteRows(string tableName, string where)
		{
			return LibSql.ExecuteNonQuery("DELETE FROM " + tableName + " WHERE " + where);
		}
		public static string InsertReturnID(string strInsertQuery)
		{
			return LibSql.InsertAndReturnIdentity(strInsertQuery);
		}
		public static DataTable GetTableFromSql(string sql)
		{
			return LibSql.GetDataTable(sql);
		}
		public static string GetField(string strSQL)
		{
			return LibSql.GetField(strSQL);
		}
		public static string GetField(string select, string tableName, string where)
		{
			return LibSql.GetField(new LibSqlSelect(select, tableName, where));
		}
	}
}
