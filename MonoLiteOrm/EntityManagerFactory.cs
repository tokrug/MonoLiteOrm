using System;
using System.Text;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManagerFactory
	{
		private DatabaseMappings mappings;
		
		private PersistenceContextConfig config;
		
		public EntityManagerFactory (PersistenceContextConfig config, DatabaseMappings mappings)
		{
			this.config = config;
			this.mappings = mappings;
		}
		
		public EntityManager getEntityManager() {
			return new EntityManager(getNewConnection(), mappings);
		}
		
		public SqliteConnection getNewConnection() {
			SqliteConnectionStringBuilder conStringBuilder = new SqliteConnectionStringBuilder();
			conStringBuilder.Uri = "file:" + config.DatabaseName;
			conStringBuilder.Version = config.DatabaseVersion;
			SqliteConnection con = new SqliteConnection(conStringBuilder.ConnectionString);
			con.Open ();
			return con;
		}
		
		public string getSchemaScript() {
			StringBuilder build = new StringBuilder();
			foreach (ClassMapping map in mappings.getMappings()) {
				build.Append (map.CorrespondingTable.getTableSchema()).AppendLine();
			}
			return build.ToString();
		}
	}
}

