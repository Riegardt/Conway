using FakeItEasy;
using NUnit.Framework;

namespace Conway.Domain.Models.Tests
{
	public class CellsTests
	{
		private Cells sut;

		[SetUp]
		public void Setup()
		{
			sut = A.Fake<Cells>();
			A.CallTo(sut).CallsBaseMethod();
		}

		[Test]
		public void SetNextGeneration_CallsCreateTempRowPair()
		{
			var grid = ConvertJaggedIntegersToBooleans(new[]
			{
				new[] { 0, 0, 0, 0, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 1, 0, 1, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 0, 0, 0, 0 }
			});

			sut.Grid = grid;
			sut.SetNextGeneration();

			A.CallTo(() => sut.CreateTempRowPair(grid)).MustHaveHappenedOnceExactly();
		}

		/// <summary>
		/// Number of direct calls to NextCellStatus from this method should = (size x size-2)
		///		The first (size x 2) times are indirectly through CreateTempRowPair.
		/// </summary>
		[Test]
		public void SetNextGeneration_DirectlyCallsNextCellStatusSizeBySizeMinusTwoTimes()
		{
			var grid = ConvertJaggedIntegersToBooleans(new[]
			{
				new[] { 0, 0, 0, 0, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 1, 0, 1, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 0, 0, 0, 0 }
			});

			sut.Grid = grid;
			A.CallTo(() => sut.CreateTempRowPair(grid))
				.Returns(ConvertJaggedIntegersToBooleans(new[]
				{
					new[] { 0, 0, 0, 0, 0 },
					new[] { 0, 0, 0, 0, 0 }
				}));

			sut.SetNextGeneration();

			A.CallTo(() => sut.NextCellStatus(A<bool>.Ignored, A<int>.Ignored))
				.MustHaveHappenedANumberOfTimesMatching(x => x == 15);
		}

		[Test]
		public void CreateTempRowPair_CallsNextCellStatusForEachCellInFirstTwoRows()
		{
			var grid = ConvertJaggedIntegersToBooleans(new[]
			{
				new[] { 0, 0, 0, 0, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 1, 0, 1, 0 },
				new[] { 0, 1, 1, 1, 0 },
				new[] { 0, 0, 0, 0, 0 }
			});

			var result = sut.CreateTempRowPair(grid);

			A.CallTo(() => sut.NextCellStatus(false, 1)).MustHaveHappenedTwiceExactly();
			A.CallTo(() => sut.NextCellStatus(false, 2)).MustHaveHappenedANumberOfTimesMatching(x => x == 4);
			A.CallTo(() => sut.NextCellStatus(false, 3)).MustHaveHappenedOnceExactly();
			A.CallTo(() => sut.NextCellStatus(true, 3)).MustHaveHappenedTwiceExactly();
			A.CallTo(() => sut.NextCellStatus(true, 5)).MustHaveHappenedOnceExactly();
		}

		/// <summary>
		/// Rule 1: Any live cell with fewer than two live 
		///		neighbours dies, as if by underpopulation.
		/// </summary>
		[TestCase(true, 0)]
		[TestCase(true, 1)]
		public void NextCellStatus_SuccessfullyAppliesConwayRuleOne(
			bool cellValue, int neighbourCount)
		{
			var grid = SizeThreeGrid(cellValue, neighbourCount);

			var result = sut.NextCellStatus(grid[1][1], sut.SumCellBlock(grid, 1, 1));

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Rule 2: Any live cell with two or three live 
		///		neighbours lives on to the next generation.
		/// </summary>
		[TestCase(true, 2)]
		[TestCase(true, 3)]
		public void NextCellStatus_SuccessfullyAppliesConwayRuleTwo(
			bool cellValue, int neighbourCount)
		{
			var grid = SizeThreeGrid(cellValue, neighbourCount);

			var result = sut.NextCellStatus(grid[1][1], sut.SumCellBlock(grid, 1, 1));

			Assert.IsTrue(result);
		}

		/// <summary>
		/// Rule 3: Any live cell with more than three live 
		///		neighbours dies, as if by overpopulation.
		/// </summary>
		[TestCase(true, 4)]
		[TestCase(true, 5)]
		[TestCase(true, 6)]
		[TestCase(true, 7)]
		[TestCase(true, 8)]
		public void NextCellStatus_SuccessfullyAppliesConwayRuleThree(
			bool cellValue, int neighbourCount)
		{
			var grid = SizeThreeGrid(cellValue, neighbourCount);

			var result = sut.NextCellStatus(grid[1][1], sut.SumCellBlock(grid, 1, 1));

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Rule 4: Any dead cell with exactly three live 
		///		neighbours becomes a live cell, as if by reproduction.
		/// </summary>
		[TestCase(false, 3)]
		public void NextCellStatus_SuccessfullyAppliesConwayRuleFour(
			bool cellValue, int neighbourCount)
		{
			var grid = SizeThreeGrid(cellValue, neighbourCount);

			var result = sut.NextCellStatus(grid[1][1], sut.SumCellBlock(grid, 1, 1));

			Assert.IsTrue(result);
		}

		[Test]
		public void SumCellBlock_AddsSurroundingCellsToCellValue()
		{
			bool[][] grid = ConvertJaggedIntegersToBooleans(new[]
			{
				new[] { 1, 0, 1 },
				new[] { 0, 1, 0 },
				new[] { 1, 0, 1 }
			});

			Assert.AreEqual(2, sut.SumCellBlock(grid, 0, 0));
			Assert.AreEqual(3, sut.SumCellBlock(grid, 0, 1));
			Assert.AreEqual(2, sut.SumCellBlock(grid, 0, 2));

			Assert.AreEqual(3, sut.SumCellBlock(grid, 1, 0));
			Assert.AreEqual(5, sut.SumCellBlock(grid, 1, 1));
			Assert.AreEqual(3, sut.SumCellBlock(grid, 1, 2));

			Assert.AreEqual(2, sut.SumCellBlock(grid, 2, 0));
			Assert.AreEqual(3, sut.SumCellBlock(grid, 2, 1));
			Assert.AreEqual(2, sut.SumCellBlock(grid, 2, 2));
		}

		[Test]
		public void SafeGetCellValue_ReturnsCellValueIfValidElseZero()
		{
			bool[][] grid = ConvertJaggedIntegersToBooleans(new[]
			{
				new[] { 1, 1 },
				new[] { 1, 1 }
			});

			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, -1, -1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, -1, 0));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, -1, 1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, -1, 2));

			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 0, -1));
			Assert.AreEqual(Cells.Byte1, sut.SafeGetCellValue(grid, 0, 0));
			Assert.AreEqual(Cells.Byte1, sut.SafeGetCellValue(grid, 0, 1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 0, 2));

			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 1, -1));
			Assert.AreEqual(Cells.Byte1, sut.SafeGetCellValue(grid, 1, 0));
			Assert.AreEqual(Cells.Byte1, sut.SafeGetCellValue(grid, 1, 1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 1, 2));

			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 2, -1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 2, 0));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 2, 1));
			Assert.AreEqual(Cells.Byte0, sut.SafeGetCellValue(grid, 2, 2));
		}

		[Test]
		public void IsValidCell_ReturnsTrueWhenInsideBounds()
		{
			Assert.IsTrue(sut.IsValidCell(1, 0, 0));
		}

		[Test]
		public void IsValidCell_ReturnsFalseWhenOutsideBounds()
		{
			Assert.IsFalse(sut.IsValidCell(1, -1, -1));
			Assert.IsFalse(sut.IsValidCell(1, -1, 0));
			Assert.IsFalse(sut.IsValidCell(1, -1, 1));

			Assert.IsFalse(sut.IsValidCell(1, 0, -1));
			Assert.IsFalse(sut.IsValidCell(1, 0, 2));

			Assert.IsFalse(sut.IsValidCell(1, 1, -1));
			Assert.IsFalse(sut.IsValidCell(1, 1, 0));
			Assert.IsFalse(sut.IsValidCell(1, 1, 1));
		}

		private bool[][] ConvertJaggedIntegersToBooleans(int[][] input)
		{
			var output = new bool[input.Length][];
			for (int y = 0; y < input.Length; ++y)
			{
				output[y] = new bool[input[y].Length];
				for (int x = 0; x < input[y].Length; ++x)
				{
					output[y][x] = input[y][x] == 1;
				}
			}

			return output;
		}

		private bool[][] SizeThreeGrid(bool centerCellValue, int neighbourCount)
		{
			var grid = new bool[3][];
			for (int y = 0; y < 3; ++y)
			{
				grid[y] = new bool[3];
				for (int x = 0; x < 3; ++x)
				{
					grid[y][x] = (x == 1 && y == 1)
						? centerCellValue : (--neighbourCount >= 0);
				}
			}

			return grid;
		}
	}
}