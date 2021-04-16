using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiJWT.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
    }

    public class FavouriteProductsModel
    {
        public int Id { get; set; }

        public string userId { get; set; }

        public int productId { get; set; }
    }

    public class OrderedProductsModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        public int productId { get; set; }
        
        public int orderId { get; set; }
    }

    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double totalPrice { get; set; }
        public string userId { get; set; }

    }

    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Method { get; set; }
    }


}
