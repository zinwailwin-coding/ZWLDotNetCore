﻿using Microsoft.AspNetCore.Mvc;
using ZWLDotNetCore.MvcChartApp.Models;

namespace ZWLDotNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult SPLineChart()
        {
            SPLineChartModel model= new SPLineChartModel();
            model.Series1 = new List<int>() { 31, 40, 28, 51, 42, 109, 100 };
            model.Series2 = new List<int>() { 11, 32, 45, 32, 34, 52, 41 };
            model.Categories= new List<string>() { "2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z" };
            return View(model);
        }          
       
    }
}
