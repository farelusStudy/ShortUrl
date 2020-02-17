using ShortUrl.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        ProjectContext db = new ProjectContext();

        public ActionResult Index(Link link = null)
        {
            return View(link ?? new Link());
        }

        [HttpPost]
        public ActionResult GetShortUrl (Link url)
        {
             return View("Index", UrlCutter.GetShortedLink(url.FullUrl));
        }

        public void HandleShort(string code)
        {
            Response.Redirect(UrlCutter.GetFullUrl(code));
        }

        public ActionResult AllLinks()
        {
            var links = db.Links.ToList();
            ViewBag.CurrentUrl = UrlCutter.MainUrl;
            return View(links);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}