using Microsoft.AspNetCore.Mvc;
using MvcCorePersonajesSeriesAzure.Models;
using MvcCorePersonajesSeriesAzure.Services;

namespace MvcCorePersonajesSeriesAzure.Controllers
{
    public class PersonajesController : Controller
    {
        private PersonajesService service;
        public PersonajesController(PersonajesService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<PersonajesSeries>personajes=await this.service.GetAllPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonajesSeries personaje)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            await this.service.InsertPersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int idpersonaje)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            PersonajesSeries personaje = await this.service.FindPersonajeByIdAsync(idpersonaje);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonajesSeries personaje)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idpersonaje)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int idpersonaje)
        {
            PersonajesSeries personaje = await this.service.FindPersonajeByIdAsync(idpersonaje);
            return View(personaje);
        }

        public async Task<IActionResult> Buscador()
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Buscador(string serie)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            List<PersonajesSeries> personajes=await this.service.GetPersonajesSeriesAsync(serie);
            return View(personajes);
        }
    }
}
