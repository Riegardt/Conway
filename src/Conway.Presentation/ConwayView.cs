using Conway.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Conway.Presentation
{
	public class ConwayView : IConwayView
	{
		private readonly int originalWindowHeight;
		private readonly int originalWindowWidth;

		public ConwayView()
		{
			originalWindowHeight = Console.WindowHeight;
			originalWindowWidth = Console.WindowWidth;
		}

		public bool HasUserInterrupted => Console.KeyAvailable;

		public int GetBoardSize()
		{
			Console.Clear();
			Console.SetWindowSize(originalWindowWidth, originalWindowHeight);
			var size = ReadNumber("Please provide a board size (blank or zero to exit): ");
			while (size > Console.LargestWindowHeight)
			{
				Console.WriteLine("Size cannot exceed maximum Console Window height!");
				size = ReadNumber($"Please provide a board size below {Console.LargestWindowHeight + 1}: ");
			}

			return size;
		}

		public int GetGenerationCount()
			=> ReadNumber("\nPlease specify the generation count (blank or zero = until keypress): ");

		public void PrepareWindow(int gridSize)
		{
			Console.Clear();
			Console.SetWindowSize(gridSize * 2, gridSize + 1);
		}

		public void ShowResult(bool[][] grid)
		{
			for (int y = 0; y < grid.Length; ++y)
			{
				Console.SetCursorPosition(0, y);
				Console.Write(StreamRow(grid, y).ToArray());
			}
		}

		public void Close()
			=> Console.WriteLine("Closing display");

		public void Pause()
			=> Console.ReadKey();

		public void Wait(int milliseconds)
			=> Thread.Sleep(milliseconds);

		private int ReadNumber(string prompt)
		{
			int number;
			Console.Write(prompt);
			var input = Console.ReadLine();
			while (!int.TryParse(input, out number))
			{
				if (input == string.Empty)
				{
					return 0;
				}

				Console.Write("Not a number, please try again: ");
				input = Console.ReadLine();
			}

			return number;
		}

		private IEnumerable<char> StreamRow(bool[][] grid, int y)
		{
			for (int x = 0; x < grid[y].Length; ++x)
			{
				if (grid[y][x])
				{
					yield return '█';
					yield return '█';
				}
				else
				{
					yield return ' ';
					yield return ' ';
				}

			}
		}
	}
}
