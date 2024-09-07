using DTOs;
using Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.SqlServer;
using UnitOfWork.Interfaces;
using Azure.Core;

namespace Services
{
    public interface IClienteService
    {
        List<ClienteDTO> Consultar();
        Respuesta Editar(ClienteDTO request);
        Respuesta Eliminar(int id);
        Respuesta Agregar(ClienteDTO request);
    }

    public class ClienteService : IClienteService
        {
        private IUnitOfWork _unitOfWork;

            public ClienteService(IUnitOfWork uow)
            {
                _unitOfWork = uow;
            }

        public Respuesta Agregar(ClienteDTO request)
        {
            Respuesta resp = new Respuesta();
            if(request==null || request.Nombre.Length<3 || request.Apellido.Length<3 || request.Edad <= 5)
            {
                resp.status = false;
                resp.msg = "Error en los camps";
                return resp;
            }
            using( var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.ClienteRepository.insert(request);
                if (!resp.status)
                {
                    resp.msg = "Error al crear la entidad";
                }
                context.saveChanges();
            }

            return resp;
        }

        public List<ClienteDTO> Consultar()
            {
                /*var result = new List<ClienteDTO>();
                using (var conn = DB.DBconnect())
                {
                    conn.Open();
                    var command = new SqlCommand("select * from Cliente",conn);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ClienteDTO()
                        {
                            Id = reader.GetInt32(0),
                            Nombre= reader.GetString(1),
                            Apellido= reader.GetString(2),
                            Edad = reader.GetInt32(3),
                        });
                    }
                }
                return result;*/
                List<ClienteDTO> result;
                using (var context = _unitOfWork.Create())
                {
                    result = context.Repositories.ClienteRepository.getList();
                }
                return result;
            }

        public Respuesta Editar(ClienteDTO request)
        {
            Respuesta resp = new Respuesta();
            if (request.Nombre.Length < 3 || request.Apellido.Length < 3 || request.Edad <= 5)
            {
                resp.status = false;
                resp.msg = "Error en los camps";
                return resp;
            }
            using (var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.ClienteRepository.update(request);
                if (!resp.status)
                {
                    resp.msg = "Error al modificar la entidad";
                }
                else
                {
                    context.saveChanges();
                }
            }

            return resp;
        }

        public Respuesta Eliminar(int id)
        {
            Respuesta resp = new Respuesta();
            using (var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.ClienteRepository.delete(id);
                if (!resp.status)
                {
                    resp.msg = "Error al eliminar la entidad";
                }
                else
                {
                    context.saveChanges();
                }
            }

            return resp;
        }
    }
}
