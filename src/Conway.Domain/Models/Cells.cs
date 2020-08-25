namespace Conway.Domain.Models
{
	public abstract class Cells
	{
		public const byte Byte0 = 0b0;

		public const byte Byte1 = 0b1;

		public bool[][] Grid { get; set; }

		public virtual void SetNextGeneration()
		{
			var tempRowPair = CreateTempRowPair(Grid);
			for (int y = 0; y < Grid.Length; ++y)
			{
				for (int x = 0; x < Grid.Length; ++x)
				{
					Grid[y][x] = tempRowPair[y & 1][x];
					if (y < Grid.Length - 2)
					{
						tempRowPair[y & 1][x] = NextCellStatus(
							Grid[y + 2][x], SumCellBlock(Grid, y + 2, x));
					}
				}
			}
		}

		internal virtual bool[][] CreateTempRowPair(bool[][] grid)
		{
			var pair = new[] { new bool[grid.Length], new bool[grid.Length] };
			for (int x = 0; x < grid.Length; ++x)
			{
				pair[0][x] = NextCellStatus(grid[0][x], SumCellBlock(grid, 0, x));
				pair[1][x] = NextCellStatus(grid[1][x], SumCellBlock(grid, 1, x));
			}

			return pair;
		}

		internal virtual bool NextCellStatus(bool cell, int cellBlockSum)
			=> cellBlockSum == 3 || cellBlockSum == 4 && cell;

		internal virtual byte SumCellBlock(bool[][] grid, int yPos, int xPos)
		{
			byte sum = 0;
			for (int y = yPos - 1; y <= yPos + 1; ++y)
			{
				for (int x = xPos - 1; x <= xPos + 1; ++x)
				{
					sum += SafeGetCellValue(grid, y, x);
				}
			}

			return sum;
		}

		internal byte SafeGetCellValue(bool[][] grid, int yPos, int xPos)
			=> IsValidCell(grid.Length, yPos, xPos) && grid[yPos][xPos] ? Byte1 : Byte0;

		internal bool IsValidCell(int gridSize, int yPos, int xPos)
			=> yPos < gridSize && xPos < gridSize && yPos >= 0 && xPos >= 0;
	}
}
