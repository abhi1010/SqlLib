using System;
namespace DigestLib.Data
{
	public struct LibDataStatus
	{
		private int _InsertedID;
		private string _whereClause;
		private int _rowsUpdated;
		private DataStatus _DatatStatus;
		public int InsertedID
		{
			get
			{
				return this._InsertedID;
			}
			set
			{
				this._InsertedID = value;
			}
		}
		public string WhereClause
		{
			get
			{
				return this._whereClause;
			}
			set
			{
				this._whereClause = value;
			}
		}
		public int RowsUpdated
		{
			get
			{
				return this._rowsUpdated;
			}
			set
			{
				this._rowsUpdated = value;
			}
		}
		public DataStatus DatatStatus
		{
			get
			{
				return this._DatatStatus;
			}
			set
			{
				this._DatatStatus = value;
			}
		}
		public override string ToString()
		{
			return string.Format("DatatStatus={0}; RowsUpdated={1}; InsertedID={2}; Whereclause={3}", new object[]
			{
				this.DatatStatus,
				this.RowsUpdated,
				this.InsertedID,
				this.WhereClause
			});
		}
	}
}
