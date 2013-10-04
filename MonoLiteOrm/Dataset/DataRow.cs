using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class DataRow
	{
		
		private Dictionary<DataColumn, object> values = new Dictionary<DataColumn, object>();
		
		public virtual DataTable Table {get;set;}
		
		public DataRow ()
		{
		}
		
		/// <summary>
		/// Gets or sets the value of the specified column in this data row.
		/// </summary>
		/// <param name='column'>
		/// Column.
		/// </param>
		public virtual object this[DataColumn column] {
			get {
				return this.values[column];	
			}
			set {
				values[column] = value;	
			}
		}
		
		public virtual object this[string columnName] {
			get {
				DataColumn col = Table.Columns.Find ((x) => x.Name == columnName);
				return this.values[col];
			}
			set {
				DataColumn col = Table.Columns.Find ((x) => x.Name == columnName);
				this.values[col] = value;
			}
		}
		
		public virtual bool IsEmpty() {
			foreach (DataColumn col in Table.Columns) {
				if (values[col] != null)
					return false;
			}
			return true;
		}
		
		public override int GetHashCode ()
		{
			unchecked // Overflow is fine, just wrap
		    {
		        int hash = 17;
				foreach (DataColumn col in Table.Columns) {
					hash = hash * 23 + values[col].GetHashCode();	
				}
		        return hash;
		    }
		}
		
		public override bool Equals (object obj)
		{
			
			var item = obj as DataRow;
			
			if (item == null)
				return false;
			
			foreach (DataColumn col in Table.Columns) {
				if (!item.values[col].Equals (values[col]))
					return false;
			}
			
			return true;
			
		}
		
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append ("DataRow: ");
			foreach (DataColumn col in Table.Columns) {
				builder.Append (col.Name + " = " + values[col].ToString () + ", ");
			}
			builder.Remove(builder.Length-2,2);
			return builder.ToString ();
		}
		
	}
}

