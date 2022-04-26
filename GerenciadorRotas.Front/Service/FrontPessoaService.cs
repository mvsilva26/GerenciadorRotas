using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GerenciadorRotas.Front.Service
{
    public class FrontPessoaService
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<List<Pessoa>> GetListaPessoas()
        {
            List<Pessoa> listapessoas = new ();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44386/api/Pessoa");
            string responseBody = await user.Content.ReadAsStringAsync();
            listapessoas = JsonConvert.DeserializeObject<List<Pessoa>>(responseBody);

            return listapessoas;



        }



    }
}
