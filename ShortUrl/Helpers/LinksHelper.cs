using ShortUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrl.Helpers
{
    public static class LinksHelper
    {
        /// <summary>
        /// Create html table of links's proporties:
        /// full link as text, short link as link, and button for coping short link
        /// </summary>
        public static MvcHtmlString CreateLinkTable(this HtmlHelper html, IEnumerable<Link> links)
        {
            if (links == null)
            {
                return MvcHtmlString.Create("");
            }
            TagBuilder table = new TagBuilder("table");
            table.MergeAttribute("class", "table");
            foreach (var link in links)
            {
                TagBuilder row = new TagBuilder("tr");
                TagBuilder fullUrlCol = new TagBuilder("td");
                TagBuilder shortUrlCol = new TagBuilder("td");
                TagBuilder btnCopyCol = new TagBuilder("td");

                fullUrlCol.SetInnerText(link.FullUrl);//todo: fix hard code

                TagBuilder shortUrlLink = new TagBuilder("a");
                string fullShort = UrlShorter.MainUrl + link.ShortUrl;
                shortUrlLink.MergeAttribute("href", fullShort);
                shortUrlLink.MergeAttribute("id", $"shortLink{link.Id}");
                shortUrlLink.SetInnerText(fullShort);
                shortUrlCol.InnerHtml = shortUrlLink.ToString();


                TagBuilder copyBtn = new TagBuilder("button");
                string copyScrip = "let tmp   = document.createElement('INPUT');" +
                                   $"  tmp.value = document.getElementById('shortLink{link.Id}').href;" +
                                    "  document.body.appendChild(tmp);" +
                                    "  tmp.select();" +
                                    "  document.execCommand('copy');" +
                                    "  document.body.removeChild(tmp);";
                copyBtn.MergeAttribute("onclick", $"alert(shortLink{link.Id})");
                //copyBtn.MergeAttribute("onclick", $"copyById(shortLink{link.Id})");
                copyBtn.SetInnerText("Копировать");
                btnCopyCol.InnerHtml = copyBtn.ToString();

                row.InnerHtml = fullUrlCol.ToString() + shortUrlCol.ToString() + btnCopyCol.ToString();
                table.InnerHtml += row.ToString();
            }

            return MvcHtmlString.Create(table.ToString());
        }


    }
}