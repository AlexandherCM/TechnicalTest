using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalTest.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaService _service;
        public PersonaController(PersonaService service)
            => _service = service;

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PersonaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errores = 
                    ModelState.Where(x => x.Value.Errors.Any())
                              .Select(x => new
                              {
                                  Campo = x.Key,
                                  Errores = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                              })
                              .ToList();

                return View(nameof(Registro));
            }


            _service.AgregarPersona(model);
            return View(nameof(Registro));
        }
    }
}