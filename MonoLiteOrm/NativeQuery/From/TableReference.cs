using System;

namespace Mono.Mlo
{
	public class TableReference : ITableExpression
	{
		
		public virtual string Name {get;set;}
		public virtual string Alias {get;set;}
		
		public TableReference ()
		{
		}
		
		public virtual ColumnExpression GetColumn(string columnName) {
			return new ColumnExpression() {Table = (Alias != null ? Alias : Name), Column = columnName};
		}
		
		public virtual string ToQueryString() {
			return Name + (Alias == null ? "" : " " + Alias);
		}
		
	}

}

