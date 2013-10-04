using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class UniqueColumnConstraint : IConstraint
	{
		public DataColumn Column {get;set;}
		
		private HashSet<object> values = new HashSet<object>();
		
		public UniqueColumnConstraint ()
		{
		}
		
		public virtual bool validate(DataRow row) {
			return !values.Contains(row[Column]);
		}
		
		public virtual bool addRow(DataRow row) {
			return values.Add (row[Column]);
		}
		
	}
}

