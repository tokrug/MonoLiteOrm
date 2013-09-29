using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class GroupByTest
	{
		[Test()]
		public void EmptyTest ()
		{
			
			var tested = new GroupByClause();
			
			string expected = "";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void MultiGroupTest ()
		{
			
			var tested = new GroupByClause();
			
			var mockExpression = new Mock<IQueryExpression>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			tested.Expressions.Add (mockExpression.Object);
			tested.Expressions.Add (mockExpression.Object);
			tested.Expressions.Add (mockExpression.Object);
			
			string expected = "GROUP BY A, A, A";
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
	}
}

