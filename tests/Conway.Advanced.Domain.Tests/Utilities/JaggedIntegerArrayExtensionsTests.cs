using NUnit.Framework;

namespace Conway.Advanced.Domain.Utilities.Tests
{
	public class JaggedIntegerArrayExtensionsTests
	{
		[Test]
		public void ConvertToJaggedBooleans_Success()
		{
			var jaggerIntegerArray = new[]
			{
				new[] { 1, 1, 1 },
				new[] { 0, 1, 0 },
				new[] { 0, 1, 0 }
			};

			var actualResult = jaggerIntegerArray.ConvertToJaggedBooleans();

			var expectedResult = new[]
			{
				new[] { true, true, true },
				new[] { false, true, false },
				new[] { false, true, false }
			};

			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}