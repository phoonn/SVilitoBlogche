using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAcess;
using DataAcess.Repositories;
using DataModel.Identity;
using DataModel.Identity.ManagerAndStore;
using IdentityMVC.Models;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using Markdig;

namespace IdentityMVC.Controllers
{
    public class BlogPostsController : Controller
    {

        private readonly IBlogPostRepository Repo;

        //inject dependency 
        public BlogPostsController(IBlogPostRepository Repo)
        {
            this.Repo = Repo;
        }

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = Repo.GetAll();
            foreach (var item in blogPosts)
            {
                //item.TextPost = Markdown.ToPlainText(item.TextPost);
                item.TextPost = Server.HtmlDecode(Server.HtmlEncode(item.TextPost));
                //item.TextPost = CommonMark.CommonMarkConverter.Convert(item.TextPost);
            }
            return View(blogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = Repo.GetById((int)id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            blogPost.TextPost =Server.HtmlDecode(Server.HtmlEncode(blogPost.TextPost));
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(PostModel postModel)
        {
            BlogPost newPost = new BlogPost();
            if (ModelState.IsValid)
            {
                newPost.TextPost = postModel.PostContent;
                newPost.TitleOfPost = postModel.Title;
                newPost.UserId = Convert.ToInt32(User.Identity.GetUserId());
                newPost.DateOfPost = DateTime.Now;
            }

            byte[] fileData = null;

            int count  = Request.Files.Count;

            if(Request.Files.Count != 0)
            {
                using (var binaryReader = new BinaryReader(Request.Files["photo"].InputStream))
                {
                    if (Request.Files["photo"].ContentLength == 0)
                    {
                        return RedirectToAction("Index");
                    }

                    fileData = binaryReader.ReadBytes(Request.Files["photo"].ContentLength);
                    newPost.PictureContent = fileData;
                }
            }
            

            Repo.Save(newPost);
            Repo.Commit();
            

            return RedirectToAction("Index");

        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = Repo.GetById((int)id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            PostModel EditPost = new PostModel(blogPost.Id,blogPost.TextPost, blogPost.TitleOfPost, blogPost.PictureContent);
            return View(EditPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                BlogPost newPost = new BlogPost();

                newPost.Id = postModel.PostId;
                newPost.TextPost = postModel.PostContent;
                newPost.TitleOfPost = postModel.Title;
                newPost.UserId = Convert.ToInt32(User.Identity.GetUserId());

                if (Request.Files.Count > 0)
                {
                    using (var binaryReader = new BinaryReader(Request.Files["photo"].InputStream))
                    {
                        if (Request.Files["photo"].ContentLength != 0)
                        {
                            newPost.PictureContent = binaryReader.ReadBytes(Request.Files["photo"].ContentLength);
                        }

                    }
                }

                Repo.Update(newPost);
                Repo.Commit();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = Repo.GetById((int) id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = Repo.GetById(id);
            Repo.Delete(blogPost);
            Repo.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public void DeleteImage(int id)
        {
            BlogPost blogPost = Repo.GetById(id);
            blogPost.PictureContent = null;
            Repo.Update(blogPost);
            Repo.Commit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
