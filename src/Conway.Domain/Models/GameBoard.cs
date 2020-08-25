namespace Conway.Domain.Models
{
	public class GameBoard
	{
		public Cells Cells { get; set; }

		public int BoardSize { get; set; }

		public int GenerationCount { get; set; }

		public int CurrentGeneration { get; set; }

		public virtual bool HasValidSize => BoardSize > 1;

		public bool GenerateNext(bool hasUserInterrupted)
		{
			if (hasUserInterrupted
				|| CurrentGeneration == GenerationCount
				|| !HasValidSize)
			{
				return false;
			}

			this.Cells.SetNextGeneration();
			++CurrentGeneration;
			return true;
		}

		public virtual void ResetStartingPattern()
		{
			Cells = new RandomCells(BoardSize);
			this.CurrentGeneration = 1;
		}
	}
}
