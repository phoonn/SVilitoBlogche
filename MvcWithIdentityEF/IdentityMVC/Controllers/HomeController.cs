using Ganss.XSS;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogPostRepository Repo;

        public HomeController(IBlogPostRepository repo)
        {
            this.Repo = repo;
        }

        // GET: BlogPosts
        public ActionResult Index()
        {
            var blogPosts = Repo.Get(null, o => o.OrderByDescending(i => i.DateOfPost), "", 2, 0);
            foreach (var item in blogPosts)
            {
                var sanitizer = new HtmlSanitizer();
                //item.PostContent = Markdown.ToPlainText(item.PostContent);
                item.PostContent = sanitizer.Sanitize(item.PostContent);
                //item.PostContent = CommonMark.CommonMarkConverter.Convert(item.PostContent);
            }
            return View(blogPosts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}