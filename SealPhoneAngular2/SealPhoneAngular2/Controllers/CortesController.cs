using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SealPhoneAngular2.Models;
using Pagination;

namespace SealPhoneAngular2.Controllers
{
    public class CortesController : Controller
    {
        private DBComercialEntities db = new DBComercialEntities();

        // GET: CortesProgramados
        public ActionResult Index(int? page)
        {
            var pager = new Pagination.Pager(db.CortesProgramados.Count(), page);
            var viewModel = new IndexViewModel
            {
                Items = db.CortesProgramados.OrderBy(p => p.Fecha)
                                    .Skip((pager.CurrentPage - 1) * pager.PageSize)
                                    .Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return View(viewModel);
        }

        // GET: CortesProgramados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CortesProgramados CortesProgramados = db.CortesProgramados.Find(id);
            if (CortesProgramados == null)
            {
                return HttpNotFound();
            }
            return View(CortesProgramados);
        }

        // GET: CortesProgramados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CortesProgramados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Fecha,HoraInicio,HoraFinalizacion,Descripcion,Ubicacion")] CortesProgramados CortesProgramados)
        {
            if (ModelState.IsValid)
            {
                UserAD userad = (UserAD)Session["user"];
                CortesProgramados.Usuario = userad.DisplayName;
                db.CortesProgramados.Add(CortesProgramados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CortesProgramados);
        }

        // GET: CortesProgramados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CortesProgramados CortesProgramados = db.CortesProgramados.Find(id);
            if (CortesProgramados == null)
            {
                return HttpNotFound();
            }
            return View(CortesProgramados);
        }

        // POST: CortesProgramados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Fecha,HoraInicio,HoraFinalizacion,Descripcion,Ubicacion")] CortesProgramados CortesProgramados)
        {
            if (ModelState.IsValid)
            {
                UserAD userad = (UserAD)Session["user"];
                CortesProgramados.Usuario = userad.DisplayName;
                db.Entry(CortesProgramados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CortesProgramados);
        }

        // GET: CortesProgramados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CortesProgramados CortesProgramados = db.CortesProgramados.Find(id);
            if (CortesProgramados == null)
            {
                return HttpNotFound();
            }
            return View(CortesProgramados);
        }

        // POST: CortesProgramados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CortesProgramados CortesProgramados = db.CortesProgramados.Find(id);
            db.CortesProgramados.Remove(CortesProgramados);
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
