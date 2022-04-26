using GerenciadorRotasFrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class FrontEquipeService
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<List<Equipe>> GetListaEquipes()
        {
            List<Equipe> listaequipes = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44372/api/Equipe");
            string responseBody = await user.Content.ReadAsStringAsync();
            listaequipes = JsonConvert.DeserializeObject<List<Equipe>>(responseBody);

            return listaequipes;

        }

        public static async Task<Equipe> GetIdEquipes(string id)
        {
            Equipe equipesId = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44372/api/Equipe/" + id);
            string responseBody = await user.Content.ReadAsStringAsync();
            equipesId = JsonConvert.DeserializeObject<Equipe>(responseBody);

            return equipesId;

        }


        public static async Task<List<Equipe>> GetEquipesCidadesId(string id)
        {
            List<Equipe> equipesId = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44372/api/Equipe/cidade/" + id);
            string responseBody = await user.Content.ReadAsStringAsync();
            equipesId = JsonConvert.DeserializeObject<List<Equipe>>(responseBody);

            return equipesId;

        }


        public static async Task CreateEquipe(Equipe equipe)
        {

            HttpClient ApiConnection = new HttpClient();

           await ApiConnection.PostAsJsonAsync("https://localhost:44372/api/Equipe", equipe);


        }

        public static void UpdateEquipe(string id, Equipe equipe)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PutAsJsonAsync("https://localhost:44372/api/Equipe/" + id, equipe);


        }

        public static void DeleteEquipe(string id)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.DeleteAsync("https://localhost:44372/api/Equipe/" + id);


        }



    }
}
