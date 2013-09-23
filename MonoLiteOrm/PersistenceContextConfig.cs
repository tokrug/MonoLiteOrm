using System;
using System.Collections.Generic;

namespace Mono.Ormo
{
	// assemblies and database name
	public class PersistenceContextConfig
	{
		private List<string> assemblies = new List<string>();
		
		public string DatabaseName {get;set;}
		public int DatabaseVersion {get;set;}
		public List<string> Assemblies {get {return assemblies;}}
		
		public PersistenceContextConfig ()
		{
			
		}
	}
}

