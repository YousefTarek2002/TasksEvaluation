using Microsoft.AspNetCore.Mvc;

namespace TasksEvaluation.Web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            // يمكن استخدام نموذج (Model) لتمرير البيانات بدل ViewBag
            ViewBag.UserSettings = new
            {
                Username = "JohnDoe",
                Email = "johndoe@example.com",
                EmailNotifications = true,
                SMSNotifications = false
            };
            return View();
        }
    }
}
