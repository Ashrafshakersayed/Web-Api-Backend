using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Services
{
    public interface IFavouriteProductService
    {

        Task<FavouriteProductsModel[]> GetFavouriteProductByUserId(string userId);

    }
}
