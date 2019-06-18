using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ExpressionUtils.Test
{
	public class UtilsTests
	{
		private const string Wow = "Wow";
		private const string Hello = "Hello";
		private const string Big = "Big";
		private const string World = "World";

		private readonly IEnumerable<Foo> _foos;

		public UtilsTests()
		{
			_foos = new List<Foo>
			{
				new Foo
				{
					Bar = Wow,
					Baz = 1
				},
				new Foo
				{
					Bar = Hello,
					Baz = 2
				},
				new Foo
				{
					Bar = Big,
					Baz = 2
				},
				new Foo
				{
					Bar = World,
					Baz = 2
				}
			};
		}

		[Fact]
		public void EnsureThatAndCombinationWorksForParamsMethodOverload()
		{
			// Arrange
			Expression<Func<Foo, bool>> helloExpr = f => f.Bar.Equals(Hello, StringComparison.OrdinalIgnoreCase);
			Expression<Func<Foo, bool>> worldExpr = f => f.Baz == 2;

			var resultExpr = Utils.And(helloExpr, worldExpr);

			// Act
			var resultItems = _foos.AsQueryable().Where(resultExpr);

			// Assert
			Assert.Equal(1, resultItems.Count());
		}

		[Fact]
		public void EnsureThatAndCombinationWorksForEnumerableMethodOverload()
		{
			// Arrange
			Expression<Func<Foo, bool>> helloExpr = f => f.Bar.Equals(Hello, StringComparison.OrdinalIgnoreCase);
			Expression<Func<Foo, bool>> worldExpr = f => f.Baz == 2;

			IEnumerable<Expression<Func<Foo, bool>>> expressionsEnumerable = new List<Expression<Func<Foo, bool>>>
			{
				helloExpr,
				worldExpr
			};

			var resultExpr = Utils.And(expressionsEnumerable);

			// Act
			var resultItems = _foos.AsQueryable().Where(resultExpr);

			// Assert
			Assert.Equal(1, resultItems.Count());
		}

		[Fact]
		public void EnsureThatOrCombinationWorksForParamsMethodOverload()
		{
			// Arrange
			Expression<Func<Foo, bool>> helloExpr = f => f.Bar.Equals(Hello, StringComparison.OrdinalIgnoreCase);
			Expression<Func<Foo, bool>> worldExpr = f => f.Baz == 2;

			var resultExpr = Utils.Or(helloExpr, worldExpr);

			// Act
			var resultItems = _foos.AsQueryable().Where(resultExpr);

			// Assert
			Assert.Equal(3, resultItems.Count());
		}

		[Fact]
		public void EnsureThatOrCombinationWorksForEnumerableMethodOverload()
		{
			// Arrange
			Expression<Func<Foo, bool>> helloExpr = f => f.Bar.Equals(Hello, StringComparison.OrdinalIgnoreCase);
			Expression<Func<Foo, bool>> worldExpr = f => f.Baz == 2;

			IEnumerable<Expression<Func<Foo, bool>>> expressionsEnumerable = new List<Expression<Func<Foo, bool>>>
			{
				helloExpr,
				worldExpr
			};

			var resultExpr = Utils.Or(expressionsEnumerable);

			// Act
			var resultItems = _foos.AsQueryable().Where(resultExpr);

			// Assert
			Assert.Equal(3, resultItems.Count());
		}
	}
}