using Microsoft.AspNetCore.Mvc;

namespace TasksEvaluation.Web.Controllers
{
    public class NavigationController : Controller
    {
        public IActionResult List()
        {
            ViewBag.Navigation = new List<dynamic>
            {
                new { Id = 1, Message = "Your assignment has been graded.", Time = "5 hours ago", Type = "info" },
                new { Id = 2, Message = "New system notification available.", Time = "1 day ago", Type = "warning" }
            };
            return View();
        }
    }
}
