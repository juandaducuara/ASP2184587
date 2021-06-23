using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;
using Rotativa;

namespace ASP_2184587.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {         
                using (var db = new inventarioEntities1())
                {
                    return View(db.compra.ToList());
                }            
        }
        public static string nombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities1())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }
        public static string nombreCliente(int? idCliente)
        {
            using (var db = new inventarioEntities1())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }
        public ActionResult ListarUsuarios()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }
        public ActionResult ListarClientes()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.cliente.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.compra.Add(compra);
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
                compra compraEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(compraEdit);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    compra anteriorCompra = db.compra.Find(compraEdit.id);
                    anteriorCompra.fecha = compraEdit.fecha;
                    anteriorCompra.total = compraEdit.total;
                    anteriorCompra.id_usuario = compraEdit.id_usuario;
                    anteriorCompra.id_cliente = compraEdit.id_cliente;

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
                var findUser = db.compra.Find(id);
                return View(findUser);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findUser = db.compra.Find(id);
                    db.compra.Remove(findUser);
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
        public ActionResult Facturacion()
        {
            var db = new inventarioEntities1();
            var query = from tabCompra in db.compra
                        join tabCliente in db.cliente on tabCompra.id equals tabCliente.id
                        select new Facturacion
                        {
                            nombreCliente = tabCliente.nombre,
                            documentoCliente = tabCliente.documento,
                            fechaCompra = tabCompra.fecha,
                            totalCompra= tabCompra.total,                            
                        };
            return View(query);

        }
        public ActionResult ImprimirFaturacion()
        {
            return new ActionAsPdf("Facturacion") { FileName = "Facturacion.Pdf" };
        }
    }
}