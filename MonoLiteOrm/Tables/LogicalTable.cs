using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mono.Mlo
{
	/// <summary>
	/// Logical table. Abstraction over using join tables or leaving foreign keys in main (entity) tables. Each logical table models either an entity or a relation.
	/// All of the physical columns included in it must belong to only one physical table.
	/// </summary>
	public class LogicalTable
	{
		
		private List<TableColumn> columns;
		
		public virtual List<TableColumn> Columns {get{return this.columns;}}
		// unique name across persistence unit
		public virtual string Name {get;set;}
		public virtual TableDefinition PartOfTable {get;set;}
		
		public LogicalTable ()
		{
			this.columns = new List<TableColumn>();
		}
		
		public virtual void AddColumn(TableColumn column) {
			this.columns.Add (column);
		}
		
		public virtual ReadOnlyCollection<TableColumn> GetColumns() {
			return new ReadOnlyCollection<TableColumn>(this.columns);
		}
		
		public bool IsPartOfTheSameTable(LogicalTable table) {
			return this.PartOfTable.Equals (table.PartOfTable);
		}
		
	}
}

