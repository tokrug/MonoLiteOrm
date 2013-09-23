using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.Mlo
{
	public class DatabaseMappings
	{
		private Dictionary<Type, ClassMapping> classMappings = new Dictionary<Type, ClassMapping>();
		
		public DatabaseMappings(PersistenceContextConfig config) {
			// convert names to assembly objects
			IEnumerable<Assembly> assemblies = getAssemblies(config.Assemblies);
			IEnumerable<Type> persistentTypes = AttributeUtils.GetTypesWithAttribute<Entity>(assemblies);
			
			foreach (Type type in persistentTypes) {
				this.classMappings.Add (type, new ClassMapping(type));	
			}
	
		}
		
		private static IEnumerable<Assembly> getAssemblies(IEnumerable<string> assNames) {
			foreach (string name in assNames) {
				yield return Assembly.Load (name);	
			}
		}
		
		public ClassMapping getMapping<T>() {
			return classMappings[typeof(T)];
		}
		
		public List<ClassMapping> getMappings() {
			List<ClassMapping> result = new List<ClassMapping>();
			foreach (ClassMapping map in classMappings.Values) {
				result.Add (map);
			}
			return result;
		}
	}
}

