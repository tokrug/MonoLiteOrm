using System;
using System.Reflection;
using System.Data;

namespace Mono.Mlo
{
	/// <summary>
	/// Field mapping. T denotes type specified by this field.
	/// </summary>
	public class FieldMapping<T>
	{	
		public virtual FieldInfo ClassField {get;set;}
		public virtual TableColumn Column {get;set;}
		public virtual bool IsId {get;set;}
		
		public FieldMapping() {}
		
		public virtual T GetValue(object obj) {
			return (T) ClassField.GetValue (obj);
		}
		
		public virtual void SetValue(object obj, T value) {
			ClassField.SetValue(obj, value);	
		}
		
	}
}

