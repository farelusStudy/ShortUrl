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
            ViewBag.UserLinks = GetShortsFromCookies("UserShorts")??new List<Link>();
            ViewBag.CurrentUrl = UrlShorter.MainUrl;

            return View(link ?? new Link());
        }

        private List<Link> GetShortsFromCookies(string cookieName)
        {
            List<Link> userLinks = null; ;
            if (Request.Cookies[cookieName] != null)
            {
                userLinks = new List<Link>();
                string userShorts = Request.Cookies[cookieName].Value;
                var parsedData = userShorts.Split(':').Reverse();
                foreach (var item in parsedData)
                {
                    var currentLink = db.Links.Where(l => l.ShortUrl == item).FirstOrDefault();
                    if (currentLink != null)
                    {
                        userLinks.Add(currentLink);
                    }
                }
            }
            return userLinks;
        }

        [HttpPost]
        public RedirectToRouteResult GetShortUrl(Link url)
        {
            if(String.IsNullOrEmpty(url.FullUrl)) return RedirectToAction("/Home/Index");
            var link = UrlShorter.GetShortedLink(url.FullUrl);

            string cookies = "";
            if (Request.Cookies["UserShorts"] != null)
            {
                cookies = Request.Cookies["UserShorts"].Value + ":";
            }
            Response.Cookies["UserShorts"].Value = cookies + link.ShortUrl;
            Response.Cookies["UserShorts"].Expires = DateTime.Now.AddDays(100);

            ViewBag.UserLinks = GetShortsFromCookies("UserShorts");
            ViewBag.CurrentUrl = UrlShorter.MainUrl;
            return RedirectToAction("Index", "Home", new { Id = link.Id, FullUrl = link.FullUrl, ShortUrl = link.ShortUrl });
        }

        public void HandleShort(string code)
        {
            Response.Redirect(UrlShorter.GetFullUrl(code));
        }
        public ActionResult AllLinks()
        {
            var links = db.Links.ToList();
            ViewBag.CurrentUrl = UrlShorter.MainUrl;
            links.Reverse();
            return View(links);
        }

        public string GetCookie()
        {
            string MyCookieValue = "";
            // сначала нам требуется проверить на null наличие cookie
            if (Request.Cookies["Coco"] != null)
                MyCookieValue = Request.Cookies["Coco"].Value;
            if (Request.Cookies["Links"] != null)
            {
                MyCookieValue = Request.Cookies["Links"].Value;
            }
            return MyCookieValue;
        }
        public string SetCookie()
        {
            // создаем cookie
            Response.Cookies["Coco"].Value = "Value";

            // задаем срок истечения срока действия cookie
            Response.Cookies["Coco"].Expires = DateTime.Now.AddDays(1);

            var links = db.Links.ToList();
            string cookieValue = "";
            foreach (var item in links)
            {
                cookieValue += item.ShortUrl + ":";
            }
            Response.Cookies["Links"].Value = cookieValue.Substring(0, cookieValue.Length - 1);
            Response.Cookies["Links"].Expires = DateTime.Now.AddDays(1);

            return GetCookie();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}