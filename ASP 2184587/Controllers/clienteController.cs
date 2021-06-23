using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    [Authorize]
    public class clienteController : Controller
    {
        // GET: cliente
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.cliente.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.cliente.Add(cliente);
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
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var findUser = db.cliente.Find(id);
                return View(findUser);

            }
        }        
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente findUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente editUser)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente cliente = db.cliente.Find(editUser.id);
                    cliente.nombre = editUser.nombre;
                    cliente.documento = editUser.documento;
                    cliente.email = editUser.email;                   

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
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findUser = db.cliente.Find(id);
                    db.cliente.Remove(findUser);
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
        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            string filePath = string.Empty;
            if (fileForm != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(fileForm.FileName);
                string extension = Path.GetExtension(fileForm.FileName);
                fileForm.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newCliente = new cliente
                        {
                            nombre = row.Split(';')[0],
                            documento = row.Split(';')[1],
                            email= row.Split(';')[2],                            
                        };
                        using (var db = new inventarioEntities1())
                        {
                            db.cliente.Add(newCliente);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}