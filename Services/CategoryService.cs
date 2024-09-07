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
    public interface ICategoryService
    {
        List<CategoryDTO> Consultar();
        Respuesta Editar(CategoryDTO request);
        Respuesta Eliminar(int id);
        Respuesta Agregar(CategoryDTO request);
    }

    public class CategoryService : ICategoryService
        {
        private IUnitOfWork _unitOfWork;

            public CategoryService(IUnitOfWork uow)
            {
                _unitOfWork = uow;
            }

        public Respuesta Agregar(CategoryDTO request)
        {
            Respuesta resp = new Respuesta();
            if(request==null || request.Nombre.Length<3)
            {
                resp.status = false;
                resp.msg = "Error en los camps";
                return resp;
            }
            using( var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.CategoryRepository.insert(request);
                if (!resp.status)
                {
                    resp.msg = "Error al crear la entidad";
                }
                context.saveChanges();
            }

            return resp;
        }

        public List<CategoryDTO> Consultar()
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

                List<CategoryDTO> result;
                using (var context = _unitOfWork.Create())
                {
                    result = context.Repositories.CategoryRepository.getList();
                }
                return result;
            }

        public Respuesta Editar(CategoryDTO request)
        {
            Respuesta resp = new Respuesta();
            if (request.Nombre.Length < 3)
            {
                resp.status = false;
                resp.msg = "Error en los campos";
                return resp;
            }
            using (var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.CategoryRepository.update(request);
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
