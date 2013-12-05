using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uber.Web.Models
{
    public class LeftAppMenuModel
    {
        public bool AllowReadProducts { get; set; }
        public bool AllowReadProductTypes { get; set; }
        public bool AllowReadCustomers { get; set; }
        public bool AllowReadOrders { get; set; }
        public bool AllowReadUsers { get; set; }
        public bool AllowReadRoles { get; set; }
    }
}