using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public class FavouriteProductService : IFavouriteProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public FavouriteProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FavouriteProductsModel[]> GetFavouriteProductByUserId(string userId)
        {
            var favProducts = _context.FavouriteProducts.Include(f => f.Product).Where(f => f.userId == userId).ToList();
            var favProductsModel = _mapper.Map<FavouriteProductsModel[]>(favProducts);
            return favProductsModel;
        }

    }
}
