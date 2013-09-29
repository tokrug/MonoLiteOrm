using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class FromClauseTest
	{
		[Test()]
		public void ClauseTest ()
		{
			
			var mockTable = new Mock<ITableExpression>();
			mockTable.Setup (x => x.ToQueryString()).Returns ("A");
			
			var tested = new FromClause();
			tested.Source = mockTable.Object;
			
			string expected = "FROM A";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
			
		}
		
		[Test()]
		public void InnerJoinTest ()
		{
			
			var mockTable = new Mock<ITableExpression>();
			mockTable.Setup (x => x.ToQueryString()).Returns ("A");
			
			var mockTable2 = new Mock<ITableExpression>();
			mockTable2.Setup (x => x.ToQueryString()).Returns ("B");
			
			var mockCondition = new Mock<ILogicalCondition>();
			mockCondition.Setup (x => x.ToQueryString()).Returns ("A.Name = B.Name");
			
			string alias = "C";
			
			var tested = new JoinedTables();
			tested.Alias = alias;
			tested.Table = mockTable.Object;
			tested.JoinedWith = mockTable2.Object;
			tested.On = mockCondition.Object;
			tested.JoinType = JoinType.INNERJOIN;
			
			string expected = "(A INNER JOIN B ON A.Name = B.Name) C";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void LeftJoinTest ()
		{
			
			var mockTable = new Mock<ITableExpression>();
			mockTable.Setup (x => x.ToQueryString()).Returns ("A");
			
			var mockTable2 = new Mock<ITableExpression>();
			mockTable2.Setup (x => x.ToQueryString()).Returns ("B");
			
			var mockCondition = new Mock<ILogicalCondition>();
			mockCondition.Setup (x => x.ToQueryString()).Returns ("A.Name = B.Name");
			
			string alias = "C";
			
			var tested = new JoinedTables();
			tested.Alias = alias;
			tested.Table = mockTable.Object;
			tested.JoinedWith = mockTable2.Object;
			tested.On = mockCondition.Object;
			tested.JoinType = JoinType.LEFTJOIN;
			
			string expected = "(A LEFT JOIN B ON A.Name = B.Name) C";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void CrossJoinTest ()
		{
			
			var mockTable = new Mock<ITableExpression>();
			mockTable.Setup (x => x.ToQueryString()).Returns ("A");
			
			var mockTable2 = new Mock<ITableExpression>();
			mockTable2.Setup (x => x.ToQueryString()).Returns ("B");
			
			var mockCondition = new Mock<ILogicalCondition>();
			mockCondition.Setup (x => x.ToQueryString()).Returns ("A.Name = B.Name");
			
			string alias = "C";
			
			var tested = new JoinedTables();
			tested.Alias = alias;
			tested.Table = mockTable.Object;
			tested.JoinedWith = mockTable2.Object;
			tested.On = mockCondition.Object;
			tested.JoinType = JoinType.CROSSJOIN;
			
			string expected = "(A CROSS JOIN B) C";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void TableWithAlias ()
		{
			var tested = new TableReference();
			
			tested.Alias = "B";
			tested.Name = "A";
			
			string expected = "A B";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
		}
		
		[Test()]
		public void TableWithoutAlias ()
		{
			var tested = new TableReference();
			
			tested.Name = "A";
			
			string expected = "A";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
		}
		
		[Test()]
		public void SubqueryTest ()
		{
			var tested = new SubqueryTable();
			
			var mockQuery = new Mock<Query>();
			mockQuery.Setup (x => x.ToQueryString()).Returns ("SELECT x FROM y");
			
			tested.Query = mockQuery.Object;
			tested.Alias = "B";
			
			string expected = "(SELECT x FROM y) B";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual (expected, actual);
		}
		
	}
}

