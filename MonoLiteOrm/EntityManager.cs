using System;
using System.Text;
using System.Data;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManager
	{
		// it has to be injected somehow
		private IDbConnection con;
		private DatabaseMappings mapping;
		
		private Transaction transaction;
		
		public EntityManager (IDbConnection con, DatabaseMappings mappings)
		{
			this.con = con;
			this.mapping = mappings;
		}
		
		public T load2<T>(int id) where T : new() {
			ClassMapping classMapping = mapping.getMapping<T>();
			EntityAdapter<T> adapt = new EntityAdapter<T>(classMapping, this.con);
			string query = classMapping.getLoadQuery();
			query = String.Format (query, id);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataReader reader = cmd.ExecuteReader();
			if (reader.Read()) {
				return (T) classMapping.toObject(reader);
			} else
				return default(T);
		}
		
		public T load<T>(int id) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getLoadQuery();
			query = String.Format (query, id);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			IDataReader reader = cmd.ExecuteReader();
			if (reader.Read()) {
				return (T) mapp.toObject(reader);
			} else
				return default(T);
		}
		
		public void save<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getInsertQuery();
			query = mapp.toSQLParams(query, obj);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			int affectedRows = cmd.ExecuteNonQuery();
			// does not retrieve ID!!!!!!!!!!!!!!!!
		}
		
		public void update<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getUpdateQuery();
			query = mapp.toSQLParams(query, obj);
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public void delete<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getDeleteQuery();
			query = String.Format (query, mapp.getIdValue(obj));
			IDbCommand cmd = con.CreateCommand();
			cmd.CommandText = query;
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public Transaction startTransaction() {
			this.transaction = new Transaction(con.BeginTransaction());
			return this.transaction;
		}
		
	}
}

