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
		// tree of joins
		// one table cannot appear here twice
		private Tree<LogicalQueryTable> joinTree = new Tree<LogicalQueryTable>();
		
		private TreeNode<LogicalQueryTable> lastAdded;
		
		public LogicalQuery ()
		{
		}
		
		// to be used for consecutive joins without forks
		public virtual LogicalQueryTable JoinTable(LogicalTable table) {
			if (this.lastAdded == null) {
				this.lastAdded = setRootTable (table);
			} else {
				this.lastAdded = attachTable (lastAdded, table);
			}
			
			return this.lastAdded.Value;
		}
		
		// should throw an exception if source is not included in the tree
		public virtual LogicalQueryTable JoinTable(LogicalQueryTable source, LogicalTable joinWith) {
			// first check if source is the last added node, if not search the whole tree
			TreeNode<LogicalQueryTable> sourceNode = (lastAdded.Value.Equals (source) ? lastAdded : joinTree.FindFirst (source));
			if (sourceNode == null) {
				throw new ArgumentException("Source table is not present in the query.");
			}
			this.lastAdded = attachTable(sourceNode, joinWith);
			return this.lastAdded.Value;
		}
		
		public virtual Query ToNativeQuery() {
			Query result = new Query();
			
			ITableExpression rootReference = joinTree.Root.Value.ToTableReference();
			
			result.From.Source = rootReference;
			
			// add columns to select
			foreach (TableColumn col in joinTree.Root.Value.Table.PartOfTable.getColumns()) {
				result.Select.SelectedColumns.Add (Select.Column(joinTree.Root.Value.Alias, col.Name));
			}
			
			joinTree.VisitPairs((x,y) => joinTables (result, x, y));
			
			return result;
		}
		
		
		private void joinTables(Query query, LogicalQueryTable source, LogicalQueryTable target) {
			
			// if both tables are in fact the same one then no join is necessary
			if (!source.Table.IsPartOfTheSameTable(target.Table)) {
				// should be changed, it is based on convention 
				LogicalQueryTable relationTable = (source.Table.GetColumns().Count == 2 ? source : target);
				LogicalQueryTable entityTable = (relationTable.Equals (source) ? target : source);
				
				TableColumn primaryKey = null;
				foreach (TableColumn col in entityTable.Table.GetColumns()) {
					if (col.IsPrimaryKey) {
						primaryKey = col;
						break;
					}
				}
				
				TableColumn foreignKey = null;
				foreach (TableColumn col in relationTable.Table.GetColumns()) {
					if (primaryKey.Equals (col.ReferencedColumn)) {
						foreignKey = col;
						break;
					}
				}
				
				ILogicalCondition on = Logical.Equal(Expression.Column(entityTable.Alias, primaryKey.Name), Expression.Column (relationTable.Alias, foreignKey.Name));
				
				query.From.Source = From.Left (query.From.Source, target.ToTableReference(), on);
				
				// add columns
				
				foreach (TableColumn col in target.Table.GetColumns()) {
					query.Select.SelectedColumns.Add (Select.Column(target.Alias, col.Name));
				}
				
			}
		}
		
		private TreeNode<LogicalQueryTable> setRootTable(LogicalTable rootTable) {
			string alias = createTableAlias(rootTable.PartOfTable.Name, 0);
			LogicalQueryTable queryTable = new LogicalQueryTable() {Table = rootTable, Alias = alias};
			return joinTree.SetRoot (queryTable);
		}
		
		private TreeNode<LogicalQueryTable> attachTable(TreeNode<LogicalQueryTable> sourceNode, LogicalTable joinWith) {
			LogicalQueryTable result = new LogicalQueryTable() {Table = joinWith};
			string alias;
			if (sourceNode.Value.Table.IsPartOfTheSameTable(joinWith)) {
				alias = sourceNode.Value.Alias;
			} else {
				alias = createTableAlias(joinWith.PartOfTable.Name, howManyTimesUsed(joinWith));
			}
			result.Alias = alias;
			return sourceNode.AddChild(result);
		}
		
		private int howManyTimesUsed(LogicalTable table) {
			return this.joinTree.FindAll ((x) => x.Table.Equals (table)).Count;
		}
		
		private string createTableAlias(string tableName, int index) {
			return tableName + "_" + index;	
		}
		
	}
	
}

