using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uber.Web.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }

        public string PhantomId { get; set; }

        public bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }
    }
}