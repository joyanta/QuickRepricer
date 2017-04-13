using Microsoft.AspNetCore.Mvc;
using QuickRepricer.Core.ViewModels;
using QuickRepricer.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace QuickRepricer.Controllers.Web
{
    [RequireHttps]
    public class AppController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;

        public AppController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public IActionResult About()
        {
           

            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();

        }


        public IActionResult RefundPolicy()
        {
            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //_mailservice.SendMail(_config["MailSettings:ToSupportAddress"], vm.Email, "From Repricerbot", vm.Message);

                ModelState.Clear();

                ViewData["Message"] = "Message sent";
                return RedirectToAction("Index");
            }

            return BadRequest(ModelState);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
