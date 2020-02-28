﻿using ShortUrl.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    public class Link
    {
        public static string MainUrl;

        public int Id { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        //[NotContain(Substring: UrlShorter.MainUrl)]
        public string FullUrl { get; set; }
    }
}