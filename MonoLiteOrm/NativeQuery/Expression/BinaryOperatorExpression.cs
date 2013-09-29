using System;

namespace Mono.Mlo
{
	public class BinaryOperatorExpression : IQueryExpression
	{
		
		public virtual IQueryExpression LeftExpression {get;set;}
		public virtual IQueryExpression RightExpression {get;set;}
		public virtual BinaryOperator Operation {get;set;}
		
		public BinaryOperatorExpression ()
		{
		}
		
		public virtual string ToQueryString() {
			return LeftExpression.ToQueryString() + convertOperatorToString(Operation) + RightExpression.ToQueryString();	
		}
		
		private string convertOperatorToString(BinaryOperator operation) {
			string result = "";
			switch (operation) {
			case BinaryOperator.PLUS: {
				result = "+";
				break;
			}
			case BinaryOperator.MINUS: {
				result = "-";
				break;
			}
			case BinaryOperator.MULTIPLY: {
				result = "*";
				break;
			}
			case BinaryOperator.DIVIDE: {
				result = "/";
				break;
			}
			case BinaryOperator.MODULO: {
				result = "%";
				break;
			}
			case BinaryOperator.SHIFT_LEFT: {
				result = "<<";
				break;
			}
			case BinaryOperator.SHIFT_RIGHT: {
				result = ">>";
				break;
			}
			case BinaryOperator.BINARY_AND: {
				result = "&";
				break;
			}
			case BinaryOperator.BINARY_OR: {
				result = "|";
				break;
			}
			case BinaryOperator.CONCAT: {
				result = "||";
				break;
			}
			}
			return result;
		}
	}
}

