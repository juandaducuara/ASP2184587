using ASP_2184587.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_2184587.Controllers
{
    public class productoCompraController : Controller
    {
        // GET: productoCompra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto_compra.ToList());
            }
        }        
        public static string nombreProducto(int? idProducto)
        {
            using (var db = new inventarioEntities1())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }
        public ActionResult ListarCompra()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.compra.ToList());
            }
        }
        public ActionResult ListarProducto()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra productoCompra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.producto_compra.Add(productoCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                producto_compra productocompraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(productocompraEdit);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra producto_Compra)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto_compra productoCompra = db.producto_compra.Find(producto_Compra.id);
                    productoCompra.id_compra = producto_Compra.id_compra;
                    productoCompra.id_producto = producto_Compra.id_producto;
                    productoCompra.cantidad = producto_Compra.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var findUser = db.producto_compra.Find(id);
                return View(findUser);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findUser = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
    }
}