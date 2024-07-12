using Microsoft.AspNetCore.Mvc;

namespace ZWLDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        private readonly ILogger<CanvasJsController> _logger;

		public CanvasJsController(ILogger<CanvasJsController> logger)
		{
			_logger = logger;
		}

		public IActionResult LineChart()
        {
            _logger.LogInformation("Line Chart...");
            return View();
        }
    }
}
