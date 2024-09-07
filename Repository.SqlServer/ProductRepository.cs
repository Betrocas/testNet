using Repository.Interface;
using DTOs.Product;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public bool delete(int id)
        {
            var command = CreateCommand("delete from dbo.Category where id = @id;");
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            return true;
        }

        public List<ProductDTO> getList()
        {
            var list = new List<ProductDTO>();
            var command = CreateCommand("select * from Product");
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new ProductDTO()
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDouble(2),
                        CategoryId = reader.IsDBNull(3)?0: reader.GetInt32(3),
                    }) ;
                }
            }
            return list;
        }

        public bool insert(ProductDTO category)
        {
            var command = CreateCommand("insert into Category (Nombre,Precio,CategoryId) values (@nombre,@precio,@categoryId)");
            command.Parameters.AddWithValue("nombre", category.Nombre);
            command.Parameters.AddWithValue("precio", category.Precio);
            if(category.CategoryId > 0)
            {
                command.Parameters.AddWithValue("categoryId", category.CategoryId);
            }
            else
            {
                command.Parameters.AddWithValue("categoryId", null);
            }
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }

        public bool update(ProductDTO category)
        {
            var command = CreateCommand("update Cliente set Nombre=@nombre,Precio=@precio,CategoryId=@categoryId where id=@id;");
            command.Parameters.AddWithValue("id", category.Id);
            command.Parameters.AddWithValue("nombre", category.Nombre);
            command.Parameters.AddWithValue("precio", category.Precio);
            if(category.CategoryId > 0)
            {
                command.Parameters.AddWithValue("categoryID", category.CategoryId);
            }
            else
            {
                command.Parameters.AddWithValue("categoryID", null);
            }
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }
    }
}
