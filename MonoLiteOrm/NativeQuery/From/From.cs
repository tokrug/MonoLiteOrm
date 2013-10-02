using System;

namespace Mono.Mlo
{
	public static class From
	{
		
		public static JoinedTables Inner(ITableExpression table, ITableExpression withTable, ILogicalCondition on, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, Alias = alias, JoinType = JoinType.INNERJOIN};
		}
		
		public static JoinedTables Inner(ITableExpression table, ITableExpression withTable, ILogicalCondition on) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, JoinType = JoinType.INNERJOIN};
		}
		
		public static JoinedTables Left(ITableExpression table, ITableExpression withTable, ILogicalCondition on, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, Alias = alias, JoinType = JoinType.LEFTJOIN};
		}
		
		public static JoinedTables Left(ITableExpression table, ITableExpression withTable, ILogicalCondition on) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, JoinType = JoinType.LEFTJOIN};
		}
		
		public static JoinedTables Cross(ITableExpression table, ITableExpression withTable, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, Alias = alias, JoinType = JoinType.CROSSJOIN};
		}
		
		public static JoinedTables Cross(ITableExpression table, ITableExpression withTable) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, JoinType = JoinType.CROSSJOIN};
		}
		
		public static TableReference Table(string tableName, string alias) {
			return new TableReference() {Name = tableName, Alias = alias};	
		}
		
		public static TableReference Table(string tableName) {
			return new TableReference() {Name = tableName};	
		}
		
		public static SubqueryTable Subquery(Query subquery, string alias) {
			return new SubqueryTable() {Query = subquery, Alias = alias};
		}
		
	}
}

