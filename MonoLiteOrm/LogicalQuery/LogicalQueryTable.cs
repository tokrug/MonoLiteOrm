using System;

namespace Mono.Mlo
{
	
	/// <summary>
	/// Logical query table. 
	/// </summary>
	public class LogicalQueryTable
	{
		
		public virtual LogicalTable Table {get;set;}
		public virtual string Alias {get;set;}
		
		public LogicalQueryTable ()
		{
		}
		
		public TableReference ToTableReference() {
			return From.Table (Table.PartOfTable.Name, Alias);
		}
		
	}
}

