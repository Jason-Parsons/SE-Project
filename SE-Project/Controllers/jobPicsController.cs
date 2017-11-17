using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SE_Project.Models;

namespace SE_Project.Controllers
{
    public class jobPicsController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: jobPics
        public ActionResult Index()
        {
            int id = (int)Session["userID"];
            var list = db.jobs.Where(x => x.CreatedBy == id);

            var jobPics = db.jobPics.Include(j => j.job);
            return View(jobPics.ToList());
        }

        // GET: jobPics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobPic jobPic = db.jobPics.Find(id);
            if (jobPic == null)
            {
                return HttpNotFound();
            }
            return View(jobPic);
        }

        // GET: jobPics/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address");
            return View();
        }

        // POST: jobPics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,Before,After")] jobPic jobPic)
        {
            if (ModelState.IsValid)
            {
                db.jobPics.Add(jobPic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", jobPic.JobID);
            return View(jobPic);
        }

        // GET: jobPics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobPic jobPic = db.jobPics.Find(id);
            if (jobPic == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", jobPic.JobID);
            return View(jobPic);
        }

        // POST: jobPics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,Before,After")] jobPic jobPic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", jobPic.JobID);
            return View(jobPic);
        }

        // GET: jobPics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobPic jobPic = db.jobPics.Find(id);
            if (jobPic == null)
            {
                return HttpNotFound();
            }
            return View(jobPic);
        }

        // POST: jobPics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobPic jobPic = db.jobPics.Find(id);
            db.jobPics.Remove(jobPic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
