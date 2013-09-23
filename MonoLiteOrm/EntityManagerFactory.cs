using System;
using System.Text;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManagerFactory
	{
		private DatabaseMappings mappings;
		
		private PersistenceContextConfig config;
		
		public EntityManagerFactory (PersistenceContextConfig config)
		{
			this.config = config;
			mappings = new DatabaseMappings(this.config);
		}
		
		public EntityManager getEntityManager() {
			return new EntityManager(getNewConnection(), mappings);
		}
		
		public SqliteConnection getNewConnection() {
			string cs = "URI=file:"+config.DatabaseName+",version="+config.DatabaseVersion;
			SqliteConnection con = new SqliteConnection(cs);
			con.Open ();
			return con;
		}
		
		public string getSchemaScript() {
			StringBuilder build = new StringBuilder();
			foreach (ClassMapping map in mappings.getMappings()) {
				build.Append (map.getSchemaScript()).AppendLine();
			}
			return build.ToString();
		}
	}
}

