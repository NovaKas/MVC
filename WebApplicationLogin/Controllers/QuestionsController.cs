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
            var items = new List<QuestionViewModel>();
            foreach (var item in db.Questions.Include(q => q.Quizs))
            {
                var question = new QuestionViewModel
                {
                    Id = item.QuestionID,
                    BadAnswer = item.BadAnswer,
                    Content = item.Content,
                    GoodAnswer = item.GoodAnswer,
                    Points = item.Points
                };
                question.QuizIds = item.Quizs.Select(y => y.QuizID).Any() ? item.Quizs.Select(y => y.QuizID).ToList() : new List<int>();
                items.Add(question);
            }

            return View(items);
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
                QuizIds = question.Quizs.Select(y => y.QuizID).Any() ? question.Quizs.Select(y => y.QuizID).ToList() : new List<int>(),
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
            return View(new QuestionViewModel());
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,GoodAnswer,BadAnswer,Points,QuizIds")] QuestionViewModel question)
        {
            if (ModelState.IsValid)
            {
                var selectedQuizs = db.Quizs.Where(x => question.QuizIds.Contains(x.QuizID)).ToList();
                var model = new Question
                {
                    QuestionID = question.Id,
                    Quizs = selectedQuizs,
                    BadAnswer = question.BadAnswer,
                    Content = question.Content,
                    GoodAnswer = question.GoodAnswer,
                    Points = question.Points
                };

                db.Questions.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuizs()
        {
            var userId = User.Identity.GetUserId();
            var quizsForTeacher = db.Quizs.Where(x => x.User.Id == userId);
            var list = new MultiSelectList(quizsForTeacher, "QuizID", "Name");
            return Json(list, JsonRequestBehavior.AllowGet);
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

            var model = new QuestionViewModel {
                Id = question.QuestionID,
                BadAnswer = question.BadAnswer,
                Content = question.Content,
                GoodAnswer = question.GoodAnswer,
                Points = question.Points,
                QuizIds = question.Quizs.Select(y => y.QuizID).Any() ? question.Quizs.Select(y => y.QuizID).ToList() : new List<int>()
            };

            return View(model);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,GoodAnswer,BadAnswer,Points,userID,QuizIds")] QuestionViewModel question)
        {
            if (ModelState.IsValid)
            {
                var model = db.Questions.Include(q => q.Quizs).SingleOrDefault(x => x.QuestionID == question.Id);
                if(model.GoodAnswer != question.GoodAnswer)
                {
                    model.GoodAnswer = question.GoodAnswer;
                }
                if (model.BadAnswer != question.BadAnswer)
                {
                    model.BadAnswer = question.BadAnswer;
                }
                if (model.Points != question.Points)
                {
                    model.Points = question.Points;
                }
                if (model.Content != question.Content)
                {
                    model.Content = question.Content;
                }

                var userId = User.Identity.GetUserId();
                var existsQuizsIds = model.Quizs.Select(x => x.QuizID);
                var selectedQuizsIds = db.Quizs.Where(x => question.QuizIds.Contains(x.QuizID)).Select(x => x.QuizID).ToList();

                var differencesToDelete = existsQuizsIds.Except(selectedQuizsIds).ToList(); // Delete
                if (differencesToDelete.Any())
                {
                    var differencesToDeleteItems = db.Quizs.Where(x => differencesToDelete.Contains(x.QuizID));
                    foreach (var item in differencesToDeleteItems)
                    {
                        model.Quizs.Remove(item);
                    }
                }

                var differencesToAdd = selectedQuizsIds.Except(existsQuizsIds).ToList(); // Add
                if(differencesToAdd.Any())
                {
                    model.Quizs.AddRange(db.Quizs.Where(x => differencesToAdd.Contains(x.QuizID)));
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

            var model = new QuestionViewModel
            {
                Id = question.QuestionID,
                BadAnswer = question.BadAnswer,
                Content = question.Content,
                GoodAnswer = question.GoodAnswer,
                Points = question.Points,
                QuizIds = question.Quizs.Select(y => y.QuizID).Any() ? question.Quizs.Select(y => y.QuizID).ToList() : new List<int>()
            };

            return View(model);
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
