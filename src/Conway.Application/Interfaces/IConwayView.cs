namespace Conway.Application.Interfaces
{
	public interface IConwayView
	{
		public bool HasUserInterrupted { get; }

		public int GetBoardSize();

		public int GetGenerationCount();

		public void PrepareWindow(int gridSize);

		public void ShowResult(bool[][] grid);

		public void Wait(int milliseconds);

		public void Pause();

		public void Close();
	}
}
