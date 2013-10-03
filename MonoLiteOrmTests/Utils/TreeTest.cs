using NUnit.Framework;
using System;
using Mono.Mlo;

namespace MonoLiteOrmTests
{
	[TestFixture()]
	public class TreeTest
	{
		[Test()]
		public void VisitCountTest ()
		{
			
			Tree<string> tested = new Tree<string>();
			
			TreeNode<string> root = tested.SetRoot("A");
			root.AddChild("B");
			root.AddChild("C");
			
			int actualCount = 0;
			
			tested.Visit((x) => actualCount++);
			
			int expectedCount = 3;
			
			Assert.AreEqual(expectedCount, actualCount);
			
		}
		
		[Test()]
		public void VisitPairCountTest ()
		{
			Tree<string> tested = new Tree<string>();
			
			TreeNode<string> root = tested.SetRoot("A");
			root.AddChild("B");
			root.AddChild("C");
			
			int actualCount = 0;
			
			tested.VisitPairs((x,y) => actualCount++);
			
			int expectedCount = 2;
			
			Assert.AreEqual(expectedCount, actualCount);	
		}
		
		[Test()]
		public void FindAllTest ()
		{
			Tree<string> tested = new Tree<string>();
			
			TreeNode<string> root = tested.SetRoot("A");
			root.AddChild("B");
			root.AddChild("C");
			
			int actualCount = tested.FindAll ((x) => x.Equals ("A") || x.Equals("B")).Count;
			
			int expectedCount = 2;
			
			Assert.AreEqual(expectedCount, actualCount);
		}
		
		[Test()]
		public void FindFirstNullResultTest ()
		{
			Tree<string> tested = new Tree<string>();
			
			TreeNode<string> root = tested.SetRoot("A");
			root.AddChild("B");
			root.AddChild("C");
			
			TreeNode<string> found = tested.FindFirst ((x) => x.Equals ("D"));
			
			Assert.IsNull(found);
		}
		
	}
}

