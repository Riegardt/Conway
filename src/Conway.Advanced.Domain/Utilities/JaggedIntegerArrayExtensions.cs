namespace Conway.Advanced.Domain.Utilities
{
	public static class JaggedIntegerArrayExtensions
	{
		public static bool[][] ConvertToJaggedBooleans(this int[][] input)
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

	}
}
