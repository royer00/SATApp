using System;
using System.Net.Mail;
using System.Web.Mvc;
using SATApp.UI.MVC.Models;
namespace SATApp.UI.MVC.Controllers
{

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Contact()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"{cvm.Name} has contacted you from {cvm.Email} with a subject of {cvm.Subject}.<br/>" +
               $"The message is as follows:<br/> {cvm.Message}";

            MailMessage mail = new MailMessage("admin@kevinroyer.net", "royerkevin@outlook.com", $"{System.DateTime.Now.Date}-{ cvm.Subject}", message);
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;
            mail.ReplyToList.Add(cvm.Email);
            SmtpClient client = new SmtpClient("mail.kevinroyer.net");
            client.Credentials = new System.Net.NetworkCredential("admin@kevinroyer.net", "P@ssw0rd");
            client.Port = 8889;
            try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Sorry, there was an error.";

                if (User.IsInRole("Admin"))
                {
                    ViewBag.Error = $"Error: {e.StackTrace}";
                }

                return View(cvm);
            }
            return View("EmailConfirmation", cvm);


        }

    }
}

