using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiJWT.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double totalPrice { get; set; }
        [ForeignKey("appUser")]
        public string userId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ApplicationUser appUser { get; set; }
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Category")]
        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        [Required, Key]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class FavouriteProducts
    {
        public int Id { get; set; }
        [ForeignKey("appUser")]
        public string userId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser appUser { get; set; }
    }

    public class OrderedProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        [ForeignKey("Order")]
        public int orderId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }

    public class ShoppingCart
    {
        [Key, ForeignKey("appUser")]
        public string Id { get; set; }
        public double totalPrice { get; set; }
        public virtual ApplicationUser appUser { get; set; }
        public virtual ICollection<ShoppingCartProducts> ShoppingCartProducts { get; set; }
    }

    public class ShoppingCartProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        [ForeignKey("ShoppingCart")]
        public string shoppingCartId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }

    public class PaymentMethod
    {
        [Key, ForeignKey("Order")]
        public int Id { get; set; }
        public string Method { get; set; }
        public virtual Order Order { get; set; }
    }

    public class Paypal
    {
        [Key, ForeignKey("PaymentMethod")]
        public int Id { get; set; }
        public string Account { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }

    public class Visa
    {
        [Key, ForeignKey("PaymentMethod")]
        public int Id { get; set; }
        public long Number { get; set; }
        public string Expire { get; set; }
        public int SequreCode { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
