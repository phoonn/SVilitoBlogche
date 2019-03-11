using DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityMVC.Models
{
    public class PostViewModel
    {
        public PostViewModel() { }

        public PostViewModel(PagedListModel<PostComment> commentsList, BlogPost blogPost)
        {
            this.commentsList = commentsList;
            this.blogPost = blogPost;
        }

        public PagedListModel<PostComment> commentsList { get; set; }
        public BlogPost blogPost { get; set; }
    }
}