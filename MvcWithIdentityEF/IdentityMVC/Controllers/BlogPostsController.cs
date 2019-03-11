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
using Ganss.XSS;

namespace IdentityMVC.Controllers
{
    public class BlogPostsController : Controller
    {

        private readonly IBlogPostRepository Repo;
        private readonly ICommentRepository CommentRepo;

        //inject dependency 
        public BlogPostsController(IBlogPostRepository Repo, ICommentRepository CommentRepo)
        {
            this.Repo = Repo;
            this.CommentRepo = CommentRepo;
        }

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = Repo.GetAll();
            foreach (var item in blogPosts)
            {
                var sanitizer = new HtmlSanitizer();
                //item.PostContent = Markdown.ToPlainText(item.PostContent);
                item.PostContent = sanitizer.Sanitize(item.PostContent);
                //item.PostContent = CommonMark.CommonMarkConverter.Convert(item.PostContent);
            }
            return View(blogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id, int page = 1)
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

            var sanitizer = new HtmlSanitizer();
            blogPost.PostContent = sanitizer.Sanitize(blogPost.PostContent);
            //blogPost.PostContent =Server.HtmlDecode(Server.HtmlEncode(blogPost.PostContent));

            IEnumerable<PostComment> postComments;
            int PageSize = 10;
            int TotalCount = CommentRepo.CountCommentsAsync((int)id);
            postComments = CommentRepo.Get(null, o => o.OrderByDescending(i => i.DateOfComment), "", PageSize, (PageSize * (page - 1)));
            int pageCount = (TotalCount % PageSize == 0 ? TotalCount / PageSize : (TotalCount / PageSize + 1));
            var result = new PagedListModel<PostComment>(postComments, TotalCount, pageCount, page);


            PostViewModel postView = new PostViewModel(result,blogPost);

            return View(postView);
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
        public ActionResult Create(BlogPostCreationModel postModel)
        {
            BlogPost newPost = new BlogPost();
            if (ModelState.IsValid)
            {
                newPost.PostContent = postModel.PostContent;
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
        [ValidateAntiForgeryToken]
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
            BlogPostCreationModel EditPost = new BlogPostCreationModel(blogPost.Id,blogPost.PostContent, blogPost.TitleOfPost, blogPost.PictureContent);
            return View(EditPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(BlogPostCreationModel postModel)
        {
            if (ModelState.IsValid)
            {
                BlogPost newPost = new BlogPost();

                newPost.Id = postModel.PostId;
                newPost.PostContent = postModel.PostContent;
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

        //[HttpPost]
        //public void CreateComment(int postId, string commentText)
        //{
        //    PostComment comment = new PostComment();
        //    comment.BlogpostId = postId;
        //    comment.DateOfComment = DateTime.Now;
        //    comment.TextComment = commentText;
        //    comment.UserId = Convert.ToInt32(User.Identity.GetUserId());

        //    CommentRepo.Save(comment);
        //    CommentRepo.Commit();
        //}


        //[HttpPost]
        //public void DeleteComment(int id)
        //{
        //    CommentRepo.Delete(id);
        //    CommentRepo.Commit();
        //}

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
