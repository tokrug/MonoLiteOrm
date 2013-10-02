using System;

namespace Mono.Mlo
{
	public class SubqueryTable : ITableExpression
	{
		
		public virtual Query Query {get;set;}
		public virtual string Alias {get;set;}
		
		public SubqueryTable ()
		{
		}
		
		public virtual ColumnExpression GetColumn(string columnName) {
			return new ColumnExpression() {Table = Alias, Column = columnName};
		}
		
		public virtual string ToQueryString() {
			return "(" + Query.ToQueryString() + ")" + (Alias == null ? "" : " " + Alias);
		}
	}
}

