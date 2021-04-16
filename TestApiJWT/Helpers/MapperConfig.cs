using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<Category, CategoryModel>().ReverseMap();
            this.CreateMap<FavouriteProducts, FavouriteProductsModel>().ReverseMap();
            this.CreateMap<OrderedProducts, OrderedProductsModel>().ReverseMap();
            this.CreateMap<Order, OrderModel>().ReverseMap();
            this.CreateMap<PaymentMethod, PaymentMethodModel>().ReverseMap();

        }
    }
}
