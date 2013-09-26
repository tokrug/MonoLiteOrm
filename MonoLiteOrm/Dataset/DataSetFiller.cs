using System;
using System.Data;

namespace Mono.Mlo
{
	/// <summary>
	/// Data set filler.
	/// Gets a query and fills a data set based on it.
	/// </summary>
	public class DataSetFiller
	{
		public DataSetFiller ()
		{
		}
		
		public DataSet queryResultToDataSet(IDataReader reader, TableDefinition table) {
			DataSet dataSet = new DataSet();
			DataTable mainTable = definitionToDataTable(table);
			dataSet.Tables.Add (mainTable);
			while (reader.Read ()) {
				DataRow row = mainTable.addRow();
				foreach (DataColumn col in mainTable.Columns) {
					row[col] = reader[col.Name];
				}
			}
			return dataSet;
		}
		
		private DataTable definitionToDataTable(TableDefinition table) {
			DataTable dataTable = new DataTable();
			dataTable.Name = table.Name;
			foreach (TableColumn col in table.getColumns()) {
				dataTable.Columns.Add (new DataColumn() {Name = col.Name, Table = dataTable});
			}
			return dataTable;
		}
	}
}

