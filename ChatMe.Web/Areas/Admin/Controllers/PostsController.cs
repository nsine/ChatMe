using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatMe.DataAccess.EF;
using ChatMe.DataAccess.Entities;
using ChatMe.Web.Areas.Admin.Models;
using ChatMe.DataAccess.Interfaces;
using ChatMe.Web.Areas.Admin.Controllers.Abstract;

namespace ChatMe.Web.Areas.Admin.Controllers
{
    public class PostsController : AdminAuthorizeController
    {
        private IUnitOfWork db;

        public PostsController(IUnitOfWork unitOfWork) {
            db = unitOfWork;
        }

        // GET: Admin/Posts
        public async Task<ActionResult> Index()
        {
            var posts = await db.Posts.GetAll().ToListAsync();
            var postViewModels = posts.Select(p => ToViewModel(p));

            return View(postViewModels);
        }

        // GET: Admin/Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);

            if (post == null) {
                return HttpNotFound();
            }

            return View(ToViewModel(post));
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            var viewModel = new PostViewModel();
            return View(viewModel);
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Time = DateTime.Now;
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);

            if (post == null) {
                return HttpNotFound();
            }

            return View(ToViewModel(post));
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostViewModel viewModel)
        {
            if (ModelState.IsValid) {
                var post = await db.Posts.FindAsync(viewModel.Id);
                FromViewModel(viewModel, post);
                db.Posts.Update(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);

            if (post == null) {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private PostViewModel ToViewModel(Post post) {
            return new PostViewModel {
                Id = post.Id,
                Body = post.Body,
                UserId = post.UserId,
                UserName = post.User.UserName,
                Time = post.Time
            };
        }

        private void FromViewModel(PostViewModel viewModel, Post post) {
            post.Body = viewModel.Body;
        }
    }
}
