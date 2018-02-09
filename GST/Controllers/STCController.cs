using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GST.Models;

namespace GST.Controllers
{
    public class STCController : Controller
    {
        DBCTX dc = new DBCTX();
        [Authorize]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(STC e)
        {
            using (dc)
            {
                dc.STC.Add(e);
                dc.SaveChanges();
            }
            return View();
        }

        public JsonResult List_Pr(int page, int rows, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var dbResult = dc.STC.Select(
                a => new
                {
                    a.RID,
                    a.HSN_NO,
                    a.PARTI,
                    a.MRP,
                    a.TAX,
                    a.QTY,
                    a.TOTAL,
                    a.UNIT
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
        public ActionResult LIST()
        {
            return View(dc.STC.ToList());
        }

        [HttpPost]
        public string crt([Bind(Exclude = "RID")] STC objPMR)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    dc.STC.Add(objPMR);
                    dc.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation Failed";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public string edt(STC objPMR)
        {
            string msg;
            try
            {
                dc.Entry(objPMR).State = EntityState.Modified;
                dc.SaveChanges();
                msg = "Saved Successfully";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public string dlt(int id)
        {
            STC dl = dc.STC.Find(id);
            dc.STC.Remove(dl);
            dc.SaveChanges();
            return "Deleted Successfully!";
        }
    }
}