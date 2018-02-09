using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GST.Models;
using System.Web.Security;

namespace GST.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(USER1 L, string ReturnUrl = "")
        {
            using (DBCTX DC1 = new DBCTX())

            {
                var user = DC1.USER1.Where(a => a.uid.Equals(L.uid) && a.pass.Equals(L.pass)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.fname, false);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Home");
        }
    }
}