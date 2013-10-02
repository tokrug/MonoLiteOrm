using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class Tree<T>
	{
		
		public virtual TreeNode<T> Root {get;set;}
		
		public Tree ()
		{
		}
		
		// broad search
		public virtual void Visit(System.Action<TreeNode<T>> action) {
			Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
			if (Root != null) {
				queue.Enqueue(Root);
				while (queue.Count > 0) {
					TreeNode<T> currentNode = queue.Dequeue ();
					action(currentNode);
					foreach (TreeNode<T> child in currentNode.Children) {
						queue.Enqueue(child);	
					}
				}
			}
		}
		
		public virtual void VisitPairs(Action<T,T> action) {
			Visit((x) => x.Children.ForEach ((y) => action(x.Value, y.Value)));
		}
		
		public virtual TreeNode<T> SetRoot(T root) {
			TreeNode<T> result = new TreeNode<T>() {Value = root};
			Root = result;
			return result;
		}
		
		public virtual TreeNode<T> FindFirst(System.Predicate<T> match) {
			TreeNode<T> result = null;
			Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
			if (Root != null) {
				queue.Enqueue(Root);
				while (queue.Count > 0) {
					TreeNode<T> currentNode = queue.Dequeue ();
					if (match(currentNode.Value)) {
						result = currentNode;
						break;
					}
					foreach (TreeNode<T> child in currentNode.Children) {
						queue.Enqueue(child);	
					}
				}
			}
			return result;
		}
		
		public virtual TreeNode<T> FindFirst(T value) {
			TreeNode<T> result = null;
			Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
			if (Root != null) {
				queue.Enqueue(Root);
				while (queue.Count > 0) {
					TreeNode<T> currentNode = queue.Dequeue ();
					if (currentNode.Value.Equals (value)) {
						result = currentNode;
						break;
					}
					foreach (TreeNode<T> child in currentNode.Children) {
						queue.Enqueue(child);	
					}
				}
			}
			return result;
		}
		
		public virtual List<TreeNode<T>> FindAll(System.Predicate<T> match) {
			List<TreeNode<T>> result = new List<TreeNode<T>>();	
			Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
			if (Root != null) {
				queue.Enqueue(Root);
				while (queue.Count > 0) {
					TreeNode<T> currentNode = queue.Dequeue ();
					if (match(currentNode.Value)) {
						result.Add (currentNode);
					}
					foreach (TreeNode<T> child in currentNode.Children) {
						queue.Enqueue(child);	
					}
				}
			}
			return result;
		}
		
		public virtual bool IsEmpty() {
			return Root == null;	
		}
		
	}
}

