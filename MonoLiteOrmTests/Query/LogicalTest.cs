using NUnit.Framework;
using System;
using Mono.Mlo;
using Moq;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class LogicalTest
	{
		[Test()]
		public void AndSingleTest ()
		{
			
			var mockExpression = new Mock<ILogicalCondition>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			LogicalAnd tested = new LogicalAnd();
			tested.Conditions.Add (mockExpression.Object);
			
			string expected = "A";
			
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void AndTripleTest ()
		{
			
			var mockExpression = new Mock<ILogicalCondition>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			LogicalAnd tested = new LogicalAnd();
			tested.Conditions.Add (mockExpression.Object);
			tested.Conditions.Add (mockExpression.Object);
			tested.Conditions.Add (mockExpression.Object);
			
			string expected = "(A AND A AND A)";
			
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void OrSingleTest ()
		{
			
			var mockExpression = new Mock<ILogicalCondition>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			LogicalOr tested = new LogicalOr();
			tested.Conditions.Add (mockExpression.Object);
			
			string expected = "A";
			
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void OrTripleTest ()
		{
			
			var mockExpression = new Mock<ILogicalCondition>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			LogicalOr tested = new LogicalOr();
			tested.Conditions.Add (mockExpression.Object);
			tested.Conditions.Add (mockExpression.Object);
			tested.Conditions.Add (mockExpression.Object);
			
			string expected = "(A OR A OR A)";
			
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
		
		[Test()]
		public void NotTest ()
		{
			
			var mockExpression = new Mock<ILogicalCondition>();
			mockExpression.Setup (x => x.ToQueryString()).Returns ("A");
			
			var tested = new LogicalNot();
			tested.Condition = mockExpression.Object;
			
			string expected = "NOT A";
			
			string actual = tested.ToQueryString();
			
			Assert.AreEqual(expected, actual);
			
		}
	}
}

