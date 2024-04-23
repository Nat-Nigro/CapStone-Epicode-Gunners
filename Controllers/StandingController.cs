using HomeTeamWebSite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace HomeTeamWebSite.Controllers
{
    public class StandingController : Controller
    {
        // GET: Standing
        public async Task<ActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://www.thesportsdb.com/api/v1/json/3/lookuptable.php?l=4328&s=2023-2024";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<StandingResponse>(json);
                    return View(responseObj.Table);
                }
                else
                {
                    return View("Error");
                }
            }
        }
    }
}