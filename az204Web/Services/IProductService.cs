using az204Web.Models;

namespace az204Web.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}