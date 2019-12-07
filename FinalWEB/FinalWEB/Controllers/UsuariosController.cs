using FinalWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalWEB.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Usuarios
        public ActionResult Index()
        {
            var Usuarios = db.AspNetUsers;
            return View(Usuarios.ToList());
        }
    }
}