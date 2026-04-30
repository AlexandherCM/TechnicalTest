using App.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TechnicalTest.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaService _service;
        private AlertaViewModel alertaViewModel = new AlertaViewModel();

        public PersonaController(PersonaService service)
            => _service = service;

        protected void ValidateAlert()
        {
            if (TempData["AlertJS"] != null)
            {
                var json = (string)TempData["AlertJS"];
                ViewBag.AlertJS = JsonConvert.DeserializeObject<RequestViewModel>(json) ?? new RequestViewModel();
            }
        }

        protected bool ValidateModel()
        {
            if (ModelState.IsValid)
                return true;

            alertaViewModel.badRequest.Leyenda = "Verifica los campos obligatorios.";
            ViewBag.AlertJS = alertaViewModel.badRequest;
            return false;
        }
            
        public ActionResult Registro()
        {
            ValidateAlert();
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PersonaViewModel model)
        {
            if (!ValidateModel())
                return View(nameof(Registro), model);   

            var request = _service.AgregarPersona(model);
            if (request.Estado)
                request.Redirect = "/Persona/Registros";    

            TempData["AlertJS"] = JsonConvert.SerializeObject(request);
            return RedirectToAction(nameof(Registro));
        }

        [HttpGet]
        public async Task<ActionResult> Registros()       
        {
            var personas = await _service.ObtenerPersonas();
            return View(personas);
        }
    }
}