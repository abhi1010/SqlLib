using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
namespace DigestLib.Data.Sql
{
	public sealed class LibSql
	{
		private static string _ProviderName;
		public static int GetDefaultConnectionStringID
		{
			get
			{
				return 0;
			}
		}
		public static string ProviderName
		{
			get
			{
				bool flag = Operators.CompareString(LibSql._ProviderName, "", false) == 0;
				if (flag)
				{
					LibSql._ProviderName = ConfigurationManager.ConnectionStrings[0].ProviderName;
				}
				return LibSql._ProviderName;
			}
			set
			{
				LibSql._ProviderName = value;
			}
		}
		public static DbProviderFactory GetFactory
		{
			get
			{
                return DbProviderFactories.GetFactory(ProviderName);
			}
		}
		public static DbConnection GetConnection
		{
			get
			{
				return LibSql.GetFactory.CreateConnection();
			}
		}
		public static string CurrentFactory
		{
			get
			{
				return LibSql.ProviderName;
			}
		}
		public static DbProviderFactory Factory
		{
			get
			{
				return DbProviderFactories.GetFactory(LibSql.CurrentFactory);
			}
		}
		public static DbConnection Connection
		{
			get
			{
				return LibSql.Factory.CreateConnection();
			}
		}
		public static DbDataAdapter DataAdapter
		{
			get
			{
				return LibSql.Factory.CreateDataAdapter();
			}
		}
		static LibSql()
		{
			LibSql._ProviderName = "";
			LibSql._ProviderName = ConfigurationManager.ConnectionStrings[0].ProviderName;
		}
		private LibSql()
		{
		}
		public static string GetProviderName()
		{
			return LibSql.GetProviderName(LibSql.GetDefaultConnectionStringID);
		}
		public static string GetProviderName(string connectionStringName)
		{
			return ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
		}
		public static string GetProviderName(int connectionStringID)
		{
			return ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName;
		}
		public static int GetConnectionStringCount()
		{
			return ConfigurationManager.ConnectionStrings.Count;
		}
		public static string GetConnectionString()
		{
			return LibSql.GetConnectionString(0);
		}
		public static string GetConnectionString(string stringName)
		{
			bool flag = stringName.Contains(";");
			string result;
			if (flag)
			{
				result = stringName;
			}
			else
			{
				result = ConfigurationManager.ConnectionStrings[stringName].ConnectionString.ToString();
			}
			return result;
		}
		public static string GetConnectionString(int connectionStringID)
		{
			return ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
		}
		public static DbConnection GetDbConnection()
		{
			return LibSql.GetDbConnection(LibSql.GetDefaultConnectionStringID);
		}
		public static DbConnection GetDbConnection(string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetDbConnection(connectionString, providerName);
		}
		public static DbConnection GetDbConnection(int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetDbConnection(connectionString, providerName);
		}
		public static DbConnection GetDbConnection(string connectionString, string providerName)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = connectionString;
			return dbConnection;
		}
		public static DataSet GetDataSet(LibSqlSelect SqlSelect)
		{
			return LibSql.GetDataSet(SqlSelect.ToString());
		}
		public static DataSet GetDataSet(string commandText)
		{
			return LibSql.GetDataSet(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static DataSet GetDataSet(LibSqlSelect SqlSelect, int connectionStringID)
		{
			return LibSql.GetDataSet(SqlSelect.ToString(), connectionStringID);
		}
		public static DataSet GetDataSet(LibSqlSelect SqlSelect, string connectionStringName)
		{
			return LibSql.GetDataSet(SqlSelect.ToString(), connectionStringName);
		}
		public static DataSet GetDataSet(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetDataSet(commandText, connectionString, providerName);
		}
		public static DataSet GetDataSet(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetDataSet(commandText, connectionString, providerName);
		}
		public static DataSet GetDataSet(string commandText, string connectionString, string providerName)
		{
			DataSet dataSet = new DataSet();
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				DbConnection dbConnection = factory.CreateConnection();
				dbConnection.ConnectionString = connectionString;
				DbConnection dbConnection2 = dbConnection;
				try
				{
					DbCommand dbCommand = factory.CreateCommand();
					dbCommand.CommandText = commandText;
					dbCommand.CommandType = CommandType.Text;
					dbCommand.Connection = dbConnection;
					DbDataAdapter dbDataAdapter = factory.CreateDataAdapter();
					dbDataAdapter.SelectCommand = dbCommand;
					dbDataAdapter.Fill(dataSet);
				}
				finally
				{
					bool flag = dbConnection2 != null;
					if (flag)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			catch (DbException expr_7E)
			{
				ProjectData.SetProjectError(expr_7E);
				DbException ex = expr_7E;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL WAS " + commandText);
			}
			catch (Exception expr_AB)
			{
				ProjectData.SetProjectError(expr_AB);
				Exception ex2 = expr_AB;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL WAS " + commandText);
			}
			return dataSet;
		}
		public static DataTable GetDataTable(LibSqlSelect SqlSelect)
		{
			return LibSql.GetDataTable(SqlSelect.ToString());
		}
		public static DataTable GetDataTable(string commandText)
		{
			return LibSql.GetDataTable(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static DataTable GetDataTable(LibSqlSelect SqlSelect, int connectionStringID)
		{
			return LibSql.GetDataTable(SqlSelect.ToString(), connectionStringID);
		}
		public static DataTable GetDataTable(LibSqlSelect SqlSelect, string connectionStringName)
		{
			return LibSql.GetDataTable(SqlSelect.ToString(), connectionStringName);
		}
		public static DataTable GetDataTable(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetDataTable(commandText, connectionString, providerName);
		}
		public static DataTable GetDataTable(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetDataTable(commandText, connectionString, providerName);
		}
		public static DataTable GetDataTable(string commandText, string connectionString, string providerName)
		{
			DataSet dataSet = LibSql.GetDataSet(commandText, connectionString, providerName);
			return dataSet.Tables[0];
		}
		public static DataTable GetSchemaTable(string table)
		{
			return LibSql.GetSchemaTable(table, LibSql.GetConnectionString());
		}
		public static DataTable GetSchemaTable(string table, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetSchemaTable(table, connectionString, providerName);
		}
		public static DataTable GetSchemaTable(string table, string connectionString)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(LibSql.GetProviderName());
			DbConnection dbConnection = factory.CreateConnection();
			DataTable schemaTable;
			try
			{
				dbConnection.ConnectionString = LibSql.GetConnectionString();
				schemaTable = LibSql.GetSchemaTable(table, dbConnection);
			}
			finally
			{
				bool flag = dbConnection != null;
				if (flag)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
			return schemaTable;
		}
		public static DataTable GetSchemaTable(string table, string connectionString, string providerName)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
			DbConnection dbConnection = factory.CreateConnection();
			DataTable schemaTable;
			try
			{
				dbConnection.ConnectionString = connectionString;
				schemaTable = LibSql.GetSchemaTable(table, dbConnection, providerName);
			}
			finally
			{
				bool flag = dbConnection != null;
				if (flag)
				{
					((IDisposable)dbConnection).Dispose();
				}
			}
			return schemaTable;
		}
		public static DataTable GetSchemaTable(string table, DbConnection connection, string providerName)
		{
			DataTable schemaTable;
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				bool flag = connection.State != ConnectionState.Open;
				if (flag)
				{
					connection.Open();
				}
				try
				{
					DbCommand dbCommand = factory.CreateCommand();
					dbCommand.CommandText = "SELECT * FROM " + table;
					dbCommand.CommandType = CommandType.Text;
					dbCommand.Connection = connection;
					schemaTable = dbCommand.ExecuteReader(CommandBehavior.SchemaOnly).GetSchemaTable();
				}
				finally
				{
					flag = (connection != null);
					if (flag)
					{
						((IDisposable)connection).Dispose();
					}
				}
			}
			catch (DbException expr_7D)
			{
				ProjectData.SetProjectError(expr_7D);
				DbException ex = expr_7D;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL WAS select * from " + table);
			}
			catch (Exception expr_A8)
			{
				ProjectData.SetProjectError(expr_A8);
				Exception ex2 = expr_A8;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL WAS select * from " + table);
			}
			return schemaTable;
		}
		public static DataTable GetSchemaTable(string table, DbConnection connection)
		{
			string providerName = LibSql.GetProviderName();
			return LibSql.GetSchemaTable(table, connection, providerName);
		}
		public static DataRow GetRow(LibSqlSelect SqlSelect)
		{
			return LibSql.GetRow(SqlSelect.ToString());
		}
		public static DataRow GetRow(string commandText)
		{
			return LibSql.GetRow(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static DataRow GetRow(LibSqlSelect SqlSelect, int connectionStringID)
		{
			return LibSql.GetRow(SqlSelect.ToString(), connectionStringID);
		}
		public static DataRow GetRow(LibSqlSelect SqlSelect, string connectionStringName)
		{
			return LibSql.GetRow(SqlSelect.ToString(), connectionStringName);
		}
		public static DataRow GetRow(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetRow(commandText, connectionString, providerName);
		}
		public static DataRow GetRow(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetRow(commandText, connectionString, providerName);
		}
		public static DataRow GetRow(string commandText, string connectionString, string providerName)
		{
			DataTable dataTable = LibSql.GetDataTable(commandText, connectionString, providerName);
			bool flag = dataTable.Rows.Count > 0;
			DataRow result;
			if (flag)
			{
				result = dataTable.Rows[0];
			}
			else
			{
				result = null;
			}
			return result;
		}
		public static string GetField(LibSqlSelect SqlSelect)
		{
			return LibSql.GetField(SqlSelect.ToString());
		}
		public static string GetField(string commandText)
		{
			return LibSql.GetField(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static string GetField(LibSqlSelect SqlSelect, int connectionStringID)
		{
			return LibSql.GetField(SqlSelect.ToString(), connectionStringID);
		}
		public static string GetField(LibSqlSelect SqlSelect, string connectionStringName)
		{
			return LibSql.GetField(SqlSelect.ToString(), connectionStringName);
		}
		public static string GetField(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetField(commandText, connectionString, providerName);
		}
		public static string GetField(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetField(commandText, connectionString, providerName);
		}
		public static string GetField(string commandText, string connectionString, string providerName)
		{
			DataTable dataTable = LibSql.GetDataTable(commandText, connectionString, providerName);
			bool flag = dataTable.Rows.Count > 0;
			string result;
			if (flag)
			{
				result = dataTable.Rows[0][0].ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}
		public static int ExecuteNonQuery(string commandText)
		{
			return LibSql.ExecuteNonQuery(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static int ExecuteNonQuery(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.ExecuteNonQuery(commandText, connectionString, providerName);
		}
		public static int ExecuteNonQuery(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.ExecuteNonQuery(commandText, connectionString, providerName);
		}
		private static object GetStoredProcedurePrefix(string providerName)
		{
			string text = LibSql.GetProviderName().ToUpper().Trim();
			bool flag = text.ToUpper().Contains("MYSQL");
			object result;
			if (flag)
			{
				result = "?";
			}
			else
			{
				flag = text.Contains("SQLCLIENT");
				if (flag)
				{
					result = "@";
				}
				else
				{
					flag = text.Contains("ODBC");
					if (flag)
					{
						result = "@";
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}
		public static void CallStoredProcedure(string procName, NameValueCollection input)
		{
			checked
			{
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(LibSql.ProviderName);
                    DbConnection dbConnection = factory.CreateConnection();
                    dbConnection.ConnectionString = LibSql.GetConnectionString();
                    DbCommand dbCommand = factory.CreateCommand();
                    dbCommand.CommandText = procName;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        int num = 0;
                        IEnumerator enumerator = input.Keys.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            string text = Conversions.ToString(enumerator.Current);
                            dbCommand.Parameters.Add(input[text]);
                            string parameterName = Conversions.ToString(Operators.AddObject(LibSql.GetStoredProcedurePrefix(LibSql.ProviderName), text));
                            
                            dbCommand.Parameters[num].ParameterName = parameterName;
                            dbCommand.Parameters[num].Direction = ParameterDirection.Input;
                            num++;
                        }

                        dbCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        dbCommand.Dispose();
                    }
                }
                catch (Exception expr_E8)
                {
                    ProjectData.SetProjectError(expr_E8);
                    Exception ex = expr_E8;
                    throw new Exception("A DbExeption Inside Stored Procedure '" + ex.Message + "'. Your SQL was " + procName);
                }
                finally
                {
                }
			}
		}
		public static int ExecuteNonQuery(string commandText, string connectionString, string providerName)
		{
			int result = 0;
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				DbConnection dbConnection = factory.CreateConnection();
				dbConnection.ConnectionString = connectionString;
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.CommandText = commandText;
				dbCommand.CommandType = CommandType.Text;
				dbCommand.Connection = dbConnection;
				DbConnection dbConnection2 = dbConnection;
				try
				{
					dbConnection.Open();
					result = dbCommand.ExecuteNonQuery();
					dbConnection.Close();
				}
				finally
				{
					bool flag = dbConnection2 != null;
					if (flag)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			catch (DbException expr_73)
			{
				ProjectData.SetProjectError(expr_73);
				DbException ex = expr_73;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL was " + commandText);
			}
			catch (Exception expr_A0)
			{
				ProjectData.SetProjectError(expr_A0);
				Exception ex2 = expr_A0;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL was " + commandText);
			}
			return result;
		}
		public static string InsertAndReturnIdentity(string commandText)
		{
			return LibSql.InsertAndReturnIdentity(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static string InsertAndReturnIdentity(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.InsertAndReturnIdentity(commandText, connectionString, providerName);
		}
		public static string InsertAndReturnIdentity(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.InsertAndReturnIdentity(commandText, connectionString, providerName);
		}
		public static string InsertAndReturnIdentity(string commandText, string connectionString, string providerName)
		{
			string result = "";
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				DbConnection dbConnection = factory.CreateConnection();
				dbConnection.ConnectionString = connectionString;
				commandText = Strings.Trim(commandText);
				bool flag = !commandText.EndsWith(";");
				if (flag)
				{
					commandText += ";";
				}
				string left = Strings.UCase(providerName);
				flag = (Operators.CompareString(left, Strings.UCase("System.Data.SqlClient"), false) == 0);
				if (flag)
				{
					commandText += "SELECT @@IDENTITY;";
				}
				else
				{
					flag = (Operators.CompareString(left, Strings.UCase("MySql.Data.MySqlClient"), false) == 0);
					if (flag)
					{
						commandText += "SELECT LAST_INSERT_ID();";
					}
				}
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.CommandText = commandText;
				dbCommand.CommandType = CommandType.Text;
				dbCommand.Connection = dbConnection;
				DbConnection dbConnection2 = dbConnection;
				try
				{
					dbConnection.Open();
					IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
					while (dataReader.Read())
					{
						result = dataReader[0].ToString();
					}
					dataReader.Close();
					dbConnection.Close();
				}
				finally
				{
					flag = (dbConnection2 != null);
					if (flag)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			catch (DbException expr_128)
			{
				ProjectData.SetProjectError(expr_128);
				DbException ex = expr_128;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL was " + commandText);
			}
			catch (Exception expr_155)
			{
				ProjectData.SetProjectError(expr_155);
				Exception ex2 = expr_155;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL was " + commandText);
			}
			return result;
		}
		private static string GetDateCreatedField(DataTable tblSchema)
		{
			DataRow[] array = tblSchema.Select("ColumnName like '%DateCreated%'");
			bool flag = array.Length > 0;
			string result;
			if (flag)
			{
				result = array[0][0].ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}
		private static string GetDateModifiedField(DataTable tblSchema)
		{
			DataRow[] array = tblSchema.Select("ColumnName like '%DateModified%'");
			bool flag = array.Length > 0;
			string result;
			if (flag)
			{
				result = array[0][0].ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}
		public static string GetUpdateSQL(string tableName, NameValueCollection fields, string where, bool magicQuotes)
		{
			return LibSql.GetUpdateSQL(false, tableName, fields, where, magicQuotes);
		}
		public static string GetUpdateSQL(string tableName, NameValueCollection fields, string where)
		{
			return LibSql.GetUpdateSQL(false, tableName, fields, where, false);
		}
		public static string GetUpdateSQL(string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes)
		{
			return LibSql.GetUpdateSQL(false, tableName, LibFieldColl, where, magicQuotes);
		}
		public static string GetUpdateSQL(string tableName, LibFieldCollection LibFieldColl, string where)
		{
			return LibSql.GetUpdateSQL(false, tableName, LibFieldColl, where, false);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, NameValueCollection fields, string where)
		{
			return LibSql.GetUpdateSQL(compareToSchema, tableName, fields, where, false);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes)
		{
			string providerName = LibSql.GetProviderName();
			string connectionString = LibSql.GetConnectionString();
			return LibSql.GetUpdateSQL(compareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString, providerName);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, int connectionStringID)
		{
			string connectionString = LibSql.GetConnectionString(connectionStringID);
			string providerName = LibSql.GetProviderName(connectionStringID);
			return LibSql.GetUpdateSQL(compareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString, providerName);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, string connectionString)
		{
			string providerName = LibSql.GetProviderName();
			return LibSql.GetUpdateSQL(compareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString, providerName);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, string connectionString, string providerName)
		{
			StringBuilder stringBuilder = new StringBuilder("UPDATE ");
			DataTable dataTable = null;
			if (compareToSchema)
			{
				dataTable = LibSql.GetSchemaTable(tableName, connectionString, providerName);
			}
			bool flag = dataTable == null & compareToSchema;
			if (flag)
			{
				throw new Exception("Schema of the table could not be found out. Please check the name of the table or perhaps disable validation against schema table.");
			}
			if (compareToSchema)
			{
				bool flag2 = false;
				try
				{
					IEnumerator enumerator = LibFieldColl.Keys.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string text = Conversions.ToString(enumerator.Current);
						flag = text.ToUpper().Contains("DATEMODIFIED");
						if (flag)
						{
							flag2 = true;
						}
					}
				}
				finally
				{
				}
				flag = !flag2;
				if (flag)
				{
					string dateModifiedField = LibSql.GetDateModifiedField(dataTable);
					flag = !(dateModifiedField == null & Operators.CompareString(dateModifiedField, "", false) == 0);
					if (flag)
					{
						LibFieldColl.Add(new LibField(dateModifiedField, LibSql.GetProviderDate()));
					}
				}
			}
			stringBuilder.Append(tableName);
			stringBuilder.Append(" SET ");
			string text2 = "";
			bool flag3;
			try
			{
				IEnumerator enumerator2 = LibFieldColl.Keys.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					string text3 = Conversions.ToString(enumerator2.Current);
					if (compareToSchema)
					{
						DataRow[] array = dataTable.Select("IsAutoIncrement=false AND ColumnName=" + LibSql.SqlText(text3));
						flag = (array.Length > 0);
						if (flag)
						{
							flag3 = (LibFieldColl[text3].Type == null);
							if (flag3)
							{
								LibFieldColl[text3].Type = Type.GetType(array[0]["DataType"].ToString());
							}
							flag3 = (LibFieldColl[text3].MaxLength == -1);
							if (flag3)
							{
								LibFieldColl[text3].MaxLength = int.Parse(array[0]["ColumnSize"].ToString());
							}
							flag3 = !LibFieldColl[text3].IsValid;
							if (flag3)
							{
								throw new Exception(string.Concat(new string[]
								{
									"LibField ",
									text3,
									" is not valid. with value: ",
									LibFieldColl[text3].Value,
									" & error = ",
									LibFieldColl[text3].ErrorMsg
								}));
							}
							flag3 = (magicQuotes | (text3.ToUpper().Contains("DATECREATED") | text3.ToUpper().Contains("DATEMODIFIED")));
							if (flag3)
							{
								text2 = string.Concat(new string[]
								{
									text2,
									text3,
									" = ",
									LibFieldColl[text3].Value,
									", "
								});
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									text3,
									" = ",
									LibSql.SqlText(LibFieldColl[text3].Value, bool.Parse(array[0]["AllowDBNull"].ToString())),
									", "
								});
							}
						}
					}
					else
					{
						if (magicQuotes)
						{
							text2 = string.Concat(new string[]
							{
								text2,
								text3,
								" = ",
								LibFieldColl[text3].Value,
								", "
							});
						}
						else
						{
							text2 = string.Concat(new string[]
							{
								text2,
								text3,
								" = ",
								LibSql.SqlText(LibFieldColl[text3].Value),
								", "
							});
						}
					}
				}
			}
			finally
			{
			}
			flag3 = (text2.Length == 0);
			string result;
			if (flag3)
			{
				result = "";
			}
			else
			{
				flag3 = (LibFieldColl.Count > 0);
				if (flag3)
				{
					text2 = text2.Substring(0, checked(text2.Length - 2));
				}
				stringBuilder.Append(text2);
				stringBuilder.Append(" WHERE ");
				stringBuilder.Append(where);
				result = stringBuilder.ToString();
			}
			return result;
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, string where)
		{
			return LibSql.GetUpdateSQL(compareToSchema, tableName, LibFieldColl, where, false);
		}
		public static string GetUpdateSQL(bool compareToSchema, string tableName, NameValueCollection fields, string where, bool magicQuotes)
		{
			LibFieldCollection wizFieldColl = new LibFieldCollection(fields);
			return LibSql.GetUpdateSQL(compareToSchema, tableName, wizFieldColl, where, magicQuotes);
		}
		public static string GetInsertSQL(string tableName, NameValueCollection fields, bool magicQuotes)
		{
			return LibSql.GetInsertSQL(false, tableName, fields, magicQuotes);
		}
		public static string GetInsertSQL(string table, LibFieldCollection LibFieldColl)
		{
			return LibSql.GetInsertSQL(false, table, LibFieldColl, false);
		}
		public static string GetInsertSQL(string table, NameValueCollection NameValueColl)
		{
			return LibSql.GetInsertSQL(false, table, NameValueColl, false);
		}
		public static string GetInsertSQL(string tableName, LibFieldCollection LibFieldColl, bool magicQuotes)
		{
			return LibSql.GetInsertSQL(false, tableName, LibFieldColl, magicQuotes);
		}
		public static string GetInsertSQL(bool compareToSchema, string tableName, NameValueCollection fields, bool magicQuotes)
		{
			LibFieldCollection wizFieldColl = new LibFieldCollection(fields);
			return LibSql.GetInsertSQL(compareToSchema, tableName, wizFieldColl, magicQuotes);
		}
		public static string GetInsertSQL(bool compareToSchema, string table, LibFieldCollection LibFieldColl)
		{
			return LibSql.GetInsertSQL(compareToSchema, table, LibFieldColl, false);
		}
		public static string GetInsertSQL(bool compareToSchema, string table, NameValueCollection NameValueColl)
		{
			return LibSql.GetInsertSQL(compareToSchema, table, NameValueColl, false);
		}
		public static string GetInsertSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, string connectionString)
		{
			string providerName = LibSql.GetProviderName();
			return LibSql.GetInsertSQL(compareToSchema, tableName, LibFieldColl, magicQuotes, connectionString, providerName);
		}
		public static string GetInsertSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, int connectionStringID)
		{
			string providerName = LibSql.GetProviderName(connectionStringID);
			string connectionString = LibSql.GetConnectionString(connectionStringID);
			return LibSql.GetInsertSQL(compareToSchema, tableName, LibFieldColl, magicQuotes, connectionString, providerName);
		}
		public static string GetInsertSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes)
		{
			string providerName = LibSql.GetProviderName();
			string connectionString = LibSql.GetConnectionString();
			return LibSql.GetInsertSQL(compareToSchema, tableName, LibFieldColl, magicQuotes, connectionString, providerName);
		}
		public static string GetInsertSQL(bool compareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, string connectionString, string providerName)
		{
			StringBuilder stringBuilder = new StringBuilder("INSERT INTO " + tableName + " (");
			DataTable dataTable = null;
			if (compareToSchema)
			{
				dataTable = LibSql.GetSchemaTable(tableName, connectionString, providerName);
			}
			bool flag = dataTable == null & compareToSchema;
			if (flag)
			{
				throw new Exception("Schema of the table could not be found out. Please check the name of the table or perhaps disable validation against schema table.");
			}
			if (compareToSchema)
			{
				bool flag2 = false;
				bool flag3 = false;
				try
				{
					IEnumerator enumerator = LibFieldColl.Keys.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string text = Conversions.ToString(enumerator.Current);
						flag = text.ToUpper().Contains("DATECREATED");
						if (flag)
						{
							flag2 = true;
						}
						else
						{
							flag = text.ToUpper().Contains("DATEMODIFIED");
							if (flag)
							{
								flag3 = true;
							}
						}
					}
				}
				finally
				{
				}
				flag = !flag2;
				if (flag)
				{
					string dateCreatedField = LibSql.GetDateCreatedField(dataTable);
					flag = (dateCreatedField != null);
					if (flag)
					{
						LibFieldColl.Add(new LibField(dateCreatedField, LibSql.GetProviderDate()));
					}
				}
				flag = !flag3;
				if (flag)
				{
					string dateModifiedField = LibSql.GetDateModifiedField(dataTable);
					flag = (dateModifiedField != null);
					if (flag)
					{
						LibFieldColl.Add(new LibField(dateModifiedField, LibSql.GetProviderDate()));
					}
				}
			}
			string text2 = "";
			string text3 = "";
			bool flag4;
			try
			{
				IEnumerator enumerator2 = LibFieldColl.Keys.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					string text4 = Conversions.ToString(enumerator2.Current);
					if (compareToSchema)
					{
						DataRow[] array = dataTable.Select("IsAutoIncrement=false AND ColumnName=" + LibSql.SqlText(text4));
						flag = (array.Length > 0);
						if (flag)
						{
							flag4 = (LibFieldColl[text4].Type == null);
							if (flag4)
							{
								LibFieldColl[text4].Type = Type.GetType(array[0]["DataType"].ToString());
							}
							flag4 = (LibFieldColl[text4].MaxLength == -1);
							if (flag4)
							{
								LibFieldColl[text4].MaxLength = int.Parse(array[0]["ColumnSize"].ToString());
							}
							flag4 = !LibFieldColl[text4].IsValid;
							if (flag4)
							{
								throw new Exception(string.Concat(new string[]
								{
									"LibField ",
									text4,
									" is not valid with Value: ",
									LibFieldColl[text4].Value,
									" & error = ",
									LibFieldColl[text4].ErrorMsg
								}));
							}
							text2 = text2 + text4 + ", ";
							flag4 = (magicQuotes | (text4.ToUpper().Contains("DATECREATED") | text4.ToUpper().Contains("DATEMODIFIED")));
							if (flag4)
							{
								text3 = text3 + LibFieldColl[text4].Value + ", ";
							}
							else
							{
								text3 = text3 + LibSql.SqlText(LibFieldColl[text4].Value, bool.Parse(array[0]["AllowDBNull"].ToString())) + ", ";
							}
						}
					}
					else
					{
						text2 = text2 + text4 + ", ";
						if (magicQuotes)
						{
							text3 = text3 + LibFieldColl[text4].Value + ", ";
						}
						else
						{
							text3 = text3 + LibSql.SqlText(LibFieldColl[text4].Value) + ", ";
						}
					}
				}
			}
			finally
			{
			}
			flag4 = (LibFieldColl.Count > 0);
			checked
			{
				if (flag4)
				{
					text2 = text2.Substring(0, text2.Length - 2);
					text3 = text3.Substring(0, text3.Length - 2);
				}
				stringBuilder.Append(text2);
				stringBuilder.Append(")");
				stringBuilder.Append(" VALUES (");
				stringBuilder.Append(text3);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}
		public static int Insert(string InsertQuery)
		{
			return LibSql.ExecuteNonQuery(InsertQuery);
		}
		public static int Insert(bool CompareToSchema, string tableName, LibField LibFieldField)
		{
			return LibSql.Insert(CompareToSchema, tableName, new LibFieldCollection(LibFieldField));
		}
		public static int Insert(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl)
		{
			string insertSQL = LibSql.GetInsertSQL(CompareToSchema, tableName, LibFieldColl, false);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes)
		{
			string insertSQL = LibSql.GetInsertSQL(CompareToSchema, tableName, LibFieldColl, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, int connectionStringID)
		{
			string providerName = LibSql.GetProviderName(connectionStringID);
			string connectionString = LibSql.GetConnectionString(connectionStringID);
			string insertSQL = LibSql.GetInsertSQL(CompareToSchema, tableName, LibFieldColl, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, string connectionString)
		{
			string providerName = LibSql.GetProviderName();
			string insertSQL = LibSql.GetInsertSQL(CompareToSchema, tableName, LibFieldColl, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, bool magicQuotes, string connectionString, string providerName)
		{
			string insertSQL = LibSql.GetInsertSQL(CompareToSchema, tableName, LibFieldColl, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(string tableName, LibField LibFieldField)
		{
			return LibSql.Insert(tableName, new LibFieldCollection(LibFieldField));
		}
		public static int Insert(string tableName, LibFieldCollection LibFieldColl)
		{
			string insertSQL = LibSql.GetInsertSQL(tableName, LibFieldColl, false);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(string tableName, LibFieldCollection LibFieldcoll, bool magicQuotes, string connectionString)
		{
			string insertSQL = LibSql.GetInsertSQL(tableName, LibFieldcoll, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(string tableName, LibFieldCollection LibFieldcoll, bool magicQuotes)
		{
			string insertSQL = LibSql.GetInsertSQL(tableName, LibFieldcoll, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(string tableName, NameValueCollection NameValueColl, bool magicQuotes)
		{
			string insertSQL = LibSql.GetInsertSQL(tableName, NameValueColl, magicQuotes);
			return LibSql.Insert(insertSQL);
		}
		public static int Insert(string tableName, NameValueCollection NameValueColl)
		{
			string insertSQL = LibSql.GetInsertSQL(tableName, NameValueColl);
			return LibSql.Insert(insertSQL);
		}
		public static int Update(string UpdateQuery)
		{
			return LibSql.ExecuteNonQuery(UpdateQuery);
		}
		public static int Update(string tableName, LibField LibFieldField, string where)
		{
			return LibSql.Update(tableName, new LibFieldCollection(LibFieldField), where);
		}
		public static int Update(bool CompareToSchema, string tableName, LibField LibField, string where)
		{
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, new LibFieldCollection(LibField), where);
			return LibSql.Update(updateSQL);
		}
		public static int Update(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, string where)
		{
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, LibFieldColl, where);
			return LibSql.Update(updateSQL);
		}
		public static int Update(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes)
		{
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, LibFieldColl, where, magicQuotes);
			return LibSql.Update(updateSQL);
		}
		public static int Update(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, int connectionStringID)
		{
			string providerName = LibSql.GetProviderName(connectionStringID);
			string connectionString = LibSql.GetConnectionString(connectionStringID);
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString);
			return LibSql.Update(updateSQL);
		}
		public static int Update(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, string connectionString)
		{
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString);
			return LibSql.Update(updateSQL);
		}
		public static int Update(bool CompareToSchema, string tableName, LibFieldCollection LibFieldColl, string where, bool magicQuotes, string connectionString, string providerName)
		{
			string updateSQL = LibSql.GetUpdateSQL(CompareToSchema, tableName, LibFieldColl, where, magicQuotes, connectionString, providerName);
			return LibSql.Update(updateSQL);
		}
		public static int Update(string tableName, LibFieldCollection LibFieldColl, string where)
		{
			string updateSQL = LibSql.GetUpdateSQL(tableName, LibFieldColl, where);
			return LibSql.Update(updateSQL);
		}
		public static int Update(string tableName, LibFieldCollection LibFieldcoll, string where, bool magicQuotes)
		{
			string updateSQL = LibSql.GetUpdateSQL(tableName, LibFieldcoll, where, magicQuotes);
			return LibSql.Update(updateSQL);
		}
		public static int Update(string tableName, NameValueCollection NameValueColl, string where, bool magicQuotes)
		{
			string updateSQL = LibSql.GetUpdateSQL(tableName, NameValueColl, where, magicQuotes);
			return LibSql.Update(updateSQL);
		}
		public static int Update(string tableName, NameValueCollection NameValueColl, string where)
		{
			string updateSQL = LibSql.GetUpdateSQL(tableName, NameValueColl, where);
			return LibSql.Update(updateSQL);
		}
		public static int Delete(string DeleteQuery)
		{
			return LibSql.ExecuteNonQuery(DeleteQuery);
		}
		public static int Delete(string tableName, string where)
		{
			return LibSql.ExecuteNonQuery("DELETE FROM " + tableName + " WHERE " + where);
		}
		public static string SqlText(string inputText)
		{
			return LibSql.SqlText(inputText, "");
		}
		public static string SqlText(string inputText, bool IsNullable)
		{
			string result;
			if (IsNullable)
			{
				result = LibSql.SqlText(inputText, "NULL");
			}
			else
			{
				result = LibSql.SqlText(inputText, "");
			}
			return result;
		}
		public static string SqlText(string inputText, bool IsNullable, int intConnectionStringID)
		{
			string strProviderName = ConfigurationManager.ConnectionStrings[intConnectionStringID].ProviderName.ToString();
			string result;
			if (IsNullable)
			{
				result = LibSql.SqlText(inputText, "NULL", strProviderName);
			}
			else
			{
				result = LibSql.SqlText(inputText, "", strProviderName);
			}
			return result;
		}
		public static string SqlText(string strInputText, int intConnectionStringID)
		{
			string strProviderName = ConfigurationManager.ConnectionStrings[intConnectionStringID].ProviderName.ToString();
			return LibSql.SqlText(strInputText, "", strProviderName);
		}
		public static string SqlText(string strInputText, string defaultReturn, int intConnectionStringID)
		{
			string strProviderName = ConfigurationManager.ConnectionStrings[intConnectionStringID].ProviderName.ToString();
			return LibSql.SqlText(strInputText, defaultReturn, strProviderName);
		}
		public static string SqlText(string inputText, string defaultReturn, string strProviderName)
		{
			bool flag = Conversions.ToBoolean(LibSql.IsKeyword(inputText));
			string result;
			if (flag)
			{
				result = inputText;
			}
			else
			{
				flag = (inputText.Length > 0);
				string text;
				if (flag)
				{
					text = Strings.Trim(inputText);
					strProviderName = strProviderName.ToUpper();
					flag = strProviderName.Contains("MYSQL");
					if (flag)
					{
						text = Strings.Replace(text, "'", "''", 1, -1, CompareMethod.Binary);
						text = Strings.Replace(text, "\\", "\\\\", 1, -1, CompareMethod.Binary);
						text = Strings.Replace(text, "\r\n", "\\r\\n", 1, -1, CompareMethod.Binary);
					}
					else
					{
						text = Strings.Replace(text, "'", "''", 1, -1, CompareMethod.Binary);
						text = Strings.Replace(text, "\r\n", "\\r\\n", 1, -1, CompareMethod.Binary);
					}
					text = "'" + text + "'";
				}
				else
				{
					flag = (defaultReturn.Length > 0);
					if (flag)
					{
						text = defaultReturn;
					}
					else
					{
						text = "''";
					}
				}
				result = text;
			}
			return result;
		}
		public static string SqlText(string inputText, string defaultReturn)
		{
			object obj = LibSql.GetProviderName();
			object arg_50_0 = null;
			Type arg_50_1 = typeof(LibSql);
			string arg_50_2 = "SqlText";
			object[] array = new object[]
			{
				inputText,
				defaultReturn,
				RuntimeHelpers.GetObjectValue(obj)
			};
			object[] arg_50_3 = array;
			string[] arg_50_4 = null;
			Type[] arg_50_5 = null;
			bool[] array2 = new bool[]
			{
				true,
				true,
				true
			};
			object arg_AC_0 = NewLateBinding.LateGet(arg_50_0, arg_50_1, arg_50_2, arg_50_3, arg_50_4, arg_50_5, array2);
			if (array2[0])
			{
				inputText = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
			}
			if (array2[1])
			{
				defaultReturn = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
			}
			if (array2[2])
			{
				obj = RuntimeHelpers.GetObjectValue(array[2]);
			}
			return Conversions.ToString(arg_AC_0);
		}
		private static string IsKeyword(string inputText)
		{
			bool flag = inputText == null;
			string result;
			if (flag)
			{
				result = Conversions.ToString(false);
			}
			else
			{
				inputText = inputText.ToUpper();
				flag = (inputText.Contains("NOW()") | inputText.Contains("DATE()"));
				if (flag)
				{
					result = Conversions.ToString(true);
				}
				else
				{
					result = Conversions.ToString(false);
				}
			}
			return result;
		}
		public static string GetProviderDate()
		{
			string text = LibSql.GetProviderName().ToUpper().Trim();
			bool flag = text.ToUpper().Contains("MYSQL");
			string result;
			if (flag)
			{
				result = "now()";
			}
			else
			{
				flag = text.Contains("SQLCLIENT");
				if (flag)
				{
					result = "GetDate()";
				}
				else
				{
					flag = text.Contains("ODBC");
					if (flag)
					{
						result = "getDate()";
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}
		public static DbDataReader GetDbReader(LibSqlSelect SqlSelect)
		{
			return LibSql.GetDbReader(SqlSelect.ToString());
		}
		public static DbDataReader GetDbReader(string commandText)
		{
			return LibSql.GetDbReader(commandText, LibSql.GetDefaultConnectionStringID);
		}
		public static DbDataReader GetDbReader(LibSqlSelect SqlSelect, int connectionStringID)
		{
			return LibSql.GetDbReader(SqlSelect.ToString(), connectionStringID);
		}
		public static DbDataReader GetDbReader(LibSqlSelect SqlSelect, string connectionStringName)
		{
			return LibSql.GetDbReader(SqlSelect.ToString(), connectionStringName);
		}
		public static DbDataReader GetDbReader(string commandText, int connectionStringID)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringID].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringID].ProviderName.ToString();
			return LibSql.GetDbReader(commandText, connectionString, providerName);
		}
		public static DbDataReader GetDbReader(string commandText, string connectionStringName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.ToString();
			string providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName.ToString();
			return LibSql.GetDbReader(commandText, connectionString, providerName);
		}
		public static DbDataReader GetDbReader(string commandText, string connectionString, string providerName)
		{
			DbDataReader result;
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				DbConnection dbConnection = factory.CreateConnection();
				dbConnection.ConnectionString = connectionString;
				DbConnection dbConnection2 = dbConnection;
				try
				{
					DbCommand dbCommand = factory.CreateCommand();
					dbCommand.CommandText = commandText;
					dbCommand.CommandType = CommandType.Text;
					dbCommand.Connection = dbConnection;
					dbCommand.Connection.Open();
					DbDataReader dbDataReader = dbCommand.ExecuteReader();
					result = dbDataReader;
				}
				finally
				{
					bool flag = dbConnection2 != null;
					if (flag)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			catch (DbException expr_78)
			{
				ProjectData.SetProjectError(expr_78);
				DbException ex = expr_78;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL was " + commandText);
			}
			catch (Exception expr_A5)
			{
				ProjectData.SetProjectError(expr_A5);
				Exception ex2 = expr_A5;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL was " + commandText);
			}
			return result;
		}
		private static int Delet2e(string DeleteQuery)
		{
            return LibSql.ExecuteNonQuery(DeleteQuery);
		}
		public static DataSet GetDataSetFromStoredProc(string storedProcedureName, string connectionString, string providerName)
		{
			DataSet dataSet = new DataSet();
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
				DbConnection dbConnection = factory.CreateConnection();
				dbConnection.ConnectionString = connectionString;
				DbConnection dbConnection2 = dbConnection;
				try
				{
					DbCommand dbCommand = factory.CreateCommand();
					DbParameter dbParameter = dbCommand.CreateParameter();
					dbParameter.ParameterName = "rows";
					dbParameter.Value = 3;
					dbCommand.Parameters.Add(dbParameter);
					dbCommand.CommandText = storedProcedureName;
					dbCommand.CommandType = CommandType.StoredProcedure;
					dbCommand.Connection = dbConnection;
					DbDataAdapter dbDataAdapter = factory.CreateDataAdapter();
					dbDataAdapter.SelectCommand = dbCommand;
					dbDataAdapter.Fill(dataSet);
				}
				finally
				{
					bool flag = dbConnection2 != null;
					if (flag)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			catch (DbException expr_B1)
			{
				ProjectData.SetProjectError(expr_B1);
				DbException ex = expr_B1;
				throw new Exception("A DbExeption was thrown with the message '" + ex.Message + "'. Your SQL WAS " + storedProcedureName);
			}
			catch (Exception expr_DE)
			{
				ProjectData.SetProjectError(expr_DE);
				Exception ex2 = expr_DE;
				throw new Exception("An Exception was thrown with the message '" + ex2.Message + "'. Your SQL WAS " + storedProcedureName);
			}
			return dataSet;
		}
		public static LibDataStatus AutoExecute(string strTableName, LibFieldCollection wizFieldCollection)
		{
			return LibSql.AutoExecute(strTableName, wizFieldCollection, LibSql.GetDefaultConnectionStringID);
		}
		public static LibDataStatus AutoExecute(string strTableName, LibFieldCollection wizFieldCollection, int intConnectionStringID)
		{
			LibDataStatus wizDataStatus = default(LibDataStatus);
			string text = string.Empty;
			wizDataStatus.DatatStatus = DataStatus.NoAction;
			string text2 = LibHelper.ShowValues(wizFieldCollection);
			DataTable schemaTable = LibSql.GetSchemaTable(strTableName, intConnectionStringID);
			DataRow[] array = schemaTable.Select(LibSql.GetPrimaryKeyColumnName(intConnectionStringID) + "=true");
			bool flag = array.Length == 0;
			if (flag)
			{
				array = schemaTable.Select();
			}
			bool flag2 = false;
			DataRow[] array2 = array;
			checked
			{
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow = array2[i];
					flag = Conversions.ToBoolean(NewLateBinding.LateGet(wizFieldCollection, null, "Contains", new object[]
					{
						Operators.AddObject(dataRow["ColumnName"], "")
					}, null, null, null));
					if (!flag)
					{
						throw new Exception(Conversions.ToString(Operators.AddObject("LibFieldCollection does not have a column (Primary Key) with the name ", dataRow["ColumnName"])));
					}
					string text3 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(wizFieldCollection, null, "Item", new object[]
					{
						Operators.AddObject(dataRow["ColumnName"], "")
					}, null, null, null), null, "Value", new object[0], null, null, null));
					flag = (text3.Length == 0);
					if (flag)
					{
						flag2 = true;
						text = string.Empty;
					}
					text = Conversions.ToString(Operators.AddObject(text, Operators.AddObject(Operators.AddObject(Operators.AddObject(dataRow["ColumnName"], "="), LibSql.SqlText(Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(wizFieldCollection, null, "Item", new object[]
					{
						Operators.AddObject(dataRow["ColumnName"], "")
					}, null, null, null), null, "Value", new object[0], null, null, null)), intConnectionStringID)), " AND ")));
				}
				flag = flag2;
				if (flag)
				{
					text = string.Empty;
					try
					{
						IEnumerator<LibField> enumerator = wizFieldCollection.GetEnumerator();
						while (enumerator.MoveNext())
						{
							LibField current = enumerator.Current;
							flag = (schemaTable.Select("ColumnName=" + LibSql.SqlText(current.Name) + "").Length > 0 & !(current.Value.Length == 0 & schemaTable.Select("ColumnName=" + LibSql.SqlText(current.Name) + " AND IsAutoIncrement=true").Length > 0));
							if (flag)
							{
								text = string.Concat(new string[]
								{
									text,
									current.Name,
									"=",
									LibSql.SqlText(current.Value, intConnectionStringID),
									" AND "
								});
							}
						}
					}
					finally
					{
                        IEnumerator<LibField> enumerator = wizFieldCollection.GetEnumerator();
						flag = (enumerator != null);
						if (flag)
						{
							enumerator.Dispose();
						}
					}
				}
				text = text.Substring(0, text.Length - 4);
				wizDataStatus.WhereClause = text;
				string commandText = string.Format("SELECT count(1) from {0} WHERE {1}", strTableName, text);
				int num = int.Parse(LibSql.GetField(commandText, intConnectionStringID));
				flag = (num > 0);
				LibDataStatus result;
				if (flag)
				{
					bool flag3 = flag2;
					if (flag3)
					{
						wizDataStatus.RowsUpdated = -1;
						result = wizDataStatus;
					}
					else
					{
						wizDataStatus.DatatStatus = DataStatus.Updated;
						wizDataStatus.RowsUpdated = LibSql.Update(true, strTableName, wizFieldCollection, text, false, intConnectionStringID);
						result = wizDataStatus;
					}
				}
				else
				{
					wizDataStatus.DatatStatus = DataStatus.Inserted;
					wizDataStatus.RowsUpdated = 1;
					wizDataStatus.InsertedID = Conversions.ToInteger(LibSql.InsertAndReturnIdentity(LibSql.GetInsertSQL(true, strTableName, wizFieldCollection, false, intConnectionStringID)));
					result = wizDataStatus;
				}
				return result;
			}
		}
		public static void Show(string str)
		{
			HttpContext.Current.Response.Write(Environment.NewLine + str);
		}
		public static string GetPrimaryKeyColumnName(int intConnectionStringID)
		{
			object instance = LibSql.GetProviderName(intConnectionStringID).ToUpper().Trim();
			bool flag = Conversions.ToBoolean(NewLateBinding.LateGet(NewLateBinding.LateGet(instance, null, "ToUpper", new object[0], null, null, null), null, "Contains", new object[]
			{
				"MYSQL"
			}, null, null, null));
			string result;
			if (flag)
			{
				result = "IsKey";
			}
			else
			{
				flag = Conversions.ToBoolean(NewLateBinding.LateGet(instance, null, "Contains", new object[]
				{
					"SQLCLIENT"
				}, null, null, null));
				if (flag)
				{
					result = "IsIdentity";
				}
				else
				{
					flag = Conversions.ToBoolean(NewLateBinding.LateGet(instance, null, "Contains", new object[]
					{
						"ODBC"
					}, null, null, null));
					if (flag)
					{
						result = "IsKey";
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}
	}
}
