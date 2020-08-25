using FakeItEasy;
using NUnit.Framework;
using Conway.Application.Interfaces;
using Conway.Domain.Models;

namespace Conway.Application.Tests
{
	public class ConwayControllerTests
	{
		private IConwayView fakeView;
		private ConwayController controller;

		[SetUp]
		public void Setup()
		{
			fakeView = A.Fake<IConwayView>();
			controller = new ConwayController(fakeView);
		}

		[Test]
		public void Start_Success()
		{
			A.CallTo(() => fakeView.GetBoardSize()).ReturnsNextFromSequence(10, 20, 0);
			A.CallTo(() => fakeView.GetGenerationCount()).ReturnsNextFromSequence(111, 222);

			controller.Start();

			A.CallTo(() => fakeView.PrepareWindow(A<int>.Ignored)).MustHaveHappenedTwiceExactly();
			A.CallTo(() => fakeView.ShowResult(A<bool[][]>.Ignored)).MustHaveHappenedANumberOfTimesMatching(x => x == 333);
			A.CallTo(() => fakeView.Wait(A<int>.Ignored)).MustHaveHappenedANumberOfTimesMatching(x => x == 333);
			A.CallTo(() => fakeView.Pause()).MustHaveHappenedTwiceExactly();
			A.CallTo(() => fakeView.Close()).MustHaveHappenedOnceExactly();
		}


		[Test]
		public void Start_SizeOfOneOrNegativeAbortsGridDisplay()
		{
			A.CallTo(() => fakeView.GetBoardSize()).ReturnsNextFromSequence(1, -5, 0);
			A.CallTo(() => fakeView.GetGenerationCount()).ReturnsNextFromSequence(111, 555);

			controller.Start();

			A.CallTo(() => fakeView.PrepareWindow(A<int>.Ignored)).MustNotHaveHappened();
			A.CallTo(() => fakeView.ShowResult(A<bool[][]>.Ignored)).MustNotHaveHappened();
			A.CallTo(() => fakeView.Wait(A<int>.Ignored)).MustNotHaveHappened();
			A.CallTo(() => fakeView.Pause()).MustNotHaveHappened();
		}

		[Test]
		public void BuildNewBoard_SetsBoardSizeAndGenerationCount()
		{
			A.CallTo(() => fakeView.GetBoardSize()).Returns(10);
			A.CallTo(() => fakeView.GetGenerationCount()).Returns(111);

			var board = new GameBoard();
			var result = controller.BuildNewBoard(board);

			Assert.AreEqual(10, board.BoardSize);
			Assert.AreEqual(111, board.GenerationCount);
			Assert.IsTrue(result);
		}

		[Test]
		public void BuildNewBoard_ReturnsFalseOnZeroBoardSize()
		{
			A.CallTo(() => fakeView.GetBoardSize()).Returns(0);

			var board = new GameBoard();
			var result = controller.BuildNewBoard(board);

			Assert.IsFalse(result);
		}
	}
}