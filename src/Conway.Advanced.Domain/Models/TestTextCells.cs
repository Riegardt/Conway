using Conway.Advanced.Domain.Utilities;
using Conway.Domain.Models;

namespace Conway.Advanced.Domain.Models
{
	public class TestTextCells : Cells
	{
		public TestTextCells()
		{
			Grid = SetTestTextCells();
		}

		private bool[][] SetTestTextCells()
			=> new[]
			{
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1 },
				new[] { 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
				new[] { 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
				new[] { 0, 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
				new[] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
				new[] { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
				new[] { 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			}.ConvertToJaggedBooleans();
	}
}