using System;
using System.Reflection;
using System.Collections.Generic;

namespace Mono.Mlo
{
	/// <summary>
	/// Persistence context config. Basic information needed for ORM to work.
	/// </summary>
	public class PersistenceContextConfig
	{
		private List<Assembly> assemblies = new List<Assembly>();
		
		public string DatabaseName {get;set;}
		public int DatabaseVersion {get;set;}
		public List<Assembly> Assemblies {get {return assemblies;}}
		
		public PersistenceContextConfig ()
		{
		}
		
		public void addAssembly(string assemblyName) {
			this.assemblies.Add (Assembly.Load (assemblyName));
		}
	}
}

