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
using DTOs.Product;

namespace Services
{
    public interface IProductService
    {
        List<ProductDTO> Consultar();
        Respuesta Editar(ProductDTO request);
        Respuesta Eliminar(int id);
        Respuesta Agregar(ProductDTO request);
    }

    public class ProductService : IProductService
        {
        private IUnitOfWork _unitOfWork;

            public ProductService(IUnitOfWork uow)
            {
                _unitOfWork = uow;
            }

        public Respuesta Agregar(ProductDTO request)
        {
            Respuesta resp = new Respuesta();
            if(request==null || request.Nombre.Length<3)
            {
                resp.status = false;
                resp.msg = "Error en los campos";
                return resp;
            }
            using( var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.ProductRepository.insert(request);
                if (!resp.status)
                {
                    resp.msg = "Error al crear la entidad";
                }
                context.saveChanges();
            }

            return resp;
        }

        public List<ProductDTO> Consultar()
            {
                List<ProductDTO> result;
                ProductIndexDTO resp = new ProductIndexDTO();
                using (var context = _unitOfWork.Create())
                {
                    result = context.Repositories.ProductRepository.getList();
                    var categories = context.Repositories.CategoryRepository.getList();
                foreach(var product in result)
                {
                    product.Category = categories.Find(category => category.Id == product.CategoryId);
                }
                    resp.Products = result;
                    resp.Categories = categories;
                    
                }
            return result;
            //return null;
            }

        public Respuesta Editar(ProductDTO request)
        {
            Respuesta resp = new Respuesta();
            if (request.Nombre.Length < 3)
            {
                resp.status = false;
                resp.msg = "Error en los camps";
                return resp;
            }
            using (var context = _unitOfWork.Create())
            {
                resp.status = context.Repositories.ProductRepository.update(request);
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
                resp.status = context.Repositories.ProductRepository.delete(id);
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
