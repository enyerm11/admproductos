using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminProductos.Controllers
{
    public class MenuPrincipalController : Controller
    {
        // GET: metodo sencillo que sirve como menu principal una vez se haya confirmado el usuario
        public ActionResult Inicio()
        {
            return View(Session["Usuario"]);
        }
    }
}