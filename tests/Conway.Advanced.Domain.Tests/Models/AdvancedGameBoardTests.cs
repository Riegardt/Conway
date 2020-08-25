using NUnit.Framework;
using Conway.Advanced.Domain.Enums;
using Conway.Advanced.Domain.Models;
using System;
using System.Linq;

namespace Conway.Advanced.Domain.Tests
{
	public class AdvancedGameBoardTests
	{
		[Test]
		public void HasValidSize_ReturnsFalseIfBoardSizeIsOne()
		{
			var sut = new AdvancedGameBoard
			{
				BoardSize = 1,
			};

			Assert.IsFalse(sut.HasValidSize);
		}

		[Test]
		public void HasValidSize_ReturnsFalseIfBoardSizeIsBelowLowestStartingPatternEnum()
		{
			var lowestStartingPatternEnum = (int)Enum
				.GetValues(typeof(StartingPattern))
				.Cast<StartingPattern>()
				.Min();

			var sut = new AdvancedGameBoard
			{
				BoardSize = lowestStartingPatternEnum - 1,
			};

			Assert.IsFalse(sut.HasValidSize);
		}

		[Test]
		public void ResetStartingPattern_FixesBoardSizeAndPopulatesCellsAndResetsCurrentGeneration()
		{
			var lowestStartingPatternEnum = (int)Enum
				.GetValues(typeof(StartingPattern))
				.Cast<StartingPattern>()
				.Min();

			for (int size = lowestStartingPatternEnum; size < 0; ++size)
			{
				var sut = new AdvancedGameBoard()
				{
					BoardSize = size
				};

				sut.ResetStartingPattern();

				Assert.AreEqual(1, sut.CurrentGeneration);
				Assert.GreaterOrEqual(sut.Cells.Grid.Length, 1);
				Assert.GreaterOrEqual(sut.Cells.Grid[0].Length, 1);
			}
		}
	}
}