using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Logical query.
	/// All tables are added to the join tree. If a pair of logical tables is a part of the same physical table then no join is performed.
	/// Also the returned queryTable object will have the same alias for both logical tables.
	/// 
	/// Does not support self join (every employee has a manager). With join table it will work.
	/// </summary>
	public class LogicalQuery
	{
		
		private List<LogicalQueryTable> logicalTables = new List<LogicalQueryTable>();
		private List<PhysicalQueryTable> physicalTables = new List<PhysicalQueryTable>();
		
		private LogicalQueryTable lastTableAdded;
		
		private ITableExpression fromSource;
		
		public LogicalQuery ()
		{
		}
		
		// to be used for consecutive joins without forks
		public virtual LogicalQueryTable JoinTable(LogicalTable table) {
			
			if (this.lastTableAdded == null) {
				setRootTable(table);
			} else {
				attachTable (lastTableAdded, table);
			}
			
			return this.lastTableAdded;
		}
		
		// should throw an exception if source is not included in the tree
		public virtual LogicalQueryTable JoinTable(LogicalQueryTable source, LogicalTable joinWith) {
			// first check if source is the last added node, if not search the whole tree
			if (!this.logicalTables.Contains(source)) {
				throw new ArgumentException("Source table is not present in the query.");
			}
			attachTable(source, joinWith);
			return this.lastTableAdded;
		}
		
		public virtual Query ToNativeQuery() {
			Query result = new Query();
			
			result.From.Source = this.fromSource;
			
			// add columns
			foreach (PhysicalQueryTable table in this.physicalTables) {
				result.Select.SelectedColumns.AddRange (table.GetSelectedColumns());	
			}
			
			return result;
		}
		
		private void setRootTable(LogicalTable rootTable) {
			
			string alias = createTableAlias(rootTable.PartOfTable.Name, 0);
			LogicalQueryTable queryTable = new LogicalQueryTable() {Table = rootTable, Alias = alias};
			PhysicalQueryTable physicalQueryTable = new PhysicalQueryTable() {Table = rootTable.PartOfTable, Alias = alias};
			this.logicalTables.Add (queryTable);
			this.physicalTables.Add (physicalQueryTable);
			this.fromSource = physicalQueryTable.ToTableReference();
			this.lastTableAdded = queryTable;
			
		}
		
		private void attachTable(LogicalQueryTable source, LogicalTable joinWith) {
			
			// check if join can be performed
			if (source.Table.Equals(joinWith)) {
				throw new ArgumentException("Cannot join a logical table with itself");
			} else if (source.Table.TableType.Equals (joinWith.TableType)) {
				throw new ArgumentException("Cannot join two entity logical or two join logical tables");
			}
			
			// join tables
			LogicalQueryTable logicalTable = new LogicalQueryTable() {Table = joinWith};
			if (source.Table.IsPartOfTheSameTable(joinWith)) {
				logicalTable.Alias = source.Alias;
			} else {
				string alias = createTableAlias(joinWith.PartOfTable.Name, howManyTimesUsed(joinWith));
				logicalTable.Alias = alias;
				
				PhysicalQueryTable physicalQueryTable = new PhysicalQueryTable() {Table = joinWith.PartOfTable, Alias = alias};
				this.physicalTables.Add (physicalQueryTable);
				
				LogicalQueryTable relationTable = (source.Table.TableType == LogicalTableType.JOIN ? source : logicalTable);
				LogicalQueryTable entityTable = (source.Table.TableType == LogicalTableType.ENTITY ? source : logicalTable);
				
				TableColumn primaryKey = entityTable.Table.GetPrimaryKey();
				TableColumn foreignKey = relationTable.Table.GetForeignKey(primaryKey);
				
				ILogicalCondition on = Logical.Equal(Expression.Column(entityTable.Alias, primaryKey.Name), Expression.Column (relationTable.Alias, foreignKey.Name));
				
				this.fromSource = From.Left (this.fromSource, logicalTable.ToTableReference(), on);
			}
			
			this.logicalTables.Add (logicalTable);
			this.lastTableAdded = logicalTable;
			
		}
		
		private int howManyTimesUsed(LogicalTable table) {
			return this.physicalTables.FindAll ((x) => x.Table.Equals(table.PartOfTable)).Count;
		}
		
		private string createTableAlias(string tableName, int index) {
			return tableName + "_" + index;	
		}
		
	}
	
}

