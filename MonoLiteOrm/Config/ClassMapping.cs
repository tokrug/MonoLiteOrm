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
	public class ClassMapping<T>
	{	
		private Dictionary<FieldInfo, FieldMapping<object>> propertyMappings = new Dictionary<FieldInfo, FieldMapping<object>>();
		private List<FieldMapping<object>> propMappings = new List<FieldMapping<object>>();
		
		public virtual ReadOnlyCollection<FieldMapping<object>> PropertyMappings {get{return new ReadOnlyCollection<FieldMapping<object>>(this.propMappings);}}
		public virtual FieldMapping<object> IdMapping {get;set;}
		public virtual Type ClassType {get;set;}
		public virtual TableDefinition CorrespondingTable {get;set;}
		
		public ClassMapping() {}
		
		/// <summary>
		/// Adds the property mapping. Not to be available at interface level. Remove counterpart is not required.
		/// </summary>
		/// <param name='mapping'>
		/// Mapping
		/// </param>
		public virtual void AddPropertyMapping(FieldMapping<object> mapping) {
			propMappings.Add (mapping);
			propertyMappings.Add (mapping.ClassField, mapping);	
		}
		
		public virtual object GetIdValue(T obj) {
			return this.IdMapping.GetValue (obj);	
		}
		
		public virtual void SetIdValue(T obj, int? id) {
			this.IdMapping.SetValue(obj, id);	
		}
		
		public virtual R GetPropertyValue<R>(T obj, FieldMapping<R> field) {
			return field.GetValue(obj);	
		}
		
		public virtual void SetPropertyValue<R>(T obj, FieldMapping<R> field, R value) {
			field.SetValue(obj, value);	
		}
		
	}
}

