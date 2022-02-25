using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using ProyectoFinal_natalinviquez.Models;
using ProyectoFinal_natalinviquez.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal_natalinviquez.Controllers
{
    public class MaquinasController : Controller
    {

        ServiceCosmosDb service;
        public MaquinasController(ServiceCosmosDb service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ListaMaquinas()
        {
            return View(this.service.GetMaquinas());
        }

        public IActionResult CreateMaquina()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(String accion)
        {
            await this.service.CrearBbddAsync();
            await this.service.CrearColeccionAsync();
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMaquina(Maquina peli)
        {
            var random = new Random(Environment.TickCount);
            peli.probabilidadFallo = random.Next(0, 5);
            peli.estado = true;
            await this.service.InsertarMaquina(peli);
            return RedirectToAction("ListaMaquinas");
        }

        public async Task<IActionResult> Delete(String id)
        {
            await this.service.EliminarMaquina(id);
            return RedirectToAction("ListaMaquinas");
        }

        public async Task<IActionResult> Editar(String id)
        {
            return View(await this.service.FindPeliculaAsyn(id));
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Maquina peli)
        {
            await this.service.ModificarMaquina(peli);
            return RedirectToAction("ListaMaquinas");
        }
    }
}
