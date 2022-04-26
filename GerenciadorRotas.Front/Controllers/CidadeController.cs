using GerenciadorRotas.Front.Models;
using GerenciadorRotas.Front.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace GerenciadorRotas.Front.Controllers
{
    public class CidadeController : Controller
    {

        //private readonly CidadeService _cidadeService;
        //IWebHostEnvironment _appEnvironment;
        //public CidadeController(CidadeService cidadeService, IWebHostEnvironment env)
        //{
        //    _cidadeService = cidadeService;
        //    _appEnvironment = env;
        //}

        //public IActionResult Index()
        //{
        //    return View(_cidadeService.Get());
        //}


        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Cidades/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Cidade cidade)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    _context.Add(cidade);
        //    //    await _context.SaveChangesAsync();
        //    //    return RedirectToAction(nameof(Index));
        //    //}

        //    _cidadeService.Create(cidade);
        //    return RedirectToAction(nameof(Index));
        //}

        //// GET: Cidades/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cidade = _cidadeService.Get(id);

        //    if (cidade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cidade);
        //}

        //// POST: Cidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    _cidadeService.Remove(id);


        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cidade = _cidadeService.Get(id);

        //    if (cidade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cidade);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, Cidade cidade)
        //{
        //    if (id != cidade.Id)
        //    {
        //        return NotFound();
        //    }

        //    _cidadeService.Update(id, cidade);


        //        return RedirectToAction(nameof(Index));
        //}



    }
}
