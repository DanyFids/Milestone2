using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Interface;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("* Captcha is not valid")]
        public async Task<ActionResult> Contact(Contact model)
        {   
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} {1} ({2})</p><p>Message:</p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("danyfids@gmail.com"));
                message.From = new MailAddress("danyfids@gmail.com");
                message.Subject = string.Format("Adventure Bikes! Message from: {0} {1}", model.FirstName, model.LastName);
                message.Body = string.Format(body, model.FirstName, model.LastName, model.Email, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

    }
}