using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
using UnitOfWork.SqlServer;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        public CategoryController()
        {
            this.categoryService = new CategoryService(new UnitOfWorkSqlServer()); 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Consultar()
        {
            var resp = categoryService.Consultar();
            return Json(resp);

        }

        [HttpPost]
        public IActionResult Agregar([FromBody]CategoryDTO request)
        {
            var resp = categoryService.Agregar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] CategoryDTO request)
        {
            var resp = categoryService.Editar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            var resp = categoryService.Eliminar(id);
            return Json(resp);
        }
    }
}
