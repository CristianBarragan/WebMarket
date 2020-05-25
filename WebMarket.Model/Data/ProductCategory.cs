using System.ComponentModel.DataAnnotations.Schema;

namespace WebMarket.Model.Data
{
    public class ProductCategory
    {
        [ForeignKey("Product")]
        public long ProductId { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

    }
}
