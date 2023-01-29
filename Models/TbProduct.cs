using System;
using System.Collections.Generic;

#nullable disable

namespace RealEstate.Models
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbImages = new HashSet<TbImage>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductSalePrice { get; set; }
        public decimal ProductBuyPrice { get; set; }
        public string ImageName { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }

        public virtual TbCategory Category { get; set; }
        public virtual ICollection<TbImage> TbImages { get; set; }
    }
}
