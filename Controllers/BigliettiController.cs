using HomeTeamWebSite.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeTeamWebSite.Controllers
{

    public class BigliettiController : Controller
    {
        private ArsenalDbContext db = new ArsenalDbContext();

        // GET: Biglietti
        public ActionResult Index()
        {
            var bigliettiNonAcquistati = db.Biglietti.Where(b => b.IsAcquistato == false).Include(b => b.Partite).Include(b => b.TipoPosto);

            if (!bigliettiNonAcquistati.Any())
            {
                return View("Index", "Parite");
            }

            return View(bigliettiNonAcquistati.ToList());
        }

        // GET: Biglietti/Create
        public ActionResult Create()
        {
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "SquadraCasa");
            ViewBag.IdTipoPosto = new SelectList(db.TipoPosto, "IdTipoPosto", "Titolo");
            return View();
        }

        // POST: Biglietti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBiglietti,IdPartita,IdTipoPosto,QuantitaDisponibile,CodiceBiglietto")] Biglietti biglietti)
        {
            if (ModelState.IsValid)
            {
                db.Biglietti.Add(biglietti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "SquadraCasa", biglietti.IdPartita);
            ViewBag.IdTipoPosto = new SelectList(db.TipoPosto, "IdTipoPosto", "Titolo", biglietti.IdTipoPosto);
            return View(biglietti);
        }



        // GET: Biglietti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biglietti biglietti = db.Biglietti.Find(id);
            if (biglietti == null)
            {
                return HttpNotFound();
            }
            return View(biglietti);
        }

        // POST: Biglietti/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Biglietti biglietti = db.Biglietti.Find(id);
        //    db.Biglietti.Remove(biglietti);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biglietti biglietti = db.Biglietti.Find(id);

            var dettaglioVenditaRecords = db.DettaglioVendita.Where(d => d.IdBiglietti == id);
            db.DettaglioVendita.RemoveRange(dettaglioVenditaRecords);

            db.Biglietti.Remove(biglietti);
            db.SaveChanges();
            return RedirectToAction("Index", "Partite");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult CreateTickets()
        {
            return View();
        }

        [HttpPost]

        public ActionResult CreateTickets(int IdPartita, int IdTipoPosto)
        {
            Biglietti biglietto = new Biglietti();

            biglietto.IdPartita = IdPartita;
            biglietto.IdTipoPosto = IdTipoPosto;

            db.Biglietti.Add(biglietto);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // Action per la gestione quantita e push su DB DettaglioVendita

        [HttpPost]
        public ActionResult PushToDettaglioVendita(int? id, int quantita)
        {
            ArsenalDbContext db = new ArsenalDbContext();

            var biglietto = db.Biglietti.FirstOrDefault(b => b.IdBiglietti == id);
            var userId = db.Utenti.FirstOrDefault(u => u.Username == User.Identity.Name).IdUtente;

            Ordini ordine = new Ordini();

            ordine.IdUtente = userId;
            ordine.DataOrdine = DateTime.Now;
            db.Ordini.Add(ordine);
            db.SaveChanges();


            if (biglietto != null && biglietto.TipoPosto != null)
            {
                DettaglioVendita dettaglioVendita = new DettaglioVendita();

                dettaglioVendita.IdBiglietti = biglietto.IdBiglietti;
                dettaglioVendita.QuantitaBiglietti = quantita;
                dettaglioVendita.PrezzoUnitario = biglietto.TipoPosto.Prezzo;
                dettaglioVendita.IdOrdine = ordine.IdOrdine;

                db.DettaglioVendita.Add(dettaglioVendita);
                biglietto.IsAcquistato = true;
                db.SaveChanges();
            }

            TempData["TicketBought"] = "Thank you, the ticket has been purchased successfully.";
            return RedirectToAction("Home", "Home");
        }

    }
}

