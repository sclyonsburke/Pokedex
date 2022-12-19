using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient client;

        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["PokedexBaseUrl"]);
        }

        public ActionResult Index()
        {
            var dex = this.GetList(0);
            this.GetTypes(dex);
            return View(dex);
        }

        public ActionResult DexDetails(string id)
        {
            var pokemon = this.GetDetails(id);
            return View(pokemon);
        }

        public PokedexData GetList(int offset, string search = "", bool favorite = false, string type = "")
        {
            var queryString = "pokemon/?limit=16&offset=" + offset;
            if(search != string.Empty)
            {
                queryString += "&search=" + search;
            }

            if(favorite)
            {
                queryString += "&isFavorite=true";
            }

            if(type != string.Empty)
            {
                queryString += "&type=" + type;
            }

            var response = client.GetAsync(queryString).Result;
            return this.mapData(response);
        }

        public ActionResult RefreshList(int offset = 0, string search = "", bool favorite = false, string type = "")
        {
            var pokeList = GetList(offset, search, favorite, type);
            return PartialView("_DexGrid", pokeList);
        }

        public Pokemon GetDetails(string id)
        {
            var response = client.GetAsync("pokemon/" + id).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(data);
            pokemon.SortEvolutions();
            return pokemon;
        }

        public void ChangeFavorite(string id, bool favorite)
        {
            var response = client.PostAsync("pokemon/" + id + (favorite ? "/favorite" : "/unfavorite"), new StringContent(id)).Result;
        }

        private void GetTypes(PokedexData dex)
        {
            var response = client.GetAsync("pokemon-types").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            dex.Types = JsonConvert.DeserializeObject<IEnumerable<string>>(data).ToList();
        }

        private PokedexData mapData(HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(data);
            var list = json["items"];
            var dex = new PokedexData();
            dex.Count = json["count"].Value<int>();
            foreach(var entry in list)
            {
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(entry.ToString());
                dex.Pokemon.Add(pokemon);
            }
            return dex;
        }
    }
}