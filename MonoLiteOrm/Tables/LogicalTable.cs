using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mono.Mlo
{
	/// <summary>
	/// Logical table. Abstraction over using join tables or leaving foreign keys in main (entity) tables. Each logical table models either an entity or a relation.
	/// 
	/// Constraints:
	/// All of the physical columns included in it must belong to only one physical table.
	/// To join two logical tables there has to be exactly one link from ForeignKey column of one table to PrimaryKey column of another.
	/// If two FK point at one PK then there is no way to tell which relation should be used.
	/// Each logical table can have at most one primary key column.
	/// </summary>
	public class LogicalTable
	{
		
		// all of the columns included
		private List<TableColumn> columns;
		
		// list available to the outside world
		private ReadOnlyCollection<TableColumn> readColumns;
		
		// primary key if any
		private TableColumn primaryKey;
		
		// all foreign keys included in this logical table
		private List<TableColumn> foreignKeys;
		
		private ReadOnlyCollection<TableColumn> readForeignKeys;
		
		// unique name across persistence unit
		// it will be used as an identifier in dataset
		public virtual string Name {get;set;}
		
		public virtual LogicalTableType TableType {get;set;}
		
		// all columns must come from the same physical table
		public virtual TableDefinition PartOfTable {get;set;}
		
		public LogicalTable ()
		{
			this.columns = new List<TableColumn>();
			this.readColumns = new ReadOnlyCollection<TableColumn>(columns);
			this.foreignKeys = new List<TableColumn>();
			this.readForeignKeys = new ReadOnlyCollection<TableColumn>(foreignKeys);
		}
		
		// can throw an exception if constraints are not respected
		public virtual void AddColumn(TableColumn column) {
			// check if added column is really a part of the same physical table as other columns
			if (canColumnBeAdded(column)) {
				if (column.IsPrimaryKey) {
					addPrimaryKey (column);
				} else if (column.IsForeignKey) {
					addForeignKey (column);
				}
				this.columns.Add (column);
			} else {
				throw new ArgumentException("Logical table cannnot contain columns from more than one physical table.");
			}
		}
		
		public bool IsPartOfTheSameTable(LogicalTable table) {
			return this.PartOfTable.Equals (table.PartOfTable);
		}
		
		/// <summary>
		/// Gets the primary key. The returned value can be null (in case of separate join table or embedded collection).
		/// </summary>
		/// <returns>
		/// The primary key.
		/// </returns>
		public TableColumn GetPrimaryKey() {
			return this.primaryKey;	
		}
		
		public ReadOnlyCollection<TableColumn> GetColumns() {
			return this.readColumns;	
		}
		
		public ReadOnlyCollection<TableColumn> GetForeignKeys() {
			return this.readForeignKeys;	
		}
		
		public TableColumn GetForeignKey(TableColumn referencedPrimaryKey) {
			return this.foreignKeys.Find ((x) => x.ReferencedColumn.Equals(referencedPrimaryKey));	
		}
		
		private bool canColumnBeAdded(TableColumn column) {
			return column.Table.Equals (this.PartOfTable);
		}
		
		private void addPrimaryKey(TableColumn column) {
			// check if primary key is already assigned
			if (this.primaryKey == null) {
				this.primaryKey = column;
			} else {
				throw new ArgumentException("Logical table cannot contain more than one primary key column.");	
			}
		}
		
		private void addForeignKey(TableColumn column) {
			// check if it references the same primary key as any other foreign key already included
			if (this.foreignKeys.Find ((x) => x.ReferencedColumn.Equals(column.ReferencedColumn)) != null) {
				throw new ArgumentException("Another foreign key in this table references the same primary key.");
			} else {
				this.foreignKeys.Add (column);	
			}
		}
		
	}
}

