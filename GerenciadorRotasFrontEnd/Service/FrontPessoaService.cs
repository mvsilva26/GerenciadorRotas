

using GerenciadorRotasFrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class FrontPessoaService
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<List<Pessoa>> GetListaPessoas()
        {
            List<Pessoa> listapessoas = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44386/api/Pessoa");
            string responseBody = await user.Content.ReadAsStringAsync();
            listapessoas = JsonConvert.DeserializeObject<List<Pessoa>>(responseBody);

            return listapessoas;

        }

        public static async Task<Pessoa> GetIdPessoas(string id)
        {
            Pessoa pessoasId = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44386/api/Pessoa/" + id);
            string responseBody = await user.Content.ReadAsStringAsync();
            pessoasId = JsonConvert.DeserializeObject<Pessoa>(responseBody);

            return pessoasId;

        }

        public static async Task<Pessoa> GetIdAlterarStatus(string id)
        {
            Pessoa pessoasId = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44386/api/Pessoa/" + id + "/status");
            string responseBody = await user.Content.ReadAsStringAsync();
            pessoasId = JsonConvert.DeserializeObject<Pessoa>(responseBody);

            return pessoasId;

        }


        public static void CreatePessoa(Pessoa pessoa)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44386/api/Pessoa", pessoa);


        }

        public static void UpdatePessoa(string id, Pessoa pessoa)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PutAsJsonAsync("https://localhost:44386/api/Pessoa/" + id, pessoa);


        }

        public static void DeletePessoa(string id)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.DeleteAsync("https://localhost:44386/api/Pessoa/" + id);


        }


    }
}
