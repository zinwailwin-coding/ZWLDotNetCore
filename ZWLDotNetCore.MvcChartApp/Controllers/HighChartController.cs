using Microsoft.AspNetCore.Mvc;

namespace ZWLDotNetCore.MvcChartApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
