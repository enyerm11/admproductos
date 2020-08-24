using AdminProductos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        DBproductosEntities DB = new DBproductosEntities();

        //Inmediatamente envia lista de todos los productos en la base de datos
        public ActionResult ListaProductos()
        {

            return View(DB.Producto.ToList());
        }
        //Metodo credo con el fin de establecer la busqueda en la base de productos mediante el nombre
        [HttpPost]
        public ActionResult ListaProductos(string cadena)
        {
            List<Producto> Busqueda = DB.Producto.Where(x => x.Nombre == cadena).ToList();

            return View(Busqueda);
       
        }

        //Devuelve formulario para crear producto
        public ActionResult CrearProducto()
        {
            return View();
        }

        // metodo encargado de validar que los datos del formulario se hayan enviado de forma correcta y guardar en la base de datos
        [HttpPost]
        public ActionResult CrearProducto([Bind(Include = "Id,Nombre,Precio,Id_Categoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                DB.Producto.Add(producto);
                DB.SaveChanges();
                return RedirectToAction("Index", "Producto");
            }
            return View(producto);
        }

        // Devuelve formulario con el producto selecionado
        public ActionResult EditarProducto(int? Id)
        {
            var Producto = DB.Producto.Find(Id);

            return View(Producto);
        }

        //Recibe datos del formulario, valida y registra en la base de datos de ser correctos
        [HttpPost]
        public ActionResult EditarProducto([Bind(Include = "Id,Nombre,Precio,Id_Categoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(producto).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ListaProductos");
            }
            return View(producto);
        }

        //Devuelve cuestionante con el prodcto para validar si desea eliminar
        public ActionResult EliminarProducto(int? id)
        {
            var Producto = DB.Producto.Find(id);

            return View(Producto);
        }

        //Una vez que se acepte eliminar va a la base de datos y elimina el registro con remove()
        [HttpPost]
        public ActionResult EliminarProducto(int Id)
        {
            Producto producto = DB.Producto.Find(Id);
            DB.Producto.Remove(producto);
            DB.SaveChanges();

            return RedirectToAction("ListaProductos");
        }

    }
}