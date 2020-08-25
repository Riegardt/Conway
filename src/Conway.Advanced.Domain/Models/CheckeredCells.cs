using Conway.Domain.Models;

namespace Conway.Advanced.Domain.Models
{
	public class CheckeredCells : Cells
	{
		public CheckeredCells(int size)
		{
			Grid = SetCheckeredCells(size);
		}

		private bool[][] SetCheckeredCells(int size)
		{
			bool firstCell = false;
			var cells = new bool[size][];
			for (int y = 0; y < size; ++y)
			{
				firstCell = !firstCell;
				bool nextCell = !firstCell;
				cells[y] = new bool[size];
				for (int x = 0; x < size; ++x)
				{
					nextCell = !nextCell;
					cells[y][x] = nextCell;
				}
			}

			return cells;
		}
	}
}