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
    public class GradesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Grades
        public ActionResult Index(string sortOrder)
        {
            //var grades = db.Grades.Include(g => g.GradeList).Include(g => g.Student).Include(g => g.Subject);
            //return View(grades.ToList());

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var grades = from s in db.Grades
                         select s;
            switch (sortOrder)
            {
                case "name_desc":
                    grades = grades.OrderByDescending(s => s.StudentID);
                    break;
                case "Date":
                    grades = grades.OrderBy(s => s.DateGrade);
                    break;
                case "date_desc":
                    grades = grades.OrderByDescending(s => s.DateGrade);
                    break;
                default:
                    grades = grades.OrderBy(s => s.GradeListID);
                    break;
            }
            return View(grades.ToList());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.GradeListID = new SelectList(db.GradeLists, "GradeListID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeID,StudentID,GradeListID,Weight,Description,DateGrade,SubjectID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeListID = new SelectList(db.GradeLists, "GradeListID", "Name", grade.GradeListID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", grade.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeListID = new SelectList(db.GradeLists, "GradeListID", "Name", grade.GradeListID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", grade.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeID,StudentID,GradeListID,Weight,Description,DateGrade,SubjectID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeListID = new SelectList(db.GradeLists, "GradeListID", "Name", grade.GradeListID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", grade.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
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
