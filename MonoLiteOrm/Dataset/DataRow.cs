using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class DataRow
	{
		
		private Dictionary<DataColumn, object> values = new Dictionary<DataColumn, object>();
		
		public DataTable Table {get;set;}
		
		public DataRow ()
		{
		}
		
		/// <summary>
		/// Gets or sets the value of the specified column in this data row.
		/// </summary>
		/// <param name='column'>
		/// Column.
		/// </param>
		public object this[DataColumn column] {
			get {
				return this.values[column];	
			}
			set {
				values[column] = value;	
			}
		}
		
		public object this[string columnName] {
			get {
				DataColumn col = Table.Columns.Find ((x) => x.Name == columnName);
				return this.values[col];
			}
			set {
				DataColumn col = Table.Columns.Find ((x) => x.Name == columnName);
				this.values[col] = value;
			}
		}
		
	}
}

