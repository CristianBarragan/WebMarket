using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMarket.Model.Data
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
