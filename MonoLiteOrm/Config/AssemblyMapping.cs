using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.Mlo
{
	public class AssemblyMapping
	{
		private Dictionary<Type, ClassMapping> classMappings = new Dictionary<Type, ClassMapping>();
		
		public AssemblyMapping() {
	
		}
		
		public virtual ClassMapping getMapping<T>() {
			return classMappings[typeof(T)];
		}
		
		/// <summary>
		/// Returns the list of class mappings. It is an independent copy of the list used internally.
		/// </summary>
		/// <returns>
		/// The mappings.
		/// </returns>
		public virtual List<ClassMapping> getMappings() {
			List<ClassMapping> result = new List<ClassMapping>();
			foreach (ClassMapping map in classMappings.Values) {
				result.Add (map);
			}
			return result;
		}
		
		public virtual void addMapping(ClassMapping mapping) {
			this.classMappings.Add (mapping.ClassType, mapping);	
		}
	}
}

