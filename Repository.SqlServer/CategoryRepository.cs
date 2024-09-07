using Repository.Interface;
using DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(SqlConnection context, SqlTransaction transaction)
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

        public List<CategoryDTO> getList()
        {
            var list = new List<CategoryDTO>();
            var command = CreateCommand("select * from Category");
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new CategoryDTO()
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                    });
                }
            }
            return list;
        }

        public bool insert(CategoryDTO category)
        {
            var command = CreateCommand("insert into Category (Nombre,Descripcion) values (@nombre,@descripcion)");
            command.Parameters.AddWithValue("nombre", category.Nombre);
            command.Parameters.AddWithValue("descripcion", category.Descripcion);
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }

        public bool update(CategoryDTO category)
        {
            var command = CreateCommand("update Cliente set Nombre=@nombre,Descripcion=@descripcion where id=@id;");
            command.Parameters.AddWithValue("id", category.Id);
            command.Parameters.AddWithValue("nombre", category.Nombre);
            command.Parameters.AddWithValue("descripcion", category.Descripcion);
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }
    }
}
