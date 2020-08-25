using Conway.Application.Interfaces;
using Conway.Domain.Models;

namespace Conway.Application
{
	public class ConwayController
	{
		protected readonly IConwayView view;

		public ConwayController(IConwayView view)
		{
			this.view = view;
			Board = new GameBoard();
		}

		public virtual GameBoard Board { get; set; }

		public void Start()
		{
			while (BuildNewBoard(Board))
			{
				if (Board.HasValidSize)
				{
					Board.ResetStartingPattern();
					view.PrepareWindow(Board.BoardSize);
					do
					{
						view.ShowResult(Board.Cells.Grid);
						view.Wait(100);
					}
					while (Board.GenerateNext(view.HasUserInterrupted));
					view.Pause();
				}
			}

			view.Close();
		}

		public bool BuildNewBoard(GameBoard board)
		{
			var size = view.GetBoardSize();
			if (size == 0)
			{
				return false;
			}

			board.BoardSize = size;
			board.GenerationCount = view.GetGenerationCount();
			return true;
		}
	}
}
