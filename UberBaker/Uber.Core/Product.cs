namespace Uber.Core
{
    public class Product : BaseItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual double UnitPrice { get; set; }

        //public ProductType Type { get; set; }
    }
}
