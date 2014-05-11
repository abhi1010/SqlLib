using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Configuration;
using System.Text;
namespace DigestLib.Data.Sql
{
	public sealed class LibSqlSelect
	{
		private string _Field;
		private string _Table;
		private string _Where;
		private string _OrderBy;
		private string _GroupBy;
		private string _Having;
		private int _Top;
		private int _Offset;
		private static string _ProviderName;
		public string ProviderName
		{
			get
			{
				bool flag = Operators.CompareString(LibSqlSelect._ProviderName, "", false) == 0;
				if (flag)
				{
					LibSqlSelect._ProviderName = ConfigurationManager.ConnectionStrings[0].ProviderName;
				}
				return LibSqlSelect._ProviderName;
			}
			set
			{
				LibSqlSelect._ProviderName = value;
			}
		}
		public string Field
		{
			get
			{
				return this._Field;
			}
			set
			{
				this._Field = value;
			}
		}
		public string Table
		{
			get
			{
				return this._Table;
			}
			set
			{
				this._Table = value;
			}
		}
		public string Where
		{
			get
			{
				return this._Where;
			}
			set
			{
				this._Where = value;
			}
		}
		public string OrderBy
		{
			get
			{
				return this._OrderBy;
			}
			set
			{
				this._OrderBy = value;
			}
		}
		public string GroupBy
		{
			get
			{
				return this._GroupBy;
			}
			set
			{
				this._GroupBy = value;
			}
		}
		public string Having
		{
			get
			{
				return this._Having;
			}
			set
			{
				this._Having = value;
			}
		}
		public int Top
		{
			get
			{
				return this._Top;
			}
			set
			{
				this._Top = value;
			}
		}
		public int Offset
		{
			get
			{
				return this._Offset;
			}
			set
			{
				this._Offset = value;
			}
		}
		public string SqlSelect
		{
			get
			{
				return this.ToString();
			}
		}
		static LibSqlSelect()
		{
			LibSqlSelect._ProviderName = "";
			LibSqlSelect._ProviderName = ConfigurationManager.ConnectionStrings[0].ProviderName;
		}
		public LibSqlSelect()
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
		}
		public LibSqlSelect(string field)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
		}
		public LibSqlSelect(string field, string table)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
		}
		public LibSqlSelect(string field, string table, int top)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Top = top;
		}
		public LibSqlSelect(string field, string table, int top, int offset)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Top = top;
			this._Offset = offset;
		}
		public LibSqlSelect(string field, string table, string where)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
		}
		public LibSqlSelect(string field, string table, string where, int top)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._Top = top;
		}
		public LibSqlSelect(string field, string table, string where, int top, int offset)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._Top = top;
			this._Offset = offset;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, int top)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._Top = top;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, int top, int offset)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._Top = top;
			this._Offset = offset;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy, int top)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
			this._Top = top;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy, int top, int offset)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
			this._Top = top;
			this._Offset = offset;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy, string having)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
			this._Having = having;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy, string having, int top)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
			this._Having = having;
			this._Top = top;
		}
		public LibSqlSelect(string field, string table, string where, string orderBy, string groupBy, string having, int top, int offset)
		{
			this._Field = "";
			this._Table = "";
			this._Where = "";
			this._OrderBy = "";
			this._GroupBy = "";
			this._Having = "";
			this._Top = 0;
			this._Offset = 0;
			this._Field = field;
			this._Table = table;
			this._Where = where;
			this._OrderBy = orderBy;
			this._GroupBy = groupBy;
			this._Having = having;
			this._Top = top;
			this._Offset = offset;
		}
		public override string ToString()
		{
			bool flag = Operators.CompareString(this.ProviderName, "", false) == 0;
			if (flag)
			{
				this.ProviderName = "System.Data.SqlClient";
			}
			return this.ToString(this.ProviderName);
		}
		public string ToString(string ProviderName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string left = ProviderName.ToUpper();
			bool flag = Operators.CompareString(left, "SYSTEM.DATA.SQLCLIENT", false) == 0;
			if (flag)
			{
				bool flag2 = this._Field.Trim().Length > 0;
				if (flag2)
				{
					stringBuilder.Append("SELECT");
					flag2 = (this._Top > 0);
					if (flag2)
					{
						stringBuilder.Append(" TOP ");
						stringBuilder.Append(this._Top);
					}
					stringBuilder.Append(" " + this._Field);
					flag2 = (this._Table.Trim().Length > 0);
					if (flag2)
					{
						stringBuilder.Append(" FROM ");
						stringBuilder.Append(this._Table);
						flag2 = (this._Where.Trim().Length > 0);
						if (flag2)
						{
							stringBuilder.Append(" WHERE ");
							stringBuilder.Append(this._Where);
						}
						flag2 = (this._GroupBy.Trim().Length > 0);
						if (flag2)
						{
							stringBuilder.Append(" GROUP BY ");
							stringBuilder.Append(this._GroupBy);
							flag2 = (this._Having.Trim().Length > 0);
							if (flag2)
							{
								stringBuilder.Append(" HAVING ");
								stringBuilder.Append(this._Having);
							}
						}
						flag2 = (this._OrderBy.Trim().Length > 0);
						if (flag2)
						{
							stringBuilder.Append(" ORDER BY ");
							stringBuilder.Append(this._OrderBy);
						}
					}
				}
			}
			else
			{
				bool flag2 = Operators.CompareString(left, "SYSTEM.DATA.ODBC", false) == 0;
				if (flag2)
				{
					flag = (this._Field.Trim().Length > 0);
					if (flag)
					{
						stringBuilder.Append("SELECT");
						flag2 = (this._Top > 0);
						if (flag2)
						{
							stringBuilder.Append(" TOP ");
							stringBuilder.Append(this._Top);
						}
						stringBuilder.Append(" " + this._Field);
						flag2 = (this._Table.Trim().Length > 0);
						if (flag2)
						{
							stringBuilder.Append(" FROM ");
							stringBuilder.Append(this._Table);
							flag2 = (this._Where.Trim().Length > 0);
							if (flag2)
							{
								stringBuilder.Append(" WHERE ");
								stringBuilder.Append(this._Where);
							}
							flag2 = (this._GroupBy.Trim().Length > 0);
							if (flag2)
							{
								stringBuilder.Append(" GROUP BY ");
								stringBuilder.Append(this._GroupBy);
								flag2 = (this._Having.Trim().Length > 0);
								if (flag2)
								{
									stringBuilder.Append(" HAVING ");
									stringBuilder.Append(this._Having);
								}
							}
							flag2 = (this._OrderBy.Trim().Length > 0);
							if (flag2)
							{
								stringBuilder.Append(" ORDER BY ");
								stringBuilder.Append(this._OrderBy);
							}
						}
					}
				}
				else
				{
					flag2 = (Operators.CompareString(left, "MYSQL.DATA.MYSQLCLIENT", false) == 0);
					if (flag2)
					{
						flag = (this._Field.Trim().Length > 0);
						if (flag)
						{
							stringBuilder.Append("SELECT ");
							stringBuilder.Append(this._Field);
							flag2 = (this._Table.Trim().Length > 0);
							if (flag2)
							{
								stringBuilder.Append(" FROM ");
								stringBuilder.Append(this._Table);
								flag2 = (this._Where.Trim().Length > 0);
								if (flag2)
								{
									stringBuilder.Append(" WHERE ");
									stringBuilder.Append(this._Where);
								}
								flag2 = (this._GroupBy.Trim().Length > 0);
								if (flag2)
								{
									stringBuilder.Append(" GROUP BY ");
									stringBuilder.Append(this._GroupBy);
									flag2 = (this._Having.Trim().Length > 0);
									if (flag2)
									{
										stringBuilder.Append(" HAVING ");
										stringBuilder.Append(this._Having);
									}
								}
								flag2 = (this._OrderBy.Trim().Length > 0);
								if (flag2)
								{
									stringBuilder.Append(" ORDER BY ");
									stringBuilder.Append(this._OrderBy);
								}
							}
							flag2 = (this._Top > 0);
							if (flag2)
							{
								stringBuilder.Append(" LIMIT ");
								stringBuilder.Append(this._Top);
							}
							flag2 = (this._Offset > 0);
							if (flag2)
							{
								stringBuilder.Append(" OFFSET ");
								stringBuilder.Append(this._Offset);
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
