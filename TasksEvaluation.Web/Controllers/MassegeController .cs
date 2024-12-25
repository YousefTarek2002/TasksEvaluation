using Microsoft.AspNetCore.Mvc;

namespace TasksEvaluation.Web.Controllers
{
    public class MassegeController : Controller
    {
        public IActionResult Inbox()
        {
            ViewBag.Massege = new List<dynamic>
            {
                new { Id = 1, Subject = "System Update", Content = "A new system update has been deployed.", Time = "2 hours ago" },
                new { Id = 2, Subject = "Meeting Reminder", Content = "Don't forget the meeting tomorrow.", Time = "1 day ago" }
            };
            return View();
        }
    }
}
