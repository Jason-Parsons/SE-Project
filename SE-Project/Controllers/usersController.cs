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
    public class usersController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: users
        public ActionResult Index()
        {
            var users = db.users.Include(u => u.userAccess1);
            return View(users.ToList());
        }

        public ActionResult Success()
        {
            return View();
        }

       
        public ActionResult Login(SE_Project.Models.user userModel)
        {
            using (VolsDBEntities db = new VolsDBEntities())
            {

                var userDetails = db.users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                   // userModel.loginErrorMessage = "Wrong Email or Password.";
                    return View("Login", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userAccess"] = userDetails.UserAccess;
                    Session["userName"] = userDetails.FirstName;
                    if (userDetails.UserAccess == 1)
                    {
                        return RedirectToAction("Index", "volunteer");
                    }
                    else if(userDetails.UserAccess == 2)
                    {
                        return RedirectToAction("Index", "organizer");
                    }
                    else
                    {
                        return RedirectToAction("Index", "volunteer");
                    }
                }
            } 
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.UserAccess = new SelectList(db.userAccesses, "UserAccess1", "UserAccess1");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,MiddleName,LastName,Email,Phone,Password,UserAccess")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.UserAccess = new SelectList(db.userAccesses, "UserAccess1", "UserAccess1", user.UserAccess);
            return View(user);
        }

        // GET: users/Edit/5
       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserAccess = new SelectList(db.userAccesses, "UserAccess1", "UserAccess1", user.UserAccess);
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,MiddleName,LastName,Email,Phone,Password,UserAccess")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserAccess = new SelectList(db.userAccesses, "UserAccess1", "UserAccess1", user.UserAccess);
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
