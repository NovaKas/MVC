using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationLogin.Models;
using WebApplicationLogin.Models.ViewModels;

namespace WebApplicationLogin.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Quiz).Include(q => q.user)
                .Select(x => new QuestionViewModel {
                    QuizId = x.QuizID,
                    Id = x.QuestionID,
                    BadAnswer = x.BadAnswer,
                    Content = x.Content,
                    GoodAnswer = x.GoodAnswer,
                    Points = x.Points
                });
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            var model = new QuestionViewModel
            {
                QuizId = question.QuizID,
                Id = question.QuestionID,
                BadAnswer = question.BadAnswer,
                Content = question.Content,
                GoodAnswer = question.GoodAnswer,
                Points = question.Points
            };

            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.QuizID = new SelectList(db.Quizs, "QuizID", "Name");
            ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,GoodAnswer,BadAnswer,Points,QuizID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuizID = new SelectList(db.Quizs, "QuizID", "Name", question.QuizID);
            ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", question.userID);
            return View(question);
        }

        public ActionResult GetQuizs()
        {
            var userId = User.Identity.GetUserId();
            var quizsForTeacher = db.Quizs.Where(x => x.User.Id == userId);
            var list = new SelectList(quizsForTeacher, "QuizID", "Name", 0);
            return Json(list);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizID = new SelectList(db.Quizs, "QuizID", "Name", question.QuizID);
            ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", question.userID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,Content,GoodAnswer,BadAnswer,Points,userID,QuizID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizID = new SelectList(db.Quizs, "QuizID", "Name", question.QuizID);
            ViewBag.userID = new SelectList(db.ApplicationUsers, "Id", "Name", question.userID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
