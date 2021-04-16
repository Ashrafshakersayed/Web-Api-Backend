using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<productsPagination> GetProducts( int pageNum, int limit)
        {
            var count = _context.Products.Count();
            pageNum = (pageNum < 1) ? 1 : pageNum;

            var products = _context.Products
                .Skip((pageNum - 1) * limit)
                .Take(limit).ToList();

            var productsModel = _mapper.Map<ProductModel[]>(products);
            var paged = new productsPagination
            {
                CurrentPage = pageNum,
                PageSize = limit,
                TotalItems = count,
                TotalPages = (count + limit - 1) / limit,
                products = productsModel
            };

            return paged;
        }

        public async Task<ICollection<ProductModel>> Search(string keyword)
        {
            var products = _context.Products
                .Where(p => p.Name.ToLower().Contains(keyword.ToLower()));

            var productsModel = _mapper.Map<ProductModel[]>(products);

            return productsModel;
        }
    }
}
