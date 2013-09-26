using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class ValueSet
	{
		private List<string> values = new List<string>();
		
		public List<string> Values {get{return this.values;}}
		
		public string this[int i] {
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
		
		public void addParameter(string paramName) {
			values.Add ("@" + paramName);
		}
		
		public override string ToString ()
		{
			return String.Join (", ", values.ToArray());
		}
	}
}

