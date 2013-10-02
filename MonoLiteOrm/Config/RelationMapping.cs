using System;
using System.Reflection;

namespace Mono.Mlo
{
	public class RelationMapping<T, F> where T : new () where F : new ()
	{
		
		public virtual FieldInfo ClassField {get;set;}
		
		public virtual LogicalTable SourceTable {get;set;}
		public virtual LogicalTable JoinTable {get;set;}
		public virtual LogicalTable TargetTable {get;set;}
		
		public RelationMapping ()
		{
		}
	}
}

