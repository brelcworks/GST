using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GST.Models;
using System.Web.Security;
using MongoDB.Driver;

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
            try
            {
                var client = new MongoClient(System.Configuration.ConfigurationManager.AppSettings["mongo"]);
                var database = client.GetDatabase("appharbor_gxm5g8zh");
                var collection = database.GetCollection<USER>("USER");
                var builder = Builders<USER>.Filter;
                var filter = builder.Eq("uid", L.uid) & builder.Eq("pass", L.pass);
                var result = collection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(result.fname, false);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.ERR = "Wrong User ID or Password! Please Try Again";
                }
            }
            catch (Exception e)
            {
                ViewBag.ERR = e.ToString();
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

        [HttpPost]
        public ActionResult Register(USER um)
        {
            var client = new MongoClient(System.Configuration.ConfigurationManager.AppSettings["mongo"]);
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<USER>("USER");
            collection.InsertOne(um);
            return RedirectToAction("login");
        }
    }
}