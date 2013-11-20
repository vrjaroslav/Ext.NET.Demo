using Ext.Net.MVC;

namespace Uber.Web.Models
{
    public abstract class BaseModel
    {
        [ModelField(UseNull = true, IDProperty = true)]
        [Column(Width = 50, Order = 1)]
        public int Id { get; set; }

        [Column(Ignore = true)]
        [Field(Ignore = true)]
        public string PhantomId { get; set; }

        [Column(Ignore = true)]
        public bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }
    }
}