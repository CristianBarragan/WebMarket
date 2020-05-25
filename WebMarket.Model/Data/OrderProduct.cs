using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMarket.Model.Data
{
    public class OrderProduct
    {
        [ForeignKey("Order")]
        public long OrderId { get; set; }

        [ForeignKey("Product")]
        public long ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal SubTotal { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
