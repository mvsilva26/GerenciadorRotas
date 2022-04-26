using GerenciadorRotas.APIEquipe.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIEquipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {

        private readonly EquipeService _equipeService;

        public EquipeController(EquipeService equipeService)
        {
            _equipeService = equipeService;
        }


        [HttpGet]
        public ActionResult<List<Equipe>> Get() =>
           _equipeService.Get();



        [HttpGet("{id:length(24)}", Name = "GetEquipe")]
        public ActionResult<Equipe> Get(string id)
        {
            var equipe = _equipeService.Get(id);

            if (equipe == null)
            {
                return NotFound();
            }

            return equipe;
        }

        [HttpGet("cidade/{id}")]
        public ActionResult<List<Equipe>> GetEquipesCidades(string id) =>
            _equipeService.GetEquipesCidades(id);

        [HttpPost]
        public async Task<ActionResult<Equipe>> Create(Equipe equipe)
        {

            foreach (var pessoa in equipe.Pessoa)
            {
                await PessoaAPIService.UpdateStatus(pessoa.Id);
                pessoa.Disponivel = !pessoa.Disponivel;
            }


            var buscaCidade = SeachCityIdInApiAsync(equipe.Cidade.Id);

            equipe.Cidade = await buscaCidade;

            _equipeService.Create(equipe);

            return CreatedAtRoute("GetEquipe", new { id = equipe.Id.ToString() }, equipe);


        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Equipe equipeIn)
        {
            var equipe = _equipeService.Get(id);

            if (equipe == null)
            {
                return NotFound();
            }

            _equipeService.Update(id, equipeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var equipe = _equipeService.Get(id);

            if (equipe == null)
            {
                return NotFound();
            }

            _equipeService.Remove(equipe.Id);

            return NoContent();
        }

        //[HttpPut("insert/{id}")]
        //public async Task<dynamic> UpdateInsert(string id, Pessoa person)
        //{
        //    var team = await _equipeService.UpdateInsert(id, person);
        //    if (team == null)
        //        return BadRequest("Time não cadastrado, verifique as informações e tente novamento!");
        //    return NoContent();
        //}


        private static async Task<Cidade> SeachCityIdInApiAsync(string id)
        {
            using (var httpClient = new HttpClient())
            {

                HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44354/api/Cidade/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var CityJson = JsonConvert.DeserializeObject<Cidade>(responseBody);
                return CityJson;
            }

        }

    }



}
