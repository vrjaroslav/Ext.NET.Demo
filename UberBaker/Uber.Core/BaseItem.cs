using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Uber.Core
{
    public abstract class BaseItem
	{
		#region Properties

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

		public string PhantomId { get; set; }

        public virtual bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }

		private DateTime dateCreated = DateTime.Now;

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

		#endregion

		#region Methods

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

		#endregion
	}
}
