using AdminProductos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminProductos.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        DBproductosEntities DB = new DBproductosEntities();

        //En el controlador /Acceso se creo una variable Session con el fin de utilizarla para editar el usuario que inicio sesion
        // En este metodo se devuelve los datos del usuario
        public ActionResult Perfil()
        {
            
            return View(Session["Usuario".Trim()]);
        }

        //Recibe los datos para modificar el usuario, valida si es correcto y de ser asi modifica el registro en la base de datos.
        [HttpPost]
        public ActionResult Perfil([Bind(Include = "Id,Correo,Contrasena")] Usuario user)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(user).State = EntityState.Modified;
                DB.SaveChanges();
                ViewBag.Correcto = "Bien";

                return View(ViewBag.Correcto);
            }
            return View(user);
        }
    }
}