using System;
using System.Reflection;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class MonoLiteOrm
	{
		
		private MonoLiteOrm ()
		{
		}
		
		public static EntityManagerFactory getFactory(PersistenceContextConfig config) {
			AttributeConfigLoader configLoader = new AttributeConfigLoader();
			
			DatabaseMappings mappings = new DatabaseMappings();
			
			// convert names to assembly objects
			IEnumerable<Type> persistentTypes = AttributeUtils.GetTypesWithAttribute<Entity>(config.Assemblies);
			
			foreach (Type type in persistentTypes) {
				mappings.addMapping (configLoader.createMapping(type));	
			}
			
			return new EntityManagerFactory(config, mappings);
		}
		
	}
}

