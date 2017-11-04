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
    public class userJobsController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: userJobs
        public ActionResult Index()
        {
            var userJobs = db.userJobs.Include(u => u.job).Include(u => u.user);
            return View(userJobs.ToList());
        }

        // GET: userJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userJob userJob = db.userJobs.Find(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            return View(userJob);
        }

        // GET: userJobs/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address");
            ViewBag.UserID = new SelectList(db.users, "UserID", "FirstName");
            return View();
        }

        // POST: userJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserJobID,UserID,JobID")] userJob userJob)
        {
            if (ModelState.IsValid)
            {
                db.userJobs.Add(userJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", userJob.JobID);
            ViewBag.UserID = new SelectList(db.users, "UserID", "FirstName", userJob.UserID);
            return View(userJob);
        }

        // GET: userJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userJob userJob = db.userJobs.Find(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", userJob.JobID);
            ViewBag.UserID = new SelectList(db.users, "UserID", "FirstName", userJob.UserID);
            return View(userJob);
        }

        // POST: userJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserJobID,UserID,JobID")] userJob userJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobID = new SelectList(db.jobs, "JobID", "Address", userJob.JobID);
            ViewBag.UserID = new SelectList(db.users, "UserID", "FirstName", userJob.UserID);
            return View(userJob);
        }

        // GET: userJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userJob userJob = db.userJobs.Find(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            return View(userJob);
        }

        // POST: userJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userJob userJob = db.userJobs.Find(id);
            db.userJobs.Remove(userJob);
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
