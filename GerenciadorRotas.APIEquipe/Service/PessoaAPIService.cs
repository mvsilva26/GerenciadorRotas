using Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIEquipe.Service
{
    public static class PessoaAPIService
    {

        //public static async Task<Pessoa> UpdatePessoaStatus(string id)
        //{

        //    HttpClient ApiConnection = new HttpClient();

        //    await ApiConnection.PutAsJsonAsync("https://localhost:44386/api/Pessoa/" + id + "/status", null);

        //}

        public static async Task UpdateStatus(string id)
        {
            HttpClient httpClient = new();

            try
            {
                if (httpClient.BaseAddress == null) httpClient.BaseAddress = new Uri("https://localhost:44386/api/Pessoa/");

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PutAsync($"{id}/status", null);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }



    }
}
