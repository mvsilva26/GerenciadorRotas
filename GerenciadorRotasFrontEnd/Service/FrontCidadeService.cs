using GerenciadorRotasFrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class FrontCidadeService
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<List<Cidade>> GetListaCidades()
        {
            List<Cidade> listacidades = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44354/api/Cidade");
            string responseBody = await user.Content.ReadAsStringAsync();
            listacidades = JsonConvert.DeserializeObject<List<Cidade>>(responseBody);

            return listacidades;

        }

        public static async Task<Cidade> GetIdCidades(string id)
        {
            Cidade cidadesId = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44354/api/Cidade/" + id);
            string responseBody = await user.Content.ReadAsStringAsync();
            cidadesId = JsonConvert.DeserializeObject<Cidade>(responseBody);

            return cidadesId;

        }

        public static void CreateCidade(Cidade cidade)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44354/api/Cidade", cidade);


        }

        public static void UpdateCidade(string id, Cidade cidade)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PutAsJsonAsync("https://localhost:44354/api/Cidade/" + id, cidade);


        }

        public static void DeleteCidade(string id)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.DeleteAsync("https://localhost:44354/api/Cidade/" + id);


        }



    }
}
