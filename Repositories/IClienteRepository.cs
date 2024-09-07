using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Repository.Interface
{
    public interface IClienteRepository
    {
        List<ClienteDTO> getList();
        bool insert(ClienteDTO clienteDTO);
        bool update(ClienteDTO clienteDTO);
        bool delete(int id);
    }
}
