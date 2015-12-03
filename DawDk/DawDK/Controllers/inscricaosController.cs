using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DawDK;

namespace DawDK.Controllers
{
    public class inscricaosController : Controller
    {
        private exameEntities db = new exameEntities();

        // GET: inscricaos
        public ActionResult Index()
        {
            var inscricaos = db.inscricaos.Include(i => i.evento1).Include(i => i.participante);
            return View(inscricaos.ToList());
        }

        // GET: inscricaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscricao inscricao = db.inscricaos.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }

        // GET: inscricaos/Create
        public ActionResult Create()
        {
            ViewBag.Evento = new SelectList(db.eventos, "Id_evento", "Titulo");
            ViewBag.Participantes = new SelectList(db.participantes, "Id_participantes", "Nome");
            return View();
        }

        // POST: inscricaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_inscricao,Data_Inscricao,Evento,Participantes")] inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                db.inscricaos.Add(inscricao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Evento = new SelectList(db.eventos, "Id_evento", "Titulo", inscricao.Evento);
            ViewBag.Participantes = new SelectList(db.participantes, "Id_participantes", "Nome", inscricao.Participantes);
            return View(inscricao);
        }

        // GET: inscricaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscricao inscricao = db.inscricaos.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Evento = new SelectList(db.eventos, "Id_evento", "Titulo", inscricao.Evento);
            ViewBag.Participantes = new SelectList(db.participantes, "Id_participantes", "Nome", inscricao.Participantes);
            return View(inscricao);
        }

        // POST: inscricaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_inscricao,Data_Inscricao,Evento,Participantes")] inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscricao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Evento = new SelectList(db.eventos, "Id_evento", "Titulo", inscricao.Evento);
            ViewBag.Participantes = new SelectList(db.participantes, "Id_participantes", "Nome", inscricao.Participantes);
            return View(inscricao);
        }

        // GET: inscricaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inscricao inscricao = db.inscricaos.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }

        // POST: inscricaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            inscricao inscricao = db.inscricaos.Find(id);
            db.inscricaos.Remove(inscricao);
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
