using System.ComponentModel.DataAnnotations.Schema;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseItem
	{
		#region Properties

		[JsonProperty]
		[ModelField(IDProperty=true, UseNull=true)]
        [Field(FieldType = typeof(Ext.Net.Hidden) )]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

		[Field(Ignore=true)]
		public string PhantomId { get; set; }

		[Field(Ignore=true)]
		[ModelField(Ignore = true)]
        public virtual bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }

		private DateTime dateCreated = DateTime.Now;

        [JsonProperty]
		[ModelField(Ignore = true)]
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

        [JsonProperty]
		[ModelField(Ignore = true)]
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
