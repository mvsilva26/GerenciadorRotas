using GerenciadorRotas.APIPessoa.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIPessoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly PessoaService _pessoaService;

        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }


        [HttpGet]
        public ActionResult<List<Pessoa>> Get() =>
           _pessoaService.Get();



        [HttpGet("{id:length(24)}", Name = "GetPessoa")]
        public ActionResult<Pessoa> Get(string id)
        {
            var pessoa = _pessoaService.Get(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }


        [HttpPut("status/{id:length(24)}")]
        public IActionResult UpdateActive(string id)
        {
            var seachPerson = _pessoaService.AtualizaStatus(id);
            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");
            return NoContent();
        }





        [HttpPost]
        public async Task<ActionResult<Pessoa>> Create(Pessoa pessoa)
        {

                _pessoaService.Create(pessoa);

                return CreatedAtRoute("GetPessoa", new { id = pessoa.Id.ToString() }, pessoa);


        }

  



        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Pessoa pessoaIn)
        {
            var pessoa = _pessoaService.Get(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            _pessoaService.Update(id, pessoaIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var pessoa = _pessoaService.Get(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            _pessoaService.Remove(pessoa.Id);

            return NoContent();
        }


        [HttpPut("{id:length(24)}/status")]
        public IActionResult AtualizaStatus(string id)
        {
            var pessoa = _pessoaService.AtualizaStatus(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return NoContent();
        }



    }
}
