using Conway.Advanced.Domain.Enums;
using Conway.Domain.Models;

namespace Conway.Advanced.Domain.Models
{
	public class AdvancedGameBoard : GameBoard
	{
		public AdvancedGameBoard() : base() { }

		public override bool HasValidSize
			=> BoardSize >= (int)StartingPattern.TestText && BoardSize != 1;

		public override void ResetStartingPattern()
		{
			switch ((StartingPattern)BoardSize)
			{
				case StartingPattern.Spaceships:
					BoardSize = 29;
					Cells = new SpaceshipCells();
					break;

				case StartingPattern.Oscillators:
					BoardSize = 29;
					Cells = new OscillatorCells();
					break;

				case StartingPattern.Checkered:
					BoardSize = 29;
					Cells = new CheckeredCells(BoardSize);
					break;

				case StartingPattern.TestText:
					BoardSize = 15;
					Cells = new TestTextCells();
					break;

				default:
					base.ResetStartingPattern();
					break;
			}

			this.CurrentGeneration = 1;
		}
	}
}
