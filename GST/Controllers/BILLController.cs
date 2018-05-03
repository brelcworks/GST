using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GST.Models;
using System.Web.Security;
using System.Data.Entity;
using MongoDB.Driver;
using System.Configuration;
using System.Web.Configuration;

namespace GST.Controllers
{
    public class BILLController : Controller
    {
        DBCTX dc = new DBCTX();
        MongoClient client = new MongoClient(System.Configuration.ConfigurationManager.AppSettings["mongo"]);
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(PUCH e)
        {
            using (dc)
            {
                dc.PUCH.Add(e);
                dc.SaveChanges();
            }
            return RedirectToAction("Create");
        }

        public JsonResult BILL(int page, int rows, string sidx, string sord, string BNO)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<BILLM>("BILL");
            var dbResult = collection.AsQueryable<BILLM>().Where(b => b.BILL_NO.Equals(BNO)).Select(
                a => new
                {
                    a.BILLID,
                    a.BID,
                    a.HSN_NO,
                    a.PARTI,
                    a.MRP,
                    a.SPRICE,
                    a.SQTY,
                    a.UNIT,
                    a.TVAL,
                    a.STOT
                });

            int totalRecords = dbResult.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                dbResult = dbResult.OrderByDescending(s => s.PARTI);
                dbResult = dbResult.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                dbResult = dbResult.OrderBy(s => s.PARTI);
                dbResult = dbResult.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var JsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = dbResult
            };
            return Json(JsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BILL1(int page, int rows, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<PUCHM>("BILL1");
            var dbResult = collection.AsQueryable<PUCHM>().Select(
                a => new
                {
                    a.BID,
                    a.BNO,
                    a.BDATE,
                    a.CUST,
                    a.BAMT
                });

            int totalRecords = dbResult.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                dbResult = dbResult.OrderByDescending(s => s.BID);
                dbResult = dbResult.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                dbResult = dbResult.OrderBy(s => s.BID);
                dbResult = dbResult.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var JsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = dbResult
            };
            return Json(JsonData, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(BILLM bill)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var database = client.GetDatabase("appharbor_gxm5g8zh");
                    var collection = database.GetCollection<BILLM>("BILL");
                    collection.InsertOne(bill);
                    message = "Successfully Saved!";
                }
                catch (Exception ex) { message = ex.ToString(); }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(bill);
            }
        }

        public int DPL(string aData)
        {
            int msg;
            List<STCM> STLIST = new List<STCM>();
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<STCM>("STC");
            var builder = Builders<STCM>.Filter;
            var filter = builder.Eq("PARTI", aData);
            STLIST = collection.Find(filter).Sort(Builders<STCM>.Sort.Descending("_id")).ToList();
            msg = STLIST.Count();
            return msg;
        }

        public JsonResult CON1()
        {
            try
            {
                var database = client.GetDatabase("appharbor_gxm5g8zh");
                var collection = database.GetCollection<BILLM>("BILL");
                var max = collection.Find(f => true).Count();
                return new JsonResult { Data = max, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.ToString(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ADSTC(STCM bill)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var database = client.GetDatabase("appharbor_gxm5g8zh");
                    var collection = database.GetCollection<STCM>("STC");
                    collection.InsertOne(bill);
                    message = "Successfully Saved!";
                }
                catch (Exception ex) { message = ex.ToString(); }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(bill);
            }
        }

        [Authorize]
        public JsonResult GetParti(string term)
        {
            List<string> itms;
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<STCM>("STC");
            itms = collection.AsQueryable<STCM>().Where(e => e.PARTI.StartsWith(term)).Select(e => e.PARTI).ToList();
            return Json(itms, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult gdata2(string aData)
        {
            List<STCM> STLIST = new List<STCM>();
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<STCM>("STC");
            var builder = Builders<STCM>.Filter;
            var filter = builder.Eq("PARTI", aData);
            STLIST = collection.Find(filter).ToList();
            return new JsonResult { Data = STLIST, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        [HttpPost]
        public ActionResult UPSTC(STCM e)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var database = client.GetDatabase("appharbor_gxm5g8zh");
                    var collection = database.GetCollection<STCM>("STC");
                    var builder = Builders<STCM>.Filter;
                    var filter = builder.Eq(s => s.PARTI, e.PARTI);
                    var upd = Builders<STCM>.Update
                        .Set("MRP", e.MRP)
                        .Set("QTY", e.QTY)
                        .Set("TOTAL", e.TOTAL)
                        .Set("TAX", e.TAX)
                        .Set("TVAL", e.TVAL)
                        .Set("UNIT", e.UNIT)
                        .Set("SPRICE", e.SPRICE)
                        .Set("USER1", e.USER1);
                    var result = collection.UpdateOne(filter, upd);
                    message = "Successfully Saved!";
                }
                catch (Exception ex) { message = ex.ToString(); }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(e);
            }
        }

        [Authorize]
        public JsonResult dtls(int id)
        {
            List<BILLM> STLIST = new List<BILLM>();
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<BILLM>("BILL");
            var builder = Builders<BILLM>.Filter;
            var filter = builder.Eq("BILLID", id);
            STLIST = collection.Find(filter).ToList();
            return new JsonResult { Data = STLIST, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [Authorize]
        public JsonResult sdtls(string id)
        {
            List<STCM> STLIST = new List<STCM>();
            var database = client.GetDatabase("appharbor_gxm5g8zh");
            var collection = database.GetCollection<STCM>("STC");
            var builder = Builders<STCM>.Filter;
            var filter = builder.Eq("PARTI", id);
            STLIST = collection.Find(filter).ToList();
            return new JsonResult { Data = STLIST, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        [HttpPost]
        public ActionResult UPD(BILLM e)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var database = client.GetDatabase("appharbor_gxm5g8zh");
                    var collection = database.GetCollection<BILLM>("BILL");
                    var builder = Builders<BILLM>.Filter;
                    var filter = builder.Eq(s => s.BILLID, e.BILLID);
                    var upd = Builders<BILLM>.Update
                        .Set("MRP", e.MRP)
                        .Set("SQTY", e.SQTY)
                        .Set("TOTAL", e.TOTAL)
                        .Set("TVAL", e.TVAL)
                        .Set("UNIT", e.UNIT)
                        .Set("SPRICE", e.SPRICE)
                        .Set("HSN_NO", e.HSN_NO)
                        .Set("PARTI", e.PARTI)
                        .Set("IGST", e.IGST)
                        .Set("IGSTR", e.IGSTR)
                        .Set("CGST", e.CGSTR)
                        .Set("SGST", e.SGST)
                        .Set("SGSTR", e.SGSTR)
                        .Set("STOT", e.STOT)
                        .Set("UNIT", e.UNIT)
                        .Set("USER1", e.USER1);
                    var result = collection.UpdateOne(filter, upd);
                    message = "Successfully Saved!";
                }
                catch (Exception ex) { message = ex.ToString(); }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(e);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult DELITEM_BILL(int id)
        {
            string message = "";
            try
            {
                var database = client.GetDatabase("appharbor_gxm5g8zh");
                var collection = database.GetCollection<BILLM>("BILL");
                var builder = Builders<BILLM>.Filter;
                var filter = builder.Eq(s => s.BILLID, id);
                var result = collection.DeleteOne(filter);
                message = "Successfully Saved!";
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        [HttpPost]
        public ActionResult ADDBILL(PUCHM bill)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var database = client.GetDatabase("appharbor_gxm5g8zh");
                    var collection = database.GetCollection<PUCHM>("BILL1");
                    collection.InsertOne(bill);
                    message = "Successfully Saved!";
                }
                catch (Exception ex) { message = ex.ToString(); }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(bill);
            }
        }

        public JsonResult RCON()
        {
            string message = "";
            try
            {
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                int rc =Convert.ToInt32(webConfigApp.AppSettings.Settings["RCON"].Value.ToString());
                int CR = rc + 1;
                webConfigApp.AppSettings.Settings["RCON"].Value = Convert.ToString(CR);
                webConfigApp.Save();
                message = Convert.ToString(CR);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}