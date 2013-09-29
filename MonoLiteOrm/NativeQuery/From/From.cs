using System;

namespace Mono.Mlo
{
	public static class From
	{
		
		public static ITableExpression Inner(ITableExpression table, ITableExpression withTable, ILogicalCondition on, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, Alias = alias, JoinType = JoinType.INNERJOIN};
		}
		
		public static ITableExpression Left(ITableExpression table, ITableExpression withTable, ILogicalCondition on, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, Alias = alias, JoinType = JoinType.LEFTJOIN};
		}
		
		public static ITableExpression Right(ITableExpression table, ITableExpression withTable, ILogicalCondition on, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, On = on, Alias = alias, JoinType = JoinType.RIGHTJOIN};
		}
		
		public static ITableExpression Cross(ITableExpression table, ITableExpression withTable, string alias) {
			return new JoinedTables() {Table = table, JoinedWith = withTable, Alias = alias, JoinType = JoinType.CROSSJOIN};
		}
		
		public static ITableExpression Table(string tableName, string alias) {
			return new TableReference() {Name = tableName, Alias = alias};	
		}
		
		public static ITableExpression Table(string tableName) {
			return new TableReference() {Name = tableName};	
		}
		
		public static ITableExpression Subquery(Query subquery, string alias) {
			return new SubqueryTable() {Query = subquery, Alias = alias};
		}
		
	}
}

