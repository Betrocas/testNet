using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTOs.Product;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        List<ProductDTO> getList();
        bool insert(ProductDTO product);
        bool update(ProductDTO product);
        bool delete(int id);
    }
}
