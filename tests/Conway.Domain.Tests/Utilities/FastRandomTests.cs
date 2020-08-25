using FakeItEasy;
using NUnit.Framework;
using System;

namespace Conway.Domain.Utilities.Tests
{
	public class FastRandomTests
	{
		[Test]
		public void NextBoolean_SetsBitsOnlyOnceEveryThirtyOneCalls()
		{
			var sut = A.Fake<FastRandom>();
			A.CallTo(sut).CallsBaseMethod();

			for (int i = 0; i < 62; ++i)
			{
				sut.NextBoolean();
			}

			A.CallTo(() => sut.CallNext()).MustHaveHappenedTwiceExactly();
		}

		[Test]
		public void NextBoolean_ProducesARandomBoolean()
		{
			var sut = new FastRandom();

			var trueCount = 0;
			for (int i = 0; i < 1000000; ++i)
			{
				trueCount += sut.NextBoolean() ? 1 : 0;
			}

			Console.WriteLine($"{trueCount} / 1000000 results were true");
			Assert.IsTrue(trueCount < 1000000);
		}
	}
}