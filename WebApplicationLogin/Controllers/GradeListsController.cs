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
    [Authorize]
    public class GradeListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GradeList
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.GradeLists.ToList());
        }

        // GET: GradeList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeList gradeList = db.GradeLists.Find(id);
            if (gradeList == null)
            {
                return HttpNotFound();
            }
            return View(gradeList);
        }

        // GET: GradeList/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradeList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeListID,Value,Name")] GradeList gradeList)
        {
            if (ModelState.IsValid)
            {
                db.GradeLists.Add(gradeList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gradeList);
        }

        // GET: GradeList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeList gradeList = db.GradeLists.Find(id);
            if (gradeList == null)
            {
                return HttpNotFound();
            }
            return View(gradeList);
        }

        // POST: GradeList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeListID,Value,Name")] GradeList gradeList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradeList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gradeList);
        }

        // GET: GradeList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeList gradeList = db.GradeLists.Find(id);
            if (gradeList == null)
            {
                return HttpNotFound();
            }
            return View(gradeList);
        }

        // POST: GradeList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradeList gradeList = db.GradeLists.Find(id);
            db.GradeLists.Remove(gradeList);
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
