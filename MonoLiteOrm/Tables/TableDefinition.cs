using System;
using System.Collections.Generic;

namespace MonoLiteOrm
{
	/// <summary>
	/// Table definition.
	/// 
	/// Contains all information about tables in the database based on attributes present in class definition.
	/// </summary>
	public class TableDefinition
	{
		
		private List<TableColumn> columns;
		
		public string Name {get;set;}
		public List<TableColumn> Columns {get{return this.columns;}}
		
		public TableDefinition ()
		{
			this.columns = new List<TableColumn>();
		}
		
		
		
	}
}

