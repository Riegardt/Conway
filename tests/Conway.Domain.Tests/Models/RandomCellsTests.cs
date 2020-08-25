using FakeItEasy;
using NUnit.Framework;
using Conway.Domain.Interfaces;
using Conway.Domain.Utilities;

namespace Conway.Domain.Models.Tests
{
	public class RandomCellsTests
	{
		[Test]
		public void Constructor_SetsGridPropertyToSizeBySizeArray()
		{
			var sut = new RandomCells(10);

			Assert.AreEqual(10, sut.Grid.Length);
			Assert.AreEqual(10, sut.Grid[0].Length);
		}

		[Test]
		public void SetRandomCells_ReturnsSizeBySizeArray()
		{
			var sut = new RandomCells();
			var randomiser = new FastRandom();

			var result = sut.SetRandomCells(randomiser, 10);

			Assert.AreEqual(10, result.Length);
			Assert.AreEqual(10, result[0].Length);
		}

		[Test]
		public void SetRandomCells_CallsNextBooleanSizeBySizeArray()
		{
			var sut = new RandomCells();
			var randomiser = A.Fake<IFastRandom>();

			var result = sut.SetRandomCells(randomiser, 10);

			A.CallTo(() => randomiser.NextBoolean())
				.MustHaveHappenedANumberOfTimesMatching(x => x == 100);
		}
	}
}