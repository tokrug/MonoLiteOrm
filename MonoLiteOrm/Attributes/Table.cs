using System;

namespace Mono.Mlo
{
	// optional attribute 
	// used if table name does not conform to convention
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class Table : System.Attribute
	{
		private string name;
		
		public virtual string Name {get {return this.name;}}
		
		public Table (string name)
		{
			this.name = name;
		}
	}
}

