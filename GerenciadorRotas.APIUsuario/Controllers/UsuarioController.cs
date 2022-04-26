using GerenciadorRotas.APIUsuario.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorRotas.APIUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        public ActionResult<List<Usuario>> Get() =>
           _usuarioService.Get();



        [HttpGet("{id:length(24)}", Name = "GetUsuario")]
        public ActionResult<Usuario> Get(string id)
        {
            var usuario = _usuarioService.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet]
        [Route("login")]
        public ActionResult<Usuario> GetUserName(string loginUser)
        {
            var usuario = _usuarioService.GetUserName(loginUser);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }


        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {

            _usuarioService.Create(usuario);

            return CreatedAtRoute("GetUsuario", new { id = usuario.Id.ToString() }, usuario);


        }





        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario usuarioIn)
        {
            var usuario = _usuarioService.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioService.Update(id, usuarioIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var usuario = _usuarioService.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioService.Remove(usuario.Id);

            return NoContent();
        }





    }
}
