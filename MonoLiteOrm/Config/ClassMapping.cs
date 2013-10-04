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
		
		private Dictionary<FieldInfo, RelationMapping<T, object>> relationMappings = new Dictionary<FieldInfo, RelationMapping<T, object>>();
		private List<RelationMapping<T, object>> relMappings = new List<RelationMapping<T, object>>();
		
		public virtual FieldMapping<T, object> IdMapping {get;set;}
		public virtual Type ClassType {get;set;}
		public virtual LogicalTable CorrespondingTable {get;set;}
		
		// cached queries, they won't change
		public virtual LogicalQuery SelectAllQuery {get;set;}
		public virtual LogicalQuery SelectSingleQuery {get;set;}
		
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
		
		public virtual void AddRelationMapping(RelationMapping<T, object> mapping) {
			relMappings.Add (mapping);
			relationMappings.Add (mapping.ClassField, mapping);
		}
		
		public ReadOnlyCollection<RelationMapping<T, object>> GetRelationMappings() {
			return new ReadOnlyCollection<RelationMapping<T, object>>(this.relMappings);	
		}
		
		public virtual object GetIdValue(T obj) {
			return this.IdMapping.GetValue (obj);	
		}
		
		public virtual void SetIdValue(T obj, object id) {
			this.IdMapping.SetValue(obj, id);	
		}
		
		public virtual F GetPropertyValue<F>(T obj, FieldMapping<T, F> field) {
			return field.GetValue(obj);	
		}
		
		public virtual void SetPropertyValue<F>(T obj, FieldMapping<T, F> field, F value) {
			field.SetValue(obj, value);	
		}
		
	}
}

