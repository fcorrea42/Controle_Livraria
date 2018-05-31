using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Controle_Livraria.Models;

namespace Controle_Livraria.Controllers
{
    public class HomeController : Controller
    {
        private EstoqueEntities db = new EstoqueEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query()
        {
            return View(db.Livros.OrderBy(db => db.NomeLivro).ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Livro _livro)
        {
            if (ModelState.IsValid)
            {
                    db.Livros.Add(_livro);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Livro cadastrado com sucesso.";
            }
            return View(_livro);
        }

        public ActionResult Update(int? IdLivro)
        {
            if (IdLivro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro _livro = db.Livros.Find(IdLivro);
            if (_livro == null)
            {
                return HttpNotFound();
            }
            return View(_livro);
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateConfirmed(Livro _livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(_livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Query");
            }
            return View(_livro);
        }

        public ActionResult Delete(int? IdLivro)
        {
            if (IdLivro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro _livro = db.Livros.Find(IdLivro);
            if (_livro == null)
            {
                return HttpNotFound();
            }
            return View(_livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int IdLivro)
        {
            if (ModelState.IsValid)
            {
                Livro _livro = db.Livros.Find(IdLivro);
                db.Livros.Remove(_livro);
                db.SaveChanges();
                return RedirectToAction("Query");
            }
            return RedirectToAction("Query");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descrição da aplicação";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Informações de contato";

            return View();
        }
    }
}