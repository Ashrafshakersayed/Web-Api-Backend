using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public interface IProductService
    {
        Task<productsPagination> GetProducts(int pageNum, int limt );

        Task<ICollection<ProductModel>> Search(string keyword);

    }
}
