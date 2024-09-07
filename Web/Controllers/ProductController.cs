using DTOs;
using DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Services;
using UnitOfWork.SqlServer;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService;
        public ProductController()
        {
            this.productService = new ProductService(new UnitOfWorkSqlServer()); 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Consultar()
        {
            var resp = productService.Consultar();
            return Json(resp);

        }

        [HttpPost]
        public IActionResult Agregar([FromBody]ProductDTO request)
        {
            var resp = productService.Agregar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] ProductDTO request)
        {
            var resp = productService.Editar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            var resp = productService.Eliminar(id);
            return Json(resp);
        }
    }
}
