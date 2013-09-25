using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Table column.
	/// Tied to a specific table. Cannot be moved to another one.
	/// </summary>
	public class TableColumn
	{
		
		private TableColumn referencedColumn;
		
		public TableDefinition Table {get;set;}
		public string Name {get;set;}
		public string Type {get;set;}
		// if foreign key
		public TableColumn ReferencedColumn {get{return this.referencedColumn;}}
		public bool IsPrimaryKey {get;set;}
		public bool IsForeignKey {get;set;}
		
		public TableColumn ()
		{
			
		}
		
		public string getColumnDefinition() {
			if (IsPrimaryKey) {
				return Name + " " + Type + " PRIMARY KEY ASC";	
			} else {
				return Name + " " + Type;	
			}
		}
		
	}
}

