using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DigestLib.Data.Sql;

namespace DigestLib.Data
{
	public class LibHelper
	{
		private LibHelper()
		{
		}
		public static DataTable GetLookup(string strFieldName)
		{
			return LibSql.GetDataTable(new LibSqlSelect("*", "tblLookup", "LoFieldName=" + LibSql.SqlText(strFieldName), "LoOrderBy"));
		}
		public static string ShowValues(LibFieldCollection coll)
		{
			string text = "";
			try
			{
				IEnumerator enumerator = coll.Keys.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text2 = Conversions.ToString(enumerator.Current);
					text = string.Concat(new string[]
					{
						text,
						text2,
						" :: ",
						coll[text2].Value,
						" <br >"
					});
				}
			}
			finally
			{
                //IEnumerator enumerator;
                //bool flag = enumerator is IDisposable;
                //if (flag)
                //{
                //    (enumerator as IDisposable).Dispose();
                //}
			}
			return text;
		}
		public static string GetTableAsString(DataTable tbl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Name = " + tbl.TableName);
			DataColumnCollection columns = tbl.Columns;
			try
			{
				IEnumerator enumerator = columns.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataColumn dataColumn = (DataColumn)enumerator.Current;
					stringBuilder.Append("Column = " + dataColumn.ColumnName);
                    //stringBuilder.Append(Conversions.ToDouble("Column = ") + -(dataColumn.AutoIncrement != false));
                    //stringBuilder.Append(Conversions.ToDouble("Column = ") + (double)dataColumn.ColumnMapping);
                    //stringBuilder.Append(Conversions.ToDouble("Column = ") + -(dataColumn.Unique != false));
					stringBuilder.Append("\n\n");
				}
			}
			finally
			{
                //IEnumerator enumerator;
                //bool flag = enumerator is IDisposable;
                //if (flag)
                //{
                //    (enumerator as IDisposable).Dispose();
                //}
			}
			try
			{
				IEnumerator enumerator2 = tbl.Rows.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator2.Current;
					string text = "";
					try
					{
						IEnumerator enumerator3 = columns.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							DataColumn dataColumn2 = (DataColumn)enumerator3.Current;
							text = Conversions.ToString(Operators.AddObject(text, Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(dataColumn2.ColumnName + "=", dataRow[dataColumn2]), ""), ";"), '\t'), "")));
						}
					}
					finally
					{
                        //IEnumerator enumerator3;
                        //bool flag = enumerator3 is IDisposable;
                        //if (flag)
                        //{
                        //    (enumerator3 as IDisposable).Dispose();
                        //}
					}
					stringBuilder.Append("\n\n\n" + text);
				}
			}
			finally
			{
                //IEnumerator enumerator2;
                //bool flag = enumerator2 is IDisposable;
                //if (flag)
                //{
                //    (enumerator2 as IDisposable).Dispose();
                //}
			}
			return stringBuilder.ToString();
		}
        public static void FillSection(WebControl con, DataTable tbl)
        { }
		public static LibFieldCollection ConvertRowToLibFieldColl(DataTable tbl, DataRow row)
		{
			LibFieldCollection wizFieldCollection = new LibFieldCollection();
			try
			{
				IEnumerator enumerator = tbl.Columns.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataColumn dataColumn = (DataColumn)enumerator.Current;
					wizFieldCollection.Add(dataColumn.ColumnName, Conversions.ToString(Operators.AddObject("", row[dataColumn.ColumnName])));
				}
			}
			finally
			{
                //IEnumerator enumerator;
                //bool flag = enumerator is IDisposable;
                //if (flag)
                //{
                //    (enumerator as IDisposable).Dispose();
                //}
			}
			return wizFieldCollection;
		}
		public static DataRow ConvertLibFieldCollToRow(LibFieldCollection coll, DataTable tbl)
		{
			DataRow dataRow = tbl.NewRow();
			try
			{
				IEnumerator<LibField> enumerator = coll.GetEnumerator();
				while (enumerator.MoveNext())
				{
					LibField current = enumerator.Current;
					bool flag = tbl.Columns.Contains(current.Name);
					if (flag)
					{
						dataRow[current.Name] = coll[current.Name].Value;
					}
				}
			}
			finally
			{
                //IEnumerator<LibField> enumerator;
                //bool flag = enumerator != null;
                //if (flag)
                //{
                //    enumerator.Dispose();
                //}
			}
			return dataRow;
		}
		public static bool IsNum(string str)
		{
			bool result;
			try
			{
				long.Parse(str);
				result = true;
				return result;
			}
			catch (Exception expr_0F)
			{
				ProjectData.SetProjectError(expr_0F);
				ProjectData.ClearProjectError();
			}
			result = false;
			return result;
		}
	}
}
