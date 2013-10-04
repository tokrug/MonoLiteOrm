using System;
using System.Data;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Data set filler.
	/// Gets a query and fills a data set based on it.
	/// </summary>
	public class DataSetFiller
	{
		
		private DataSet currentSet;
		
		// to speed up lookup of tables, cant do that with columns (at least not that easily)
		private Dictionary<LogicalQueryTable, DataTable> tablesShortcut;
		
		public DataSetFiller ()
		{
			this.currentSet = new DataSet();
			this.tablesShortcut = new Dictionary<LogicalQueryTable, DataTable>();
		}
		
		public virtual DataSetFiller AddData(IDataReader reader, IEnumerable<LogicalQueryTable> logicalTables) {
			foreach (LogicalQueryTable queryTable in logicalTables) {
				AddTableToSet (queryTable);
			}
			while (reader.Read ()) {
				foreach (LogicalQueryTable queryTable in logicalTables) {
					DataTable dataTable = GetDataTable(queryTable);
					DataRow row = dataTable.CreateRow();
					foreach (TableColumn col in queryTable.Table.GetColumns()) {
						DataColumn dataSetColumn = GetDataColumn(dataTable, col);
						// TODO this sucks, I should not have to duplicate column name creation logic
						row[dataSetColumn] = reader[queryTable.Alias + "." + col.Name];
					}
					if (!row.IsEmpty())
						dataTable.AddRow(row);
				}
			}
			return this;
		}
		
		public DataSet GetDataSet() {
			return this.currentSet;	
		}
		
		private DataTable GetDataTable(LogicalQueryTable queryTable) {
			return this.tablesShortcut[queryTable];
		}
		
		private DataColumn GetDataColumn(DataTable dataTable, TableColumn col) {
			return dataTable.Columns.Find ((x) => x.Name.Equals (col.Name));
		}
		
		// add a new data table to the set only if the set does not contain apropriate table yet
		private void AddTableToSet(LogicalQueryTable queryTable) {
			if (this.currentSet.Tables.Find((x) => x.Name.Equals(queryTable.Table.Name)) == null) {
				DataTable dataTable = ToDataTable (queryTable.Table);
				this.currentSet.Tables.Add (dataTable);
				// add table shortcut
				this.tablesShortcut.Add (queryTable, dataTable);
			}
		}
		
		private DataTable ToDataTable(LogicalTable logicalTable) {
			DataTable dataTable = new DataTable();
			dataTable.Name = logicalTable.Name;
			foreach (TableColumn col in logicalTable.GetColumns()) {
				dataTable.AddColumn(new DataColumn() {Name = col.Name});
			}
			// TODO: cannot assume that for every table there should be unique row constraint
			dataTable.AddConstraint(new UniqueRowConstraint());
			return dataTable;
		}
	}
}

