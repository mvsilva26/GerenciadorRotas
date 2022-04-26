using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorRotasFrontEnd.Data;
using GerenciadorRotasFrontEnd.Models;
using GerenciadorRotasFrontEnd.Service;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorRotasFrontEnd.Controllers
{
    [Authorize]
    public class PessoaController : Controller
    {
        //private readonly GerenciadorRotasFrontEndContext _context;

        //public PessoaController(GerenciadorRotasFrontEndContext context)
        //{
        //    _context = context;
        //}

        // GET: Pessoa
        public async Task<IActionResult> Index()
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

            return View(await FrontPessoaService.GetListaPessoas());
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await FrontPessoaService.GetIdPessoas(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Disponivel")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                FrontPessoaService.CreatePessoa(pessoa);
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await FrontPessoaService.GetIdPessoas(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var buscaPessoa = await FrontPessoaService.GetIdPessoas(id);

                    FrontPessoaService.UpdatePessoa(id, pessoa);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!PessoaExists(pessoa.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await FrontPessoaService.GetIdPessoas(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pessoa = await FrontPessoaService.GetIdPessoas(id);
            FrontPessoaService.DeletePessoa(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool PessoaExists(string id)
        //{
        //    return _context.Pessoa.Any(e => e.Id == id);
        //}
    }
}
