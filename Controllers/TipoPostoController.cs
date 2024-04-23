using HomeTeamWebSite.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeTeamWebSite.Controllers
{

    public class TipoPostoController : Controller
    {
        private ArsenalDbContext db = new ArsenalDbContext();

        // GET: TipoPosto
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["TicketsPage"] = "You must be logged in to access tickets page.";
                return RedirectToAction("Index", "Home");
            }

            return View(db.TipoPosto.ToList());
        }

        // GET: TipoPosto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPosto tipoPosto = db.TipoPosto.Find(id);
            if (tipoPosto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPosto);
        }

        // GET: TipoPosto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPosto/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoPosto,Titolo,Descrizione,Prezzo")] TipoPosto tipoPosto)
        {
            if (ModelState.IsValid)
            {
                db.TipoPosto.Add(tipoPosto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPosto);
        }

        // GET: TipoPosto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPosto tipoPosto = db.TipoPosto.Find(id);
            if (tipoPosto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPosto);
        }

        // POST: TipoPosto/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoPosto,Titolo,Descrizione,Prezzo")] TipoPosto tipoPosto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPosto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPosto);
        }

        // GET: TipoPosto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPosto tipoPosto = db.TipoPosto.Find(id);
            if (tipoPosto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPosto);
        }

        // POST: TipoPosto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPosto tipoPosto = db.TipoPosto.Find(id);
            db.TipoPosto.Remove(tipoPosto);
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


        //GET: TipoPosto/Select/5

        // POST: TipoPosto/Select
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select(int idPartita, int idTipoPosto)
        {
            // Create a new instance of Biglietti
            Biglietti biglietto = new Biglietti();

            // Set the properties of the Biglietti object
            biglietto.IdPartita = idPartita;
            biglietto.IdTipoPosto = idTipoPosto;

            // Add the Biglietti object to the database
            db.Biglietti.Add(biglietto);
            db.SaveChanges();

            return RedirectToAction("Index", "Biglietti");
        }


        //GET: TipoPosto/Select/5 

        //public ActionResult Select(int? idPartita, int idTipoPosto)
        //{
        //    if (idPartita == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ViewBag.IdPartita = idPartita;
        //    IdTipoPosto = new SelectList(db.TipoPosto, "IdTipoPosto", "Titolo");
        //    return View();
        //}

        // POST: TipoPosto/Select
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Select(int idPartita, int idTipoPosto)
        //{
        //    return RedirectToAction("Create", "Biglietti", new { idPartita = idPartita, idTipoPosto = idTipoPosto });
        //}



    }
}
