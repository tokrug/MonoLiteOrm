using System;

namespace Mono.Mlo
{
	/// <summary>
	/// Data column.
	/// </summary>
	public class DataColumn
	{
		
		public virtual string Name {get;set;}
		public virtual DataTable Table {get;set;}
		
		public DataColumn ()
		{
		}
	}
}

