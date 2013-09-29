using System;

namespace Mono.Mlo
{
	public static class Select
	{
		
		public static SelectColumn Column(string tableName, string columnName, string alias) {
			return new SelectColumn() {Select = new ColumnExpression() {Table = tableName, Column = columnName}, Alias = alias};
		}
		
		public static SelectColumn Column(string tableName, string columnName) {
			return new SelectColumn() {Select = new ColumnExpression() {Table = tableName, Column = columnName}};
		}
		
		public static SelectColumn Column(string columnName) {
			return new SelectColumn() {Select = new ColumnExpression() {Column = columnName}};	
		}
		
	}
}

