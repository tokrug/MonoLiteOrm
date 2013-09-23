using System;
using System.Reflection;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class Ormo
	{
		private Ormo ()
		{
		}
		
		public static EntityManagerFactory getFactory(PersistenceContextConfig config) {
			return new EntityManagerFactory(config);
		}
		
	}
}

