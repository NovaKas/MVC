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
    public class QuizsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quizs
        public ActionResult Index()
        {
            var model = db.Quizs.Select(x => new QuizViewModel
            {
                Id = x.QuizID,
                Name = x.Name,
                Timer = x.Timer
            });
            return View(model);
        }

        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }

            var model = new QuizViewModel
            {
                Id = quiz.QuizID,
                Name = quiz.Name,
                Timer = quiz.Timer
            };

            return View(model);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuizID,Name,Timer")] QuizViewModel quiz)
        {
            if (ModelState.IsValid)
            {
                var model = new Quiz
                {
                    Name = quiz.Name,
                    QuizID = quiz.Timer
                };
                db.Quizs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }

            var model = new QuizViewModel
            {
                Id = quiz.QuizID,
                Name = quiz.Name,
                Timer = quiz.Timer
            };

            return View(model);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuizID,Name,Timer")] QuizViewModel quiz)
        {
            if (ModelState.IsValid)
            {
                var model = db.Quizs.SingleOrDefault(x => x.QuizID == quiz.Id);
                if (model.Name != quiz.Name)
                {
                    model.Name = quiz.Name;
                }
                if(model.Timer != quiz.Timer)
                {
                    model.Timer = quiz.Timer;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }

            var model = new QuizViewModel
            {
                Id = quiz.QuizID,
                Name = quiz.Name,
                Timer = quiz.Timer
            };

            return View(model);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizs.Find(id);
            db.Quizs.Remove(quiz);
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
