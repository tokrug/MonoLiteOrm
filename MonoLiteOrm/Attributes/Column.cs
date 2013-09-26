using System;

namespace Mono.Mlo
{
	// marks the field as persistent, default for private fields
	[AttributeUsage(AttributeTargets.Field)]
	public class Column : System.Attribute
	{
		private string name;
		
		public virtual string Name {get {return this.name;}}
		
		public Column (string name)
		{
			this.name = name;
		}
		
		public Column() {
			
		}
	}
}

