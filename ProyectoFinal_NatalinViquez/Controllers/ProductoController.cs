using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_natalinviquez.Services;

namespace ProyectoFinal_natalinviquez.Controllers
{
    public class ProductoController : Controller
    {

        ServiceCosmosDb service;
        public ProductoController(ServiceCosmosDb service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
