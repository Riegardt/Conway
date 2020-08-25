using NUnit.Framework;

namespace Conway.Domain.Models.Tests
{
	public class GameBoardTests
	{
		[Test]
		public void HasValidSize_ReturnsFalseIfBoardSizeIsBelowTwo()
		{
			var sut = new GameBoard
			{
				BoardSize = 1,
			};

			Assert.IsFalse(sut.HasValidSize);
		}

		[Test]
		public void GenerateNext_ReturnsFalseIfHasValidSizeIsFalse()
		{
			var sut = new GameBoard
			{
				BoardSize = 1,
				GenerationCount = 2
			};

			sut.ResetStartingPattern();
			var result = sut.GenerateNext(false);

			Assert.IsFalse(result);
		}

		[Test]
		public void GenerateNext_ReturnsFalseIfControllerSaysViewWasInterrupted()
		{
			var sut = new GameBoard
			{
				BoardSize = 10
			};

			sut.ResetStartingPattern();
			var result = sut.GenerateNext(true);

			Assert.IsFalse(result);
		}

		[Test]
		public void GenerateNext_ReturnsFalseFinalGenerationHasBeenReached()
		{
			var sut = new GameBoard
			{
				BoardSize = 10,
				GenerationCount = 2
			};

			sut.ResetStartingPattern();
			var firstResult = sut.GenerateNext(false);
			var secondResult = sut.GenerateNext(false);

			Assert.IsTrue(firstResult);
			Assert.IsFalse(secondResult);
		}

		[Test]
		public void ResetStartingPattern_PopulatesCellsAndResetsCurrentGeneration()
		{
			var sut = new GameBoard
			{
				BoardSize = 10
			};

			sut.ResetStartingPattern();

			Assert.AreEqual(1, sut.CurrentGeneration);
			Assert.AreEqual(10, sut.Cells.Grid.Length);
			Assert.AreEqual(10, sut.Cells.Grid[0].Length);
		}
	}
}