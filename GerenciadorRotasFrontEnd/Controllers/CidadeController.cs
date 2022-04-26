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

    public class CidadeController : Controller
    {
        //private readonly GerenciadorRotasFrontEndContext _context;

        //public CidadeController(GerenciadorRotasFrontEndContext context)
        //{
        //    _context = context;
        //}

        // GET: Cidade
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


            return View(await FrontCidadeService.GetListaCidades());
        }

        // GET: Cidade/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await FrontCidadeService.GetIdCidades(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Cidade/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Estado")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                FrontCidadeService.CreateCidade(cidade);
                return RedirectToAction(nameof(Index));
            }
            return View(cidade);
        }

        // GET: Cidade/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await FrontCidadeService.GetIdCidades(id);
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        // POST: Cidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Estado")] Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var buscaPessoa = await FrontCidadeService.GetIdCidades(id);
                    FrontCidadeService.UpdateCidade(id, cidade);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CidadeExists(cidade.Id))
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
            return View(cidade);
        }

        // GET: Cidade/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await FrontCidadeService.GetIdCidades(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cidade = await FrontCidadeService.GetIdCidades(id);
            FrontCidadeService.DeleteCidade(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool CidadeExists(string id)
        //{
        //    return _context.Cidade.Any(e => e.Id == id);
        //}
    }
}
