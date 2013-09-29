using System;

namespace Mono.Mlo
{
	public class UnaryOperatorExpression : IQueryExpression
	{
		
		public virtual IQueryExpression Expression {get;set;}
		public virtual UnaryOperator Operation {get;set;}
		
		public UnaryOperatorExpression ()
		{
		}
		
		public virtual string ToQueryString() {
			return convertOperatorToString(Operation) + Expression.ToQueryString();
		}
		
		private string convertOperatorToString(UnaryOperator operation) {
			string result = "";
			switch (operation) {
			case UnaryOperator.PLUS: {
				result = "+";
				break;
			}
			case UnaryOperator.MINUS: {
				result = "-";
				break;
			}
			case UnaryOperator.BINARY_NOT: {
				result = "~";
				break;
			}
			}
			return result;
		}
	}
}

