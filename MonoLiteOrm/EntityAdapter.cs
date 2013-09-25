using System;
using System.Data;

namespace Mono.Mlo
{
	public class EntityAdapter<T> where T : new()
	{
		private ClassMapping classMapping;
		private IDbConnection con;
		
		public EntityAdapter (ClassMapping mapping, IDbConnection connection)
		{
			this.classMapping = mapping;
			this.con = connection;
		}
		
		
		
	}
}

