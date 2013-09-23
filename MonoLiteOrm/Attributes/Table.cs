using System;

namespace Mono.Ormo
{
	// optional attribute 
	// used if table name does not conform to convention
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class Table : System.Attribute
	{
		private string name;
		
		public string Name {get {return this.name;}}
		
		public Table (string name)
		{
			this.name = name;
		}
	}
}

