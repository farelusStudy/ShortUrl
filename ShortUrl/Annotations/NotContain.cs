using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShortUrl.Annotations
{
    public class NotContain : ValidationAttribute
    {
        private static string _substring;

        public NotContain(string Substring)
        {
            _substring = Substring;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string str = value.ToString();
                return !str.Contains(_substring);
            }
            return false;
        }
    }
}