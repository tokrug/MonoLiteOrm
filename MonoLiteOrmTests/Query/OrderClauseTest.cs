using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class OrderClauseTest
	{
		[Test()]
		public void AscendingTest ()
		{
			
			var orderExpression = new Mock<IQueryExpression>();
			orderExpression.Setup (x => x.ToQueryString ()).Returns ("A");
			
			var tested = new OrderByElement();
			tested.Direction = SortDirection.ASC;
			tested.Expression = orderExpression.Object;
			
			string expected = "A ASC";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void DescendingTest ()
		{
			
			var orderExpression = new Mock<IQueryExpression>();
			orderExpression.Setup (x => x.ToQueryString ()).Returns ("A");
			
			var tested = new OrderByElement();
			tested.Direction = SortDirection.DESC;
			tested.Expression = orderExpression.Object;
			
			string expected = "A DESC";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void MultiOrderTest ()
		{
			
			var order = new Mock<OrderByElement>();
			order.Setup (x => x.ToQueryString()).Returns("A ASC");
			
			var tested = new OrderClause();
			tested.OrderBy.Add (order.Object);
			tested.OrderBy.Add (order.Object);
			tested.OrderBy.Add (order.Object);
			
			string expected = "ORDER BY A ASC, A ASC, A ASC";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void NoOrderTest ()
		{
			
			var tested = new OrderClause();
			
			string expected = "";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
	}
}

