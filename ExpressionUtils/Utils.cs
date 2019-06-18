using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionUtils
{
	public class Utils
	{
		public static Expression<Func<T, bool>> And<T>(params Expression<Func<T, bool>>[] items)
			=> And(items.AsEnumerable());

		public static Expression<Func<T, bool>> And<T>(IEnumerable<Expression<Func<T, bool>>> items)
		{
			var p = Expression.Parameter(typeof(T));
			var body = items.Select(item => item.BetaReduce(p)).Aggregate(Expression.AndAlso);
			return Expression.Lambda<Func<T, bool>>(body, p);
		}

		public static Expression<Func<T, bool>> Or<T>(params Expression<Func<T, bool>>[] items)
			=> Or(items.AsEnumerable());

		public static Expression<Func<T, bool>> Or<T>(IEnumerable<Expression<Func<T, bool>>> items)
		{
			var p = Expression.Parameter(typeof(T));
			var body = items.Select(item => item.BetaReduce(p)).Aggregate(Expression.OrElse);
			return Expression.Lambda<Func<T, bool>>(body, p);
		}
	}
}