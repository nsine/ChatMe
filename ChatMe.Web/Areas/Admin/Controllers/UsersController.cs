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
using ChatMe.DataAccess.Interfaces;
using AutoMapper;
using ChatMe.Web.Areas.Admin.Models;
using ChatMe.Web.Areas.Admin.Controllers.Abstract;
using Microsoft.AspNet.Identity;

namespace ChatMe.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminAuthorizeController
    {
        private IUnitOfWork db;

        public UsersController(IUnitOfWork unitOfWork) {
            db = unitOfWork;
        }

        // GET: Admin/Users
        public async Task<ActionResult> Index() {
            var users = await db.Users.Users.ToListAsync();
            var userViewModels = users.Select(user => ToViewModel(user));

            return View(userViewModels);
        }

        // GET: Admin/Users/Details/5
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = await db.Users.FindByIdAsync(id);
            if (user == null) {
                return HttpNotFound();
            }
            return View(ToViewModel(user));
        }

        // GET: Admin/Users/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewUserViewModel userViewModel) {
            if (ModelState.IsValid) {
                var user = new User();
                FromViewModel(userViewModel, user);

                await db.Users.CreateAsync(user, userViewModel.Password);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        // GET: Admin/Users/Edit/5
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = await db.Users.FindByIdAsync(id);

            if (user == null) {
                return HttpNotFound();
            }

            return View(ToViewModel(user));
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel userViewModel) {
            if (ModelState.IsValid) {
                var user = await db.Users.FindByIdAsync(userViewModel.Id);
                FromViewModel(userViewModel, user);
                
                await db.Users.UpdateAsync(user);

                var roles = userViewModel.RolesString.Split(' ');
                foreach (var role in roles) {
                    await db.Users.AddToRoleAsync(user.Id, role);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // GET: Admin/Users/Delete/5
        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindByIdAsync(id);

            if (user == null) {
                return HttpNotFound();
            }
            return View(ToViewModel(user));
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            User user = await db.Users.FindByIdAsync(id);
            await db.Users.DeleteAsync(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private UserViewModel ToViewModel(User user) {
            var viewModel = new UserViewModel {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.UserInfo.FirstName,
                LastName = user.UserInfo.LastName,
                Email = user.Email,
                Phone = user.UserInfo.Phone,
                Skype = user.UserInfo.Skype,
                AboutMe = user.UserInfo.AboutMe,
                RegistrationDate = user.UserInfo.RegistrationDate,
                AvatarFilename = user.UserInfo.AvatarFilename,
                AvatarPath = Url.RouteUrl("Avatar", new { userId = user.Id }),
            };

            var roles = db.Users.GetRoles(user.Id);
            viewModel.RolesString = string.Join(" ", roles);

            return viewModel;
        }

        private void FromViewModel(UserViewModel viewModel, User user) {
            user.UserName = viewModel.UserName;
            user.UserInfo.FirstName = viewModel.FirstName;
            user.UserInfo.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.UserInfo.Phone = viewModel.Phone;
            user.UserInfo.Skype = viewModel.Skype;
            user.UserInfo.AboutMe = viewModel.AboutMe;
        }
    }
}
