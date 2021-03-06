﻿using ShortUrl.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<RedirectToRouteResult> GetShortUrl(Link url)
        {
            if(String.IsNullOrEmpty(url.FullUrl)) 
                return RedirectToAction("Index", "Home");
            var link = UrlShorter.GetShortedLink(url.FullUrl, db.Links.ToList());
            db.Links.Add(link);
            await db.SaveChangesAsync();

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
            var link = db.Links.Where(l => l.ShortUrl == code).ToList().FirstOrDefault();
            Response.Redirect(link.FullUrl);
        }
        public ActionResult AllLinks()
        {
            var links = db.Links.ToList();
            ViewBag.CurrentUrl = UrlShorter.MainUrl;
            links.Reverse();
            return View(links);
        }

        public ActionResult DeleteLink(Link link)
        {
            if (link == null) return HttpNotFound();
            db.Links.Remove(link);
            db.SaveChangesAsync();

            return View("AllLinks");
        }
     
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}