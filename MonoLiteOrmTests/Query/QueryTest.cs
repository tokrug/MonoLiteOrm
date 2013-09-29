using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class QueryTest
	{
		[Test()]
		public void FullQueryTest ()
		{
			
			var tested = new Query();
			
			var mockSelect = new Mock<SelectClause>();
			mockSelect.Setup (x => x.ToQueryString()).Returns ("SELECT A");
			
			var mockFrom = new Mock<FromClause>();
			mockFrom.Setup (x => x.ToQueryString()).Returns ("FROM B");
			
			var mockWhere = new Mock<WhereClause>();
			mockWhere.Setup (x => x.ToQueryString()).Returns("WHERE B.A = 'A'");
			
			var mockOrder = new Mock<OrderClause>();
			mockOrder.Setup (x => x.ToQueryString()).Returns("ORDER BY B.A");
			
			var mockGroup = new Mock<GroupByClause>();
			mockGroup.Setup (x => x.ToQueryString()).Returns("GROUP BY B.A");
			
			var mockHaving = new Mock<HavingClause>();
			mockHaving.Setup (x => x.ToQueryString()).Returns("HAVING B.A > 2");
			
			tested.Select = mockSelect.Object;
			tested.From = mockFrom.Object;
			tested.Where = mockWhere.Object;
			tested.GroupBy = mockGroup.Object;
			tested.Having = mockHaving.Object;
			tested.Order = mockOrder.Object;
			
			string expected = "SELECT A FROM B WHERE B.A = 'A' GROUP BY B.A HAVING B.A > 2 ORDER BY B.A;";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void MinimalQueryTest ()
		{
			var tested = new Query();
			
			var mockSelect = new Mock<SelectClause>();
			mockSelect.Setup (x => x.ToQueryString()).Returns ("SELECT A");
			
			var mockFrom = new Mock<FromClause>();
			mockFrom.Setup (x => x.ToQueryString()).Returns ("FROM B");
			
			tested.Select = mockSelect.Object;
			tested.From = mockFrom.Object;
			
			string expected = "SELECT A FROM B;";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
		}
	}
}

