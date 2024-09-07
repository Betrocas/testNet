using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
using UnitOfWork.SqlServer;

namespace Web.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteService clienteService;
        public ClienteController()
        {
            this.clienteService = new ClienteService(new UnitOfWorkSqlServer()); 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Consultar()
        {
            var resp = clienteService.Consultar();
            return Json(resp);

        }

        [HttpPost]
        public IActionResult Agregar([FromBody]ClienteDTO request)
        {
            var resp = clienteService.Agregar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] ClienteDTO request)
        {
            var resp = clienteService.Editar(request);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            var resp = clienteService.Eliminar(id);
            return Json(resp);
        }
    }
}
