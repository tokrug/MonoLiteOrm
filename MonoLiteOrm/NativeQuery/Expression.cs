using System;

namespace Mono.Mlo
{
	public static class Expression
	{
		
		public static IQueryExpression Column(string table, string column) {
			return new ColumnExpression() {Table = table, Column = column};	
		}
		
		public static IQueryExpression Column(string column) {
			return new ColumnExpression() {Column = column};	
		}
		
		public static IQueryExpression Parameter(string name) {
			return new Parameter() {Name = name};	
		}
		
	}
}

