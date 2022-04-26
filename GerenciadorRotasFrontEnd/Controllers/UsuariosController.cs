
using GerenciadorRotasFrontEnd.Models;
using GerenciadorRotasFrontEnd.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Controllers
{
    
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {

            string user = "Anonymous";
            bool authenticate = false;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = HttpContext.User.Identity.Name;
                authenticate = true;

                if (HttpContext.User.IsInRole("Admin"))
                    ViewBag.Role = "Admin";
                else
                    ViewBag.Role = "User";

            }
            else
            {
                user = "Não Logado";
                authenticate = false;
                ViewBag.Role = "";
            }

            ViewBag.User = user;
            ViewBag.Authenticate = authenticate;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> Login(Usuario usuarioLogin)
        {

            var buscarUsuario = await FrontUsuarioService.GetLogin(usuarioLogin.LoginUser);

            if (buscarUsuario != null)
            {
                if (usuarioLogin.Senha == buscarUsuario.Senha)
                {
                    List<Claim> usuarioClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, buscarUsuario.LoginUser),
                        new Claim("Role", buscarUsuario.Role),
                        new Claim(ClaimTypes.Role, buscarUsuario.Role),
                    };

                    var identificacao = new ClaimsIdentity(usuarioClaims, "Usuario");
                    var usuarioPrincipal = new ClaimsPrincipal(new[] { identificacao });

                    await HttpContext.SignInAsync(usuarioPrincipal);

                    return RedirectToRoute(new { controller = "Equipes", action = "Index" });

                }
            }

            ViewBag.Message = "Usuário ou senha inválidos";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Usuario>> Create(Usuario novoUsuario)
        {

            var inserirUsuario = await FrontUsuarioService.PostUsuario(novoUsuario);

            if (inserirUsuario == null)
                return BadRequest("Não é possível inserir o usuário");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit()
        {
            //if (id == null)
            //    return NotFound();

            //var usuario = await UsuarioServices.GetId(id);
            //if (usuario == null)
            //    return NotFound("Usuário não encontrado");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete()
        {
            //if (id == null)
            //    return NoContent();

            //var usuario = await UsuarioServices.GetId(id);
            //if (usuario == null)
            //    return NotFound("Usuário não encontrado");

            return RedirectToAction(nameof(Index));
        }
    }
}
