using DataModel.Identity;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository CommentRepo;

        //inject dependency 
        public CommentController(ICommentRepository CommentRepo)
        {
            this.CommentRepo = CommentRepo;
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CreateComment(int postId, string commentText)
        {
            PostComment comment = new PostComment();
            comment.BlogpostId = postId;
            comment.DateOfComment = DateTime.Now;
            comment.TextComment = commentText;
            comment.UserId = Convert.ToInt32(User.Identity.GetUserId());

            CommentRepo.Save(comment);
            CommentRepo.Commit();
        }
        
        [HttpPost]
        public void DeleteComment(int id)
        {
            CommentRepo.Delete(id);
            CommentRepo.Commit();
        }
    }
}