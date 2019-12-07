using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FinalWEB.Models;

namespace FinalWEB.Controllers
{
    [Authorize]
    public class SociosController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Socios
        public ActionResult Index()
        {
            return View(db.Socios.ToList());
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Socios foto = new Socios(); 

            using (DBModels db = new DBModels())
            {
                foto = db.Socios.Where(x => x.Id == id).FirstOrDefault();
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Add(Socios image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            image.Foto = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image"), fileName);
            image.ImageFile.SaveAs(fileName);
            using (DBModels db = new DBModels())
            {
                db.Socios.Add(image);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // GET: Socios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Cedula,Foto,Direccion,Telefono,Sexo,Edad,Fecha_Nacimiento,Afiliados,Membresia,Lugar_Trabajo,Direc_Oficina,Tel_Oficina,Estatus,Fecha_Ingreso,Fecha_Salida")] Socios socios)
        {
            if (ModelState.IsValid)
            {
                db.Socios.Add(socios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socios);
        }

        // GET: Socios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: Socios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Cedula,Foto,Direccion,Telefono,Sexo,Edad,Fecha_Nacimiento,Afiliados,Membresia,Lugar_Trabajo,Direc_Oficina,Tel_Oficina,Estatus,Fecha_Ingreso,Fecha_Salida")] Socios socios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socios);
        }

        // GET: Socios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var query = from x in db.Socios where x.Id == id select x;
            Socios socios = db.Socios.Find(id);
            foreach (var item in query)
            {
                item.Estatus = "Inactivo";
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
