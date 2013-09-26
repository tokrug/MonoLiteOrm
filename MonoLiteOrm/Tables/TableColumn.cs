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
		
		public virtual TableDefinition Table {get;set;}
		public virtual string Name {get;set;}
		public virtual string Type {get;set;}
		// if foreign key
		public virtual TableColumn ReferencedColumn {get{return this.referencedColumn;}}
		public virtual bool IsPrimaryKey {get;set;}
		public virtual bool IsForeignKey {get;set;}
		
		public TableColumn ()
		{
			
		}
		
		public virtual string getColumnDefinition() {
			if (IsPrimaryKey) {
				return Name + " " + Type + " PRIMARY KEY ASC";	
			} else {
				return Name + " " + Type;	
			}
		}
		
	}
}

