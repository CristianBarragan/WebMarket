using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMarket.Model.Data
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DefaultValue(OrderStatus.None)]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "The provided status is not recognised")]
        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
