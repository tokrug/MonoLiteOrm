using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mono.Mlo
{
	/// <summary>
	/// Logical query.
	/// All tables are added to the join tree. If a pair of logical tables is a part of the same physical table then no join is performed.
	/// Also the returned queryTable object will have the same alias for both logical tables.
	/// 
	/// Selft join not supported just yet
	/// </summary>
	public class LogicalQueryBuilder
	{
		
		// to know what columns should be used as a link between tables
		private List<LogicalQueryTable> logicalTables;
		// to know what columns should be selected
		private List<PhysicalQueryTable> physicalTables;
		
		// small addition to ease developers life (altough it complicates a bit code in this class)
		private LogicalQueryTable lastTableAdded;
		
		// refreshed after every join
		private ITableExpression fromSource;
		
		public LogicalQueryBuilder ()
		{
			this.logicalTables = new List<LogicalQueryTable>();
			this.physicalTables = new List<PhysicalQueryTable>();
		}
		
		// to be used for consecutive joins without forks
		public virtual LogicalQueryTable JoinTable(LogicalTable table) {
			
			if (this.lastTableAdded == null) {
				setRootTable (table);
			} else {
				attachTable (this.lastTableAdded, table);
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
			
			// attach previously created source
			result.From.Source = this.fromSource;
			
			// add columns
			foreach (PhysicalQueryTable table in this.physicalTables) {
				result.Select.SelectedColumns.AddRange (table.GetSelectedColumns());	
			}
			
			return result;
		}
		
		public virtual LogicalQuery ToLogicalQuery() {
			LogicalQuery query = new LogicalQuery();
			query.NativeQuery = ToNativeQuery ().ToQueryString ();
			query.LogicalTables = new ReadOnlyCollection<LogicalQueryTable>(this.logicalTables);
			return query;
		}
		
		// should be useful when transfering the data to dataset
		public ReadOnlyCollection<LogicalQueryTable> GetLogicalTables() {
			return new ReadOnlyCollection<LogicalQueryTable>(this.logicalTables);	
		}
		
		private void setRootTable(LogicalTable rootTable) {
			string alias = createTableAlias(rootTable);
			LogicalQueryTable queryTable = new LogicalQueryTable() {Table = rootTable, Alias = alias};
			PhysicalQueryTable physicalQueryTable = new PhysicalQueryTable() {Table = rootTable.PartOfTable, Alias = alias};
			this.logicalTables.Add (queryTable);
			this.physicalTables.Add (physicalQueryTable);
			this.fromSource = physicalQueryTable.ToTableReference();
			this.lastTableAdded = queryTable;
		}
		
		private void attachTable(LogicalQueryTable source, LogicalTable joinWith) {
			
			// check if join can be performed, if not then exception is thrown
			validateAttachArguments(source, joinWith);
			
			// join tables
			LogicalQueryTable target = new LogicalQueryTable() {Table = joinWith};
			
			// if the join table is in fact a part of another physical table already included in source logical table
			// then dont join them
			// always join tables if the target table is of entity type
			if (!isJoinRequired (source, target)) {
				target.Alias = source.Alias;
			} else {
				string alias = createTableAlias(joinWith);
				target.Alias = alias;
				
				PhysicalQueryTable physicalQueryTable = new PhysicalQueryTable() {Table = joinWith.PartOfTable, Alias = alias};
				this.physicalTables.Add (physicalQueryTable);
				
				this.fromSource = createNewJoin (this.fromSource, source, target);
			}
			
			this.logicalTables.Add (target);
			this.lastTableAdded = target;
			
		}
		
		private bool isJoinRequired(LogicalQueryTable source, LogicalQueryTable target) {
			return !source.IsLogicalTableEqual(target);
		}
		
		private void validateAttachArguments(LogicalQueryTable source, LogicalTable joinWith) {
			if (source.Table.Equals(joinWith)) {
				throw new ArgumentException("Cannot join a logical table with itself");
			} else if (source.Table.TableType.Equals (joinWith.TableType)) {
				throw new ArgumentException("Cannot join two entity logical or two join logical tables");
			}
		}
		
		private ITableExpression createNewJoin(ITableExpression oldJoin, LogicalQueryTable sourceTable, LogicalQueryTable targetTable) {
			LogicalQueryTable relationTable = (sourceTable.Table.TableType == LogicalTableType.JOIN ? sourceTable : targetTable);
			LogicalQueryTable entityTable = (sourceTable.Table.TableType == LogicalTableType.ENTITY ? sourceTable : targetTable);
				
			TableColumn primaryKey = entityTable.Table.GetPrimaryKey();
			TableColumn foreignKey = relationTable.Table.GetForeignKey(primaryKey);
				
			ILogicalCondition on = Logical.Equal(Expression.Column(entityTable.Alias, primaryKey.Name), Expression.Column (relationTable.Alias, foreignKey.Name));
				
			return From.Left (oldJoin, targetTable.ToTableReference(), on);
		}
		
		private int howManyTimesUsed(TableDefinition table) {
			return this.physicalTables.FindAll ((x) => x.Table.Equals(table)).Count;
		}
		
		private string createTableAlias(LogicalTable logicalTable) {
			return createTableAlias(logicalTable.PartOfTable.Name, howManyTimesUsed(logicalTable.PartOfTable));
		}
		
		private string createTableAlias(string tableName, int index) {
			return tableName + "_" + index;	
		}
		
	}
	
}

