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
    public class EquipesController : Controller
    {
        //private readonly GerenciadorRotasFrontEndContext _context;

        //public EquipesController(GerenciadorRotasFrontEndContext context)
        //{
        //    _context = context;
        //}

        // GET: Equipes
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

            return View(await FrontEquipeService.GetListaEquipes());
        }

        // GET: Equipes/Details/5
        public async Task<IActionResult> Details(string id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var equipe = await FrontEquipeService.GetIdEquipes(id);
            if (equipe == null)
            {
                return NotFound();
            }

           

            return View(equipe);
        }

      
        // GET: Equipes/Create
        public async Task<IActionResult> Create()
        {

            IEnumerable<Pessoa> pegarPessoas = await FrontPessoaService.GetListaPessoas();

            var pessoaDisponiveis = from pessoa in pegarPessoas where pessoa.Disponivel == true select pessoa;


            //var allCidades = await FrontCidadeService.GetListaCidades();


            //ViewBag.PegarCidades = allCidades;


            ViewBag.PegaPessoas = pessoaDisponiveis;



            return View();
        }

        // POST: Equipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cidade")] Equipe equipe)
        {

            List<Pessoa> pessoasSelecionadas = new ();

            if (ModelState.IsValid)
            {
                var pessoasList = Request.Form["txtpessoa"].ToList();

                var cidadesSelecionadas = Request.Form["Cidade"].ToString();

                if (pessoasList.Count == 0)
                {
                    return RedirectToAction(nameof(Create));
                }

                foreach (var pessoaId in pessoasList)
                {
                    var pessoa = await FrontPessoaService.GetIdPessoas(pessoaId);
                    pessoasSelecionadas.Add(pessoa);
                }

                var buscaCidade = await FrontCidadeService.GetIdCidades(cidadesSelecionadas);

                equipe.Pessoa = pessoasSelecionadas;

                equipe.Cidade = buscaCidade;

                await FrontEquipeService.CreateEquipe(equipe);
                return RedirectToAction(nameof(Index));
            }
            return View(equipe);
        }

        // GET: Equipes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            IEnumerable<Pessoa> pegarPessoas = await FrontPessoaService.GetListaPessoas();

            var pessoaDisponiveis = from pessoa in pegarPessoas where pessoa.Disponivel == true select pessoa;


            ViewBag.PegaPessoas = pessoaDisponiveis;


            

            var equipe = await FrontEquipeService.GetIdEquipes(id);

            List<Pessoa> pessoasDosTimes = new();

            if (equipe == null)
            {
                return NotFound();
            }


            foreach (var pessoaId in equipe.Pessoa)
            {
                
                pessoasDosTimes.Add(pessoaId);
            }

            ViewBag.PessoasDosTimes = pessoasDosTimes;

            return View(equipe);
        }

        // POST: Equipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Cidade")] Equipe equipe)
        {
            if (id != equipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var buscaEquipe = await FrontEquipeService.GetIdEquipes(id);
                    FrontEquipeService.UpdateEquipe(id, equipe);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!EquipeExists(equipe.Id))
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
            return View(equipe);
        }

        // GET: Equipes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await FrontEquipeService.GetIdEquipes(id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var equipe = await FrontEquipeService.GetIdEquipes(id);
            FrontEquipeService.DeleteEquipe(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool EquipeExists(string id)
        //{


        //    return _context.Equipe.Any(e => e.Id == id);
        //}
    }
}
