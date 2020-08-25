using Conway.Domain.Interfaces;
using Conway.Domain.Utilities;

namespace Conway.Domain.Models
{
	public class RandomCells : Cells
	{
		public RandomCells(int size)
		{
			Grid = SetRandomCells(new FastRandom(), size);
		}

		internal RandomCells() { }

		internal bool[][] SetRandomCells(IFastRandom randomiser, int size)
		{
			var cells = new bool[size][];
			for (int y = 0; y < size; ++y)
			{
				cells[y] = new bool[size];
				for (int x = 0; x < size; ++x)
				{
					cells[y][x] = randomiser.NextBoolean();
				}
			}

			return cells;
		}
	}
}