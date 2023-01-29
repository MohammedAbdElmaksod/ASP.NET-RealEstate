using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class ShopingItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string ImageName { get; set; }
        public decimal Total { get; set; }
    }
}
