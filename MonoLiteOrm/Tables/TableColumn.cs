using System;

namespace MonoLiteOrm
{
	/// <summary>
	/// Table column.
	/// Tied to a specific table. Cannot be moved to another one.
	/// </summary>
	public class TableColumn
	{
		
		private List<TableColumn> referencedColumns;
		
		public TableDefinition Table {get;set;}
		public string Name {get;set;}
		//public string Type {get;set;}
		public List<TableColumn> ReferencedColumns {get{return this.referencedColumns;}}
		public bool IsPrimaryKey {get;set;}
		public bool IsForeignKey {get;set;}
		
		public TableColumn ()
		{
			this.referencedColumns = new List<TableColumn>();
		}
		
		
	}
}

