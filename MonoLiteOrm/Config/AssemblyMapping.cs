using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.Mlo
{
	public class AssemblyMapping
	{
		private Dictionary<Type, ClassMapping<object>> classMappings = new Dictionary<Type, ClassMapping<object>>();
		
		public AssemblyMapping() {
	
		}
		
		public virtual ClassMapping<T> getMapping<T>() {
			return (ClassMapping<T>) Convert.ChangeType (classMappings[typeof(T)], typeof(ClassMapping<T>));
		}
		
		/// <summary>
		/// Returns the list of class mappings. It is an independent copy of the list used internally.
		/// </summary>
		/// <returns>
		/// The mappings.
		/// </returns>
		public virtual List<ClassMapping<object>> getMappings() {
			List<ClassMapping<object>> result = new List<ClassMapping<object>>();
			foreach (ClassMapping<object> map in classMappings.Values) {
				result.Add (map);
			}
			return result;
		}
		
		public virtual void addMapping(ClassMapping<object> mapping) {
			this.classMappings.Add (mapping.ClassType, mapping);	
		}
	}
}

