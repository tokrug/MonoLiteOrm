using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Data;

namespace Mono.Mlo
{
	/// <summary>
	/// Class mapping. T is the class being mapped.
	/// </summary>
	public class ClassMapping<T> where T : new()
	{	
		private Dictionary<FieldInfo, FieldMapping<T, object>> propertyMappings = new Dictionary<FieldInfo, FieldMapping<T, object>>();
		private List<FieldMapping<T, object>> propMappings = new List<FieldMapping<T, object>>();
		
		public virtual FieldMapping<T, object> IdMapping {get;set;}
		public virtual Type ClassType {get;set;}
		public virtual TableDefinition CorrespondingTable {get;set;}
		
		public ClassMapping() {}
		
		/// <summary>
		/// Adds the property mapping. Not to be available at interface level. Remove counterpart is not required.
		/// </summary>
		/// <param name='mapping'>
		/// Mapping
		/// </param>
		public virtual void AddPropertyMapping(FieldMapping<T, object> mapping) {
			propMappings.Add (mapping);
			propertyMappings.Add (mapping.ClassField, mapping);	
		}
		
		public ReadOnlyCollection<FieldMapping<T, object>> GetPropertyMappings() {
			return new ReadOnlyCollection<FieldMapping<T, object>>(this.propMappings);
		}
		
		public virtual object GetIdValue(T obj) {
			return this.IdMapping.GetValue (obj);	
		}
		
		public virtual void SetIdValue(T obj, object id) {
			this.IdMapping.SetValue(obj, id);	
		}
		
		public virtual R GetPropertyValue<R>(T obj, FieldMapping<T, R> field) {
			return field.GetValue(obj);	
		}
		
		public virtual void SetPropertyValue<R>(T obj, FieldMapping<T, R> field, R value) {
			field.SetValue(obj, value);	
		}
		
	}
}

