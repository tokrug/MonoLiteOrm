using System;

namespace Mono.Ormo
{
	// marks the field as persistent, default for private fields
	[AttributeUsage(AttributeTargets.Field)]
	public class Column : System.Attribute
	{
		private string name;
		public string Name {get {return this.name;}}
		
		public Column (string name)
		{
			this.name = name;
		}
		
		public Column() {
			
		}
	}
}

