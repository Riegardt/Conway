using Conway.Advanced.Domain.Models;
using Conway.Application;
using Conway.Application.Interfaces;

namespace Conway.Advanced.Application
{
	public class AdvancedConwayController : ConwayController
	{
		public AdvancedConwayController(IConwayView view) : base(view)
		{
			Board = new AdvancedGameBoard();
		}
	}
}
