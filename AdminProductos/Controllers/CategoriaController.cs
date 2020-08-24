using AdminProductos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminProductos.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        DBproductosEntities DB = new DBproductosEntities();

        //se traen todas las categorias como metodo principal
        public ActionResult ListaCategorias()
        {
            return View(DB.Categoria);
        }

        //Muestra formulario para crear vista
        public ActionResult CrearCategoria()
        {

            return View();
        }

        //Recibe información del formulario y el BINd se encarga de validar que el formarto sea correcto y este completo
        [HttpPost]
        public ActionResult CrearCategoria([Bind(Include = "Id,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                DB.Categoria.Add(categoria);
                DB.SaveChanges();

                //Una vez guardado vuelve a la lista de categorias
                return RedirectToAction("ListaCategorias", "Categoria");
            }

            return View(categoria);
        }

        //presenta formulario con la categoria que se desea modificar
        public ActionResult EditarCategoria(int? id)
        {
            // aqui se busca en la base de datos el id para editar
            Categoria categoria = DB.Categoria.Find(id);


            return View(categoria);
        }

        //Confirma que el formulario se envio de forma correcta con todos sus datos
        [HttpPost]
        public ActionResult EditarCategoria([Bind(Include = "Id,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                // Entry lo que hace es validar el estado de las prodpiedas y de haberse mificado se envia a la base de datos
                DB.Entry(categoria).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ListaCategorias");
            }
            return View(categoria);
        }

        //Metodo get que devuelve el elemento a eliminar
        public ActionResult EliminarCategoria(int? Id)
        {
            var categoria = DB.Categoria.Find(Id);

            return View(categoria);
        }

        //Una vez seleccionado se elimina en la base de datos con el metodo remove
        [HttpPost]
        public ActionResult EliminarCategoria(int Id)
        {
            Categoria categoria = DB.Categoria.Find(Id);
            DB.Categoria.Remove(categoria);
            DB.SaveChanges();

            return RedirectToAction("ListaCategorias");
        }
    }
}