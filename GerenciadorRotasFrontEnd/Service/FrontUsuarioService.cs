using GerenciadorRotasFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class FrontUsuarioService
    {
        
        private static readonly string _baseUri = "https://localhost:44319/api/";

        public FrontUsuarioService() {     }


        public static async Task<List<Usuario>> Get()
        {
            var usuarioJson = new List<Usuario>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Usuario");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        usuarioJson = JsonConvert.DeserializeObject<List<Usuario>>(responseBody);
                    }
                    return usuarioJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> GetId(string id)
        {
            var usuarioJson = new Usuario();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    HttpResponseMessage response = await client.GetAsync("Usuario/" + id);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        usuarioJson = JsonConvert.DeserializeObject<Usuario>(responseBody);
                    }
                    return usuarioJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> GetLogin(string login)
        {
            var usuarioJson = new Usuario();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);

                    
                    HttpResponseMessage response = await client.GetAsync("Usuario/login?loginUser=" + login);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        usuarioJson = JsonConvert.DeserializeObject<Usuario>(responseBody);
                    }
                    return usuarioJson;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> PostUsuario(Usuario novoUsuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
                    var content = new StringContent(usuarioJson, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("Usuario/", content);
                    if (result.IsSuccessStatusCode)
                        return novoUsuario;
                    else
                        novoUsuario = null;
                    return novoUsuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Usuario> PutUsuario(string id, Usuario editarUsuario)
        {
            var usuario = new Usuario();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var jsonUsuario = JsonConvert.SerializeObject(editarUsuario);
                    var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");
                    var result = await client.PutAsync($"Usuario/{editarUsuario.Id}", content);
                    if (result.IsSuccessStatusCode)
                        return editarUsuario;
                    else
                        editarUsuario = null;
                    return editarUsuario;
                }
            }
            catch (HttpRequestException)
            {
                usuario = null;
                return usuario;
            }
        }

        public static async Task<Usuario> DeleteUsuario(string id, Usuario usuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseUri);
                    var result = await client.DeleteAsync("Usuario/" + id);
                    if (result.IsSuccessStatusCode)
                        return usuario;
                    else
                        usuario = null;
                    return usuario;
                }
            }
            catch (HttpRequestException)
            {
                usuario = null;
                return usuario;
            }
        }

        HttpClient ApiConnection = new HttpClient();
        public static async Task<List<Usuario>> GetListaUsuario()
        {
            List<Usuario> listausuarios = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44319/api/Usuario");
            string responseBody = await user.Content.ReadAsStringAsync();
            listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(responseBody);

            return listausuarios;

        }




        public static async Task<Usuario> GetUsuarioName(string loginUser)
        {

            Usuario usuario = new();

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44319/api/Usuario/login?loginUser=" + loginUser);
            string responseBody = await user.Content.ReadAsStringAsync();
            usuario = JsonConvert.DeserializeObject<Usuario>(responseBody);

            return usuario;



        }

        public static void CreateUsuario(Usuario usuario)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44319/api/Usuario", usuario);
        }

        public static void UpdateUsuario(string id, Usuario usuario)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PutAsJsonAsync("https://localhost:44319/api/Usuario/" + id, usuario);

        }

        public static void DeleteUsuario(string id)
        {

            HttpClient ApiConnection = new HttpClient();

            ApiConnection.DeleteAsync("https://localhost:44319/api/Usuario/" + id);

        }


    }
}
