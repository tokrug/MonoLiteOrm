using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Data table. Holds primitive data types. No objects allowed.
	/// </summary>
	public class DataTable
	{
		
		private List<DataColumn> columns = new List<DataColumn>();
		private List<DataRow> rows = new List<DataRow>();
		
		private List<IConstraint> constraints = new List<IConstraint>();
		
		public virtual string Name {get;set;}
		public virtual List<DataColumn> Columns {get{return this.columns;}}
		public virtual List<DataRow> Rows {get{return this.rows;}}
		public virtual int RowCount {get{return rows.Count;}}
		
		public DataTable ()
		{
		}
		
		// if any of the constraints is violated then this is no op
		public virtual bool AddRow(DataRow row) {
			if (checkConstraints(row)) {
				updateConstraints(row);
				this.rows.Add (row);
				return true;
			}
			return false;
		}
		
		public virtual DataRow CreateRow() {
			DataRow row = new DataRow() {Table = this};
			return row;
		}
		
		public virtual void AddColumn (DataColumn column) {
			this.Columns.Add (column);
			column.Table = this;
		}
		
		public void AddConstraint(IConstraint constraint) {
			this.constraints.Add (constraint);
		}
		
		private bool checkConstraints(DataRow row) {
			return this.constraints.TrueForAll((x)=>x.validate(row));
		}
		
		private void updateConstraints(DataRow row) {
			this.constraints.ForEach((x)=>x.addRow(row));
		}
	}
}

