using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_natalinviquez.Models;
using ProyectoFinal_natalinviquez.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_natalinviquez.Controllers
{
    public class ProductoController : Controller
    {

        private  ICosmosDBService _cosmosDbService;
        public ProductoController(ICosmosDBService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }


        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult ListaProducto()
        {
            List<Producto> lista = ListaProducto2().Result;
            return View(lista);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }
 
        [HttpGet]
        [Route("ListaProducto")]
        public async Task<List<Producto>> ListaProducto2()
        {
            try
            {
                return (await _cosmosDbService.GetItemsAsync("SELECT * FROM c")).ToList(); ;
            }
            catch
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Producto item)
        {
            try
            {
                item.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(item);
            }
            catch { throw; }
            return RedirectToAction("ListaProducto");
        }


    }
}
