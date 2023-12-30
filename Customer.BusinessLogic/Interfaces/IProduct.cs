using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ResponseResults CreateProduct(ProductModel model);
        ResponseResults UpdateProduct(ProductModel model);
        ResponseResults<List<ProductModel>> GetProduct();
        ResponseResults<ProductModel> GetProductById(int Id);
        ResponseResults DeleteProduct(int id);
    }
}
