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
    public class ClienteRepository : Repository, IClienteRepository
    {
        public ClienteRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public bool delete(int id)
        {
            var command = CreateCommand("delete from dbo.Cliente where id = @id;");
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            return true;
        }

        public List<ClienteDTO> getList()
        {
            var list = new List<ClienteDTO>();
            var command = CreateCommand("select * from Cliente");
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new ClienteDTO()
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Edad = reader.GetInt32(3),
                    });
                }
            }
            return list;
        }

        public bool insert(ClienteDTO clienteDTO)
        {
            var command = CreateCommand("insert into Cliente (Nombre,Apellido,Edad) values (@nombre,@apellido,@edad)");
            command.Parameters.AddWithValue("nombre", clienteDTO.Nombre);
            command.Parameters.AddWithValue("apellido", clienteDTO.Apellido);
            command.Parameters.AddWithValue("edad", clienteDTO.Edad);
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }

        public bool update(ClienteDTO clienteDTO)
        {
            var command = CreateCommand("update Cliente set Nombre=@nombre,Apellido=@apellido,Edad=@edad where id=@id;");
            command.Parameters.AddWithValue("id", clienteDTO.Id);
            command.Parameters.AddWithValue("nombre", clienteDTO.Nombre);
            command.Parameters.AddWithValue("apellido", clienteDTO.Apellido);
            command.Parameters.AddWithValue("edad", clienteDTO.Edad);
            var resp = command.ExecuteNonQuery();
            return resp > 0;
        }
    }
}
