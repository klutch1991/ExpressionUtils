using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExpressionUtils
{
	internal static class ExpressionExtensions
	{
		internal static Expression BetaReduce(
			this LambdaExpression expr, 
			params Expression[] args)
		{
			Debug.Assert(expr.Parameters.Count == args.Length);
			var mapping = new Dictionary<ParameterExpression, Expression>();

			for (var i = 0; i < expr.Parameters.Count; i++)
			{
				mapping.Add(expr.Parameters[i], args[i]);
			}

			return new Reducer(mapping).Visit(expr.Body);
		}
	}
}