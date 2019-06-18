using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionUtils
{
	internal class Reducer : ExpressionVisitor
	{
		private readonly IDictionary<ParameterExpression, Expression> _args;

		internal Reducer(
			IDictionary<ParameterExpression, Expression> args) 
				=> _args = args;

		protected override Expression VisitParameter(
			ParameterExpression node) 
				=> _args.TryGetValue(node, out var result) 
					? result 
					: base.VisitParameter(node);
	}
}
