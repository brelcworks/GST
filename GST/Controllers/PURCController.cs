using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GST.Models;
using System.Web.Security;
using System.Data.Entity;

namespace GST.Controllers
{
    public class PURCController : Controller
    {
        DBCTX dc = new DBCTX();
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

            var dbResult = dc.BILL.Where(b => b.BILL_NO.Equals(BNO)).Select(
                a => new
                {
                    a.BILLID,
                    a.BID,
                    a.HSN_NO,
                    a.PARTI,
                    a.MRP,
                    a.SPRICE,
                    a.PQTY,
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

        [Authorize]
        [HttpPost]
        public ActionResult Save(BILL bill)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBCTX aid = new DBCTX())
                    {
                        aid.BILL.Add(bill);
                        aid.SaveChanges();
                        message = "Successfully Saved!";
                    }
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
            List<STC> STLIST1 = new List<STC>();
            using (DBCTX FC = new DBCTX())
            {
                STLIST1 = FC.STC.Where(a => a.PARTI.Equals(aData)).ToList();
                var cn = STLIST1.Count();
                msg = cn;
            }
            return msg;
        }

        [Authorize]
        [HttpPost]
        public ActionResult ADSTC(STC bill)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBCTX aid = new DBCTX())
                    {
                        aid.STC.Add(bill);
                        aid.SaveChanges();
                        message = "Successfully Saved!";
                    }
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
            itms = dc.STC.Where(x => x.PARTI.StartsWith(term))
                .Select(y => y.PARTI).ToList();
            return Json(itms, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult gdata2(string aData)
        {
            List<STC> STLIST = new List<STC>();
            using (DBCTX FC = new DBCTX())
            {
                STLIST = FC.STC.Where(A => A.PARTI.Equals(aData)).ToList();
            }
            return new JsonResult { Data = STLIST, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        [HttpPost]
        public ActionResult UPSTC(STC TBL)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    dc.Entry(TBL).State = EntityState.Modified;
                    dc.SaveChanges();
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
                return View(TBL);
            }
        }
    }
}