using AdminProductos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminProductos.Controllers
{
    public class AccesoController : Controller
    {
        //Aqui empieza la aplicacion web, donde se instancia la clase conection con la base de dstos usando entity
        DBproductosEntities DB = new DBproductosEntities();

        public ActionResult Login()
        {
            return View();
        }

        //Metodo para inicar sesion que recibe correo y contraseña
        [HttpPost]
        public ActionResult Login(string correo, string pass)
        {

            try
            {
                // confirma en la base de datos que el usuario y la contrasena coincidan
                var Usuario = (from d in DB.Usuario
                               where d.Correo == correo.Trim() && d.Contrasena == pass.Trim()
                               select d).FirstOrDefault();
                if (Usuario == null)
                {
                    ViewBag.error = "Usuario o contrasena no registrados";
                    return View();
                }

                Session["Usuario"] = Usuario;


                return RedirectToAction("Inicio", "MenuPrincipal");
            }
            //en caso de que haya un error devolvera el mensaje en el viewbag
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
                throw;
            }
        }

        //Metodo para registrar usuario
        public ActionResult Registrar()
        {
            return View();
        }

        //se confirma que los datos recibidos para registrar usuaruio coincidan con los datos basicos del usuario
        [HttpPost]
        public ActionResult Registrar([Bind(Include = "Id,Correo,Contrasena")] Usuario User)
        {
            if (ModelState.IsValid)
            {
                DB.Usuario.Add(User);
                DB.SaveChanges();
                return RedirectToAction("Login", "Acceso");

            }
            //en caso de que se envie la información incorrecta se devuelve los mismos datos ingredados mas mensaje de error que está en la vista

            return View(User);
        }
    }
}