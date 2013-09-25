using System;

namespace Mono.Mlo
{
	public class OrderByElement
	{
		
		public string ColumnName {get;set;}
		public SortDirection Direction {get;set;}
		
		public OrderByElement ()
		{
		}
		
		public OrderByElement (string columnName, SortDirection direction) {
			this.ColumnName = columnName;
			this.Direction = direction;
		}
		
		public override string ToString ()
		{
			 return ColumnName + " " + directionToString (Direction);
		}
		
		private string directionToString (SortDirection direction) {
			return direction.Equals(SortDirection.ASC) ? "ASC" : "DESC";
		}
		
	}
}

