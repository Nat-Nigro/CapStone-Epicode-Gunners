using HomeTeamWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HomeTeamWebSite.Controllers
{
    public class ProdottiController : Controller
    {
        private ArsenalDbContext db = new ArsenalDbContext();

        // GET: Prodotti
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }

        // GET: Prodotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        [Authorize(Roles = "Admin")]
        // GET: Prodotti/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotto,NomeProdotto,Descrizione,Prezzo,Immagine, Immagine2, Immagine3")] Prodotti prodotti, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                List<string> uploadedFiles = new List<string>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/imgProdotti"), fileName);
                        System.Diagnostics.Debug.WriteLine("Percorso di salvataggio: " + path);
                        if (!Directory.Exists(Server.MapPath("~/Content/imgProdotti/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/imgProdotti"));
                        }
                        file.SaveAs(path);
                        uploadedFiles.Add(fileName);  // Add file name to the list
                    }
                }

                if (uploadedFiles.Count > 0)
                {
                    prodotti.Immagine = uploadedFiles.Count > 0 ? uploadedFiles.ElementAtOrDefault(0) : "/Content/imgProdotti/default.jpg";
                    prodotti.Immagine2 = uploadedFiles.Count > 1 ? uploadedFiles.ElementAtOrDefault(1) : "/Content/imgProdotti/default.jpg";
                    prodotti.Immagine3 = uploadedFiles.Count > 2 ? uploadedFiles.ElementAtOrDefault(2) : "/Content/imgProdotti/default.jpg";
                }
                else
                {
                    prodotti.Immagine = "/Content/imgProdotti/default.jpg";
                    prodotti.Immagine2 = "/Content/imgProdotti/default.jpg";
                    prodotti.Immagine3 = "/Content/imgProdotti/default.jpg";
                }

                if (ModelState.IsValid)
                {
                    db.Prodotti.Add(prodotti);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }

            return View(prodotti);
        }


        // GET: Prodotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotto,NomeProdotto,Descrizione,Prezzo,Immagine")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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


        // Action per aggiungere un prodotto al carrello
        public ActionResult AddToCart(int id, int Quantita)
        {

            if (!User.Identity.IsAuthenticated)
            {
                TempData["AddCartFailed"] = "You must be logged in to add a product to the cart";
                return RedirectToAction("Index", "Home");

            }

            var prodotto = db.Prodotti.Find(id);

            if (prodotto != null)
            {
                var cart = Session["cart"] as List<Prodotti> ?? new List<Prodotti>();
                prodotto.Quantita = Quantita;
                if (cart.Any(p => p.IdProdotto == id))
                {
                    var prodottoInCart = cart.First(p => p.IdProdotto == id);
                    prodottoInCart.Quantita += Quantita;
                }
                else
                {
                    cart.Add(prodotto);
                    Session["cart"] = cart;
                    TempData["AddCart"] = "Product added to the cart";
                }
            }
            return RedirectToAction("Index");
        }

    }
}
