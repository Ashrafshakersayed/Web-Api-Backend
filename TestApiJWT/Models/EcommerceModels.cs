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

    public class PaypalModel
    {
        public int Id { get; set; }
        [Required]
        public string Account { get; set; }
    }

    public class VisaModel
    {
        public int Id { get; set; }
        [Required, MinLength(14)]
        public long Number { get; set; }
        public string Expire { get; set; }
        [Required, MinLength(3)]
        public int SequreCode { get; set; }
    }

    public class ShoppingCartProductsModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int productId { get; set; }
        public string shoppingCartId { get; set; }
    }

    public class ShoppingCartModel
    {
        public string Id { get; set; }
        public double totalPrice { get; set; }
    }

    public class ProductModel
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> CategoryId { get; set; }
    }

}
