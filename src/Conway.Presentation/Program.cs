using Conway.Advanced.Application;
using Conway.Application;

namespace Conway.Presentation
{
	public class Program
	{
		public static bool UseAdvanced = true;

		public static void Main()
		{
			var view = new ConwayView();
			ConwayController controller;
			controller = UseAdvanced
				? new AdvancedConwayController(view)
				: new ConwayController(view);

			controller.Start();
		}
	}
}
