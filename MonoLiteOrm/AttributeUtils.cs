using System;
using System.Reflection;
using System.Collections.Generic;

namespace Mono.Ormo
{
	public static class AttributeUtils
	{
		
		public static IEnumerable<Type> GetTypesWithAttribute<T>(Assembly assembly) where T : System.Attribute {
		    foreach(Type type in assembly.GetTypes()) {
		        if (isAttributePresent<T>(type)) {
		            yield return type;
		        }
		    }
		}
		
		public static IEnumerable<Type> GetTypesWithAttribute<T>(IEnumerable<Assembly> assemblies) where T : System.Attribute {
			foreach (Assembly ass in assemblies) {
			    foreach(Type type in ass.GetTypes()) {
			        if (isAttributePresent<T>(type)) {
			            yield return type;
			        }
			    }
			}
		}
		
		public static IEnumerable<Type> GetTypesWithAttribute<T>() where T : System.Attribute {
			foreach(Assembly ass in AppDomain.CurrentDomain.GetAssemblies()) {
			    foreach(Type type in ass.GetTypes()) {
			        if (isAttributePresent<T>(type)) {
			            yield return type;
			        }
			    }
			}
		}
		
		public static T getSingleAttribute<T>(Type sourceClass) where T : System.Attribute {
			object[] attributes = sourceClass.GetCustomAttributes(typeof(T), true);
			if (attributes.Length > 0) {
				return (T) attributes[0];
			}
			return null;
		}
		
		public static T getSingleAttribute<T>(FieldInfo field) where T : System.Attribute {
			object[] attr = field.GetCustomAttributes(typeof(T), true);
			if (attr.Length > 0) {
				return (T) attr[0];
			}
			return null;
		}
		
		// useful for marker attributes
		public static bool isAttributePresent<T>(Type sourceClass) where T : System.Attribute {
			if (sourceClass.GetCustomAttributes(typeof(T), true).Length > 0) 
				return true;
			else
				return false;
		}
		
		public static bool isAttributePresent<T>(FieldInfo field) where T : System.Attribute {
			if (field.GetCustomAttributes(typeof(T), true).Length > 0) 
				return true;
			else
				return false;
		}
		
	}
}

