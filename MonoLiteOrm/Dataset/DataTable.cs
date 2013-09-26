using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Data table.
	/// </summary>
	public class DataTable
	{
		
		private List<DataColumn> columns = new List<DataColumn>();
		private List<DataRow> rows = new List<DataRow>();
		
		public string Name {get;set;}
		public List<DataColumn> Columns {get{return this.columns;}}
		public List<DataRow> Rows {get{return this.rows;}}
		public int RowCount {get{return rows.Count;}}
		
		public DataTable ()
		{
		}
		
		public DataRow addRow() {
			DataRow row = new DataRow() {Table = this};
			this.rows.Add (row);
			return row;
		}
	}
}

