using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Uber.Web.Helpers
{
    public static class StringHelper
    {
        public static string Pluralize(this string value, int? count = null)
        {
            if (count.HasValue && count == 1)
            {
                return value;
            }
            else
            {
                return System.Data.Entity.Design.PluralizationServices.PluralizationService
                    .CreateService(new CultureInfo("en-US"))
                    .Pluralize(value);
            }
        }
    }
}