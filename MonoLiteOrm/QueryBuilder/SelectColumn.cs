using System;

namespace Mono.Mlo
{
	public class SelectColumn
	{
		
		public string TableName {get;set;}
		public string ColumnName {get;set;}
		public string Alias {get;set;}
		
		public SelectColumn ()
		{
		}
		
		public override string ToString ()
		{
			return (TableName == null ? "" : TableName + ".") + ColumnName + (Alias == null ? "" : " " + Alias);
		}
	}
}

