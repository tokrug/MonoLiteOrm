using System;
using System.Text;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManagerFactory
	{
		private AssemblyMapping mappings;
		
		private PersistenceContextConfig config;
		
		public EntityManagerFactory (PersistenceContextConfig config, AssemblyMapping mappings)
		{
			this.config = config;
			this.mappings = mappings;
		}
		
		public virtual EntityManager getEntityManager() {
			return new EntityManager(getNewConnection(), mappings);
		}
		
		public virtual SqliteConnection getNewConnection() {
			SqliteConnectionStringBuilder conStringBuilder = new SqliteConnectionStringBuilder();
			conStringBuilder.Uri = config.DatabaseUri;
			conStringBuilder.Version = config.DatabaseVersion;
			SqliteConnection con = new SqliteConnection(conStringBuilder.ConnectionString);
			con.Open ();
			return con;
		}
		
		public virtual string getSchemaScript() {
			StringBuilder build = new StringBuilder();
			foreach (ClassMapping<object> map in mappings.GetMappings()) {
				build.Append (map.CorrespondingTable.getTableSchema()).AppendLine();
			}
			return build.ToString();
		}
	}
}

