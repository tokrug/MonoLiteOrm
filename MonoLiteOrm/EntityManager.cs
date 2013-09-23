using System;
using System.Text;
using Mono.Data.SqliteClient;

namespace Mono.Mlo
{
	public class EntityManager
	{
		// it has to be injected somehow
		private SqliteConnection con;
		private DatabaseMappings mapping;
		
		public EntityManager (SqliteConnection con, DatabaseMappings mappings)
		{
			this.con = con;
			this.mapping = mappings;
		}
		
		public T load<T>(int id) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getLoadQuery();
			query = String.Format (query, id);
			SqliteCommand cmd = new SqliteCommand(query, con);
			SqliteDataReader reader = cmd.ExecuteReader();
			if (reader.Read()) {
				return (T) mapp.toObject(reader);
			} else
				return default(T);
		}
		
		public void save<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getInsertQuery();
			query = mapp.toSQLParams(query, obj);
			SqliteCommand cmd = new SqliteCommand(query, con);
			int affectedRows = cmd.ExecuteNonQuery();
			mapp.setIdValue(obj, con.LastInsertRowId);
		}
		
		public void update<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getUpdateQuery();
			query = mapp.toSQLParams(query, obj);
			SqliteCommand cmd = new SqliteCommand(query, con);
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public void delete<T>(T obj) {
			ClassMapping mapp = mapping.getMapping<T>();
			string query = mapp.getDeleteQuery();
			query = String.Format (query, mapp.getIdValue(obj));
			//Debug.Log(query);
			SqliteCommand cmd = new SqliteCommand(query, con);
			int affectedRows = cmd.ExecuteNonQuery();
		}
		
		public System.Data.IDbTransaction startTransaction() {
			return con.BeginTransaction();	
		}
		
	}
}

