using System;
using System.ComponentModel.DataAnnotations;

namespace Uber.Core
{
    public abstract class BaseItem
    {
        public virtual int Id { get; set; }

        public virtual bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }

        public virtual void SetDateCreated()
        {
            if (this.IsNew)
            {
                this.DateCreated = DateTime.Now;
            }

            this.SetDateUpdated();
        }

        public virtual void SetDateUpdated()
        {
            this.DateUpdated = DateTime.Now;
        }

        private DateTime dateCreated = DateTime.Now;

        [Required]
        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated;
            }
            private set
            {
                this.dateCreated = value;
            }
        }

        private DateTime dateUpdated = DateTime.Now;

        [Required]
        public DateTime DateUpdated
        {
            get
            {
                return this.dateUpdated;
            }
            private set
            {
                this.dateUpdated = value;
            }
        }
    }
}
