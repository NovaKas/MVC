using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationLogin.Models;

namespace WebApplicationLogin.Controllers
{
    public class MySubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MySubjects
        public ActionResult Index()
        {
            var mySubjects = db.MySubjects.Include(m => m.Subject).Include(m => m.user);
            return View(mySubjects.ToList());
        }

        // GET: MySubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySubject mySubject = db.MySubjects.Find(id);
            if (mySubject == null)
            {
                return HttpNotFound();
            }
            return View(mySubject);
        }

        // GET: MySubjects/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            //ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: MySubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MySubjectID,userID,SubjectID")] MySubject mySubject)
        {
            if (ModelState.IsValid)
            {
                db.MySubjects.Add(mySubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", mySubject.SubjectID);
            //ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", mySubject.userID);
            return View(mySubject);
        }

        // GET: MySubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySubject mySubject = db.MySubjects.Find(id);
            if (mySubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", mySubject.SubjectID);
            //ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", mySubject.userID);
            return View(mySubject);
        }

        // POST: MySubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MySubjectID,userID,SubjectID")] MySubject mySubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mySubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", mySubject.SubjectID);
            //ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", mySubject.userID);
            return View(mySubject);
        }

        // GET: MySubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySubject mySubject = db.MySubjects.Find(id);
            if (mySubject == null)
            {
                return HttpNotFound();
            }
            return View(mySubject);
        }

        // POST: MySubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MySubject mySubject = db.MySubjects.Find(id);
            db.MySubjects.Remove(mySubject);
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
