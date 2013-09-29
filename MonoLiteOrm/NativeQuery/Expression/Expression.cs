using System;

namespace Mono.Mlo
{
	public static class Expression
	{
		
		public static IQueryExpression Column(string table, string column) {
			return new ColumnExpression() {Table = table, Column = column};	
		}
		
		public static IQueryExpression Column(string column) {
			return new ColumnExpression() {Column = column};	
		}
		
		public static IQueryExpression Parameter(string name) {
			return new Parameter() {Name = name};	
		}
		
		public static IQueryExpression Plus(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.PLUS};	
		}
		
		public static IQueryExpression Minus(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.MINUS};
		}
		
		public static IQueryExpression Times(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.MULTIPLY};
		}
		
		public static IQueryExpression Divide(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.DIVIDE};
		}
		
		public static IQueryExpression Mod(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.MODULO};
		}
		
		public static IQueryExpression ShiftLeft(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.SHIFT_LEFT};
		}
		
		public static IQueryExpression ShiftRight(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.SHIFT_RIGHT};
		}
		
		public static IQueryExpression BinaryAnd(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.BINARY_AND};	
		}
		
		public static IQueryExpression BinaryOr(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.BINARY_OR};	
		}
		
		public static IQueryExpression Concat(IQueryExpression leftExpression, IQueryExpression rightExpression) {
			return new BinaryOperatorExpression() {LeftExpression = leftExpression, RightExpression = rightExpression, Operation = BinaryOperator.CONCAT};
		}
		
		public static IQueryExpression Plus(IQueryExpression expression) {
			return new UnaryOperatorExpression() {Expression = expression, Operation = UnaryOperator.PLUS};	
		}
		
		public static IQueryExpression Minus(IQueryExpression expression) {
			return new UnaryOperatorExpression() {Expression = expression, Operation = UnaryOperator.MINUS};	
		}
		
		public static IQueryExpression BinaryNot(IQueryExpression expression) {
			return new UnaryOperatorExpression() {Expression = expression, Operation = UnaryOperator.BINARY_NOT};	
		}
		
		public static IQueryExpression Count(IQueryExpression expression) {
			return new AggregateExpression() {Expression = expression, Aggregate = AggregateFunction.COUNT};	
		}
		
		public static IQueryExpression Count() {
			return new AggregateExpression() {Expression = Expression.Asterisk(), Aggregate = AggregateFunction.COUNT};	
		}
		
		public static IQueryExpression Max(IQueryExpression expression) {
			return new AggregateExpression() {Expression = expression, Aggregate = AggregateFunction.MAX};	
		}
		
		public static IQueryExpression Min(IQueryExpression expression) {
			return new AggregateExpression() {Expression = expression, Aggregate = AggregateFunction.MIN};
		}
		
		public static IQueryExpression Average(IQueryExpression expression) {
			return new AggregateExpression() {Expression = expression, Aggregate = AggregateFunction.AVERAGE};	
		}
		
		public static IQueryExpression Sum(IQueryExpression expression) {
			return new AggregateExpression() {Expression = expression, Aggregate = AggregateFunction.SUM};	
		}
		
		public static IQueryExpression Asterisk() {
			return new AsteriskLiteral();	
		}
		
		public static IQueryExpression Literal(string literal) {
			return new StringLiteral() {Value = literal};	
		}
		
		public static IQueryExpression Literal(int literal) {
			return new NumberLiteral<int>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(long literal) {
			return new NumberLiteral<long>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(short literal) {
			return new NumberLiteral<short>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(byte literal) {
			return new NumberLiteral<byte>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(float literal) {
			return new NumberLiteral<float>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(double literal) {
			return new NumberLiteral<double>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(decimal literal) {
			return new NumberLiteral<decimal>() {Value = literal};	
		}
		
		public static IQueryExpression Literal(bool literal) {
			return (literal ? new NumberLiteral<int>() {Value = 1} : new NumberLiteral<int>() {Value = 0});	
		}
		
	}
}

