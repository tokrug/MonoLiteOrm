using System;
using System.Collections.Generic;

namespace Mono.Mlo
{
	public class TreeNode<T>
	{
		
		private List<TreeNode<T>> children = new List<TreeNode<T>>();
		
		public virtual T Value {get;set;}
		public virtual List<TreeNode<T>> Children {get{return this.children;}}
		
		public TreeNode ()
		{
		}
		
		public virtual TreeNode<T> AddChild(T child) {
			TreeNode<T> node = new TreeNode<T>() {Value = child};
			this.children.Add (node);
			return node;
		}
	}
}

