using System.Collections.Generic;
using RealEstate.Models;
namespace RealEstate.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            lstItem = new List<ShopingItem>();
        }
        public List<ShopingItem> lstItem { get; set; }
        public decimal Total { get; set; }
    }
}
