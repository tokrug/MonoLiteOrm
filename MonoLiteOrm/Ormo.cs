using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.Ormo
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

