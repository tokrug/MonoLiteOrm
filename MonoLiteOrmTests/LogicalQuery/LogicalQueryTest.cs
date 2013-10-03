using NUnit.Framework;
using System;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class LogicalQueryTest
	{
		[Test()]
		public void TestCase ()
		{
			
			LogicalQuery query = new LogicalQuery();
			
			TableDefinition entityTable = new TableDefinition() {Name = "Entity"};
			TableColumn pk1 = new TableColumn() {IsPrimaryKey = true, Name = "id1"};
			TableColumn name1 = new TableColumn() {Name = "name1"};
			TableColumn name2 = new TableColumn() {Name = "name2"};
			entityTable.addColumn(pk1);
			entityTable.addColumn(name1);
			entityTable.addColumn(name2);
			
			TableDefinition secondEntity = new TableDefinition() {Name = "SecondEntity"};
			TableColumn pk2 = new TableColumn() {IsPrimaryKey = true, Name = "id"};
			TableColumn fk1 = new TableColumn() {IsForeignKey = true, Name = "fk1", ReferencedColumn = pk1};
			secondEntity.addColumn(pk2);
			secondEntity.addColumn(fk1);
			
			LogicalTable log1 = new LogicalTable() {Name = "Entity_Prop", PartOfTable = entityTable, TableType = LogicalTableType.ENTITY};
			log1.AddColumn(pk1);
			log1.AddColumn(name1);
			log1.AddColumn(name2);
			
			LogicalTable log2 = new LogicalTable() {Name = "Entity_SecondEntity", PartOfTable = secondEntity, TableType = LogicalTableType.JOIN};
			log2.AddColumn(pk2);
			log2.AddColumn(fk1);
			
			LogicalTable log3 = new LogicalTable() {Name = "SecondEntity_Prop", PartOfTable = secondEntity, TableType = LogicalTableType.ENTITY};
			log3.AddColumn(pk2);
			
			TableDefinition thirdEntity = new TableDefinition() {Name = "ThirdEntity"};
			TableColumn pk3 = new TableColumn() {IsPrimaryKey = true, Name = "pk3"};
			thirdEntity.addColumn(pk3);
			
			TableDefinition joinTable = new TableDefinition() {Name = "SecondEntity_ThirdEntity"};
			TableColumn join1 = new TableColumn() {IsForeignKey = true, Name = "fk2", ReferencedColumn = pk2};
			TableColumn join2 = new TableColumn() {IsForeignKey = true, Name = "fk3", ReferencedColumn = pk3};
			joinTable.addColumn(join1);
			joinTable.addColumn(join2);
			
			LogicalTable log4 = new LogicalTable() {Name = "SecondEntity_ThirdEntity", PartOfTable = joinTable, TableType = LogicalTableType.JOIN};
			log4.AddColumn(join1);
			log4.AddColumn(join2);
			
			LogicalTable log5 = new LogicalTable() {Name = "ThirdEntity", PartOfTable = thirdEntity, TableType = LogicalTableType.ENTITY};
			log5.AddColumn(pk3);
			
			query.JoinTable (log1);
			query.JoinTable (log2);
			query.JoinTable (log3);
			query.JoinTable (log4);
			query.JoinTable (log5);
			
			string actual = query.ToNativeQuery().ToQueryString ();
			Console.WriteLine(actual);
			string expected = "Dunno";
			
			Assert.AreEqual (expected, actual);
			
		}
	}
}

