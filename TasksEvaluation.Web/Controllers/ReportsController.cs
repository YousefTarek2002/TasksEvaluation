using Microsoft.AspNetCore.Mvc;

namespace TasksEvaluation.Web.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            // يمكن استبدال ViewBag ببيانات من قاعدة البيانات
            ViewBag.Reports = new List<dynamic>
            {
                new { Id = 1, Title = "Financial Report Q1", Category = "Finance", Date = "2024-01-15" },
                new { Id = 2, Title = "Academic Report", Category = "Academic", Date = "2024-01-20" }
            };
            return View();
        }
    }
}
