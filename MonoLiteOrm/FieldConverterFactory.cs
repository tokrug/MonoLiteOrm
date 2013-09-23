using System;
using System.Reflection;

namespace Mono.Ormo
{
	public class FieldConverterFactory
	{
		private FieldConverterFactory ()
		{
		}
		
		public static FieldConverter getConverter(Type type) {
			if (type.Equals(typeof(int)) || type.Equals(typeof(int?))) {
				return new IntFieldConverter();
			} else if (type.Equals(typeof(string))) {
				return new StringFieldConverter();	
			} else if (type.Equals(typeof(float)) || type.Equals(typeof(float?))) {
				return new FloatFieldConverter();
			} else if (type.Equals(typeof(double)) || type.Equals(typeof(double?))) {
				return new DoubleFieldConverter();
			} else if (type.Equals(typeof(bool)) || type.Equals(typeof(bool?))) {
				return new DoubleFieldConverter();
			} else if (type.Equals(typeof(DateTime))) {
				return new DateFieldConverter();
			}
			return null;
		}
		
	}
}

