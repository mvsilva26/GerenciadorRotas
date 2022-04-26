using GerenciadorRotas.APICidade.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorRotas.APICidade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {

        private readonly CidadeService _cidadeService;

        public CidadeController(CidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }


        [HttpGet]
        public ActionResult<List<Cidade>> Get() =>
           _cidadeService.Get();



        [HttpGet("{id:length(24)}", Name = "GetCidade")]
        public ActionResult<Cidade> Get(string id)
        {
            var cidade = _cidadeService.Get(id);

            if (cidade == null)
            {
                return NotFound();
            }

            return cidade;
        }

        [HttpPost]
        public async Task<ActionResult<Cidade>> Create(Cidade cidade)
        {

            _cidadeService.Create(cidade);

            return CreatedAtRoute("GetCidade", new { id = cidade.Id.ToString() }, cidade);


        }





        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cidade cidadeIn)
        {
            var cidade = _cidadeService.Get(id);

            if (cidade == null)
            {
                return NotFound();
            }

            _cidadeService.Update(id, cidadeIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cidade = _cidadeService.Get(id);

            if (cidade == null)
            {
                return NotFound();
            }

            _cidadeService.Remove(cidade.Id);

            return NoContent();
        }





    }
}
