using System;
using System.Text;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class ValueSet
	{
		private List<IQueryExpression> values = new List<IQueryExpression>();
		
		public virtual List<IQueryExpression> Values {get{return this.values;}}
		
		public virtual IQueryExpression this[int i] {
			get {
				return this.values[i];	
			}
			set {
				this.values[i] = value;	
			}
		}
		
		public ValueSet ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append ("(");
			foreach (IQueryExpression exp in this.values) {
				builder.Append (exp.ToQueryString() + ", ");	
			}
			builder.Remove (builder.Length-2,2);
			builder.Append ("(");
			return builder.ToString();
		}
	}
}

