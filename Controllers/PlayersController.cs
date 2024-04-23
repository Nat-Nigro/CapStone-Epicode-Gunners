using HomeTeamWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeTeamWebSite.Controllers
{
    public class PlayersController : Controller
    {
        public async Task<ActionResult> Index(string searchString = "")
        {
            using (HttpClient client = new HttpClient())
            {

                string apiUrl = "https://www.thesportsdb.com/api/v1/json/3/searchplayers.php?t=Arsenal";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<PlayerResponse>(json);

                    // Filtra i giocatori se searchString non è vuoto
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        // Applica ToLower() per un confronto case-insensitive
                        var searchStringLower = searchString.ToLower();
                        responseObj.player = responseObj.player.Where(p => p.strPlayer != null && p.strPlayer.ToLower().Contains(searchStringLower)).ToList();
                    }

                    return View(responseObj.player);
                }
                else
                {
                    return View("Error");
                }
            }
        }

    }

}


