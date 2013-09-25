using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class InsertStatementBuilder
	{
		
		private List<string> columns = new List<string>();
		
		public List<string> Columns {get{return this.columns;}}
		public string TableName {get;set;}
		
		public InsertStatementBuilder ()
		{
		}
		
		public override string ToString ()
		{
			List<string> columnNames = new List<string>();
			List<string> parameters = new List<string>();
			foreach (string columnName in this.columns) {
				columnNames.Add (columnName);
				parameters.Add ("@" + columnName);
			}
			return "INSERT INTO " + TableName + "(" + String.Join (", ", columnNames.ToArray()) + ") VALUES (" + String.Join (", ", parameters.ToArray()) + ");";
		}
		
	}
}

