using System;

namespace Mono.Mlo
{
	public class ColumnExpression : IQueryExpression
	{
		public virtual string Table {get;set;}
		public virtual string Column {get;set;}
		
		public ColumnExpression ()
		{
		}
		
		public virtual string ToQueryString() {
			return (Table == null ? "" : Table + ".") + Column;
		}
		
	}
}

