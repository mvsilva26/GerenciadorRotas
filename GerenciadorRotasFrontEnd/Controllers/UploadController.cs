using GerenciadorRotasFrontEnd.Models;
using GerenciadorRotasFrontEnd.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {


        private static IWebHostEnvironment _hostEnvironment;
        private static string downloadFile;
        public static List<List<string>> routes = new();
        public static List<string> headers = new();
        public static IEnumerable<string> serviceList;
        public static string service;
        public static string city;

        IWebHostEnvironment _appEnvironment;

        public UploadController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }




        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LeituraArquivoExcel(IFormFile file)
        {
            //service = Request.Form["serviceName"].ToString();
            //city = Request.Form["cityName"].ToString();
            int cepColumn = 0;
            int serviceColumn = 0;
            bool check = false;
            List<string> header = new();
            List<string> listService = new();
            List<List<string>> content = new();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage fileexcel = new(file.OpenReadStream());
            ExcelWorksheet worksheet = fileexcel.Workbook.Worksheets.FirstOrDefault();
            var totalColumn = worksheet.Dimension.End.Column;
            var totalRow = worksheet.Dimension.End.Row;
            for (int column = 1; column < totalColumn; column++)
            {
                header.Add(worksheet.Cells[1, column].Value.ToString());
                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                    cepColumn = column - 1;
                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("SERVIÇO"))
                    serviceColumn = column;
            }
            for (int row = 2; row < totalRow; row++)
            {
                for (int line = serviceColumn; line <= serviceColumn; line++)
                {
                    listService.Add(worksheet.Cells[row, serviceColumn].Value?.ToString() ?? null);
                }
            }
            worksheet.Cells[2, 1, totalRow, totalColumn].Sort(cepColumn, false);
            for (int rows = 1; rows < totalRow; rows++)
            {
                List<string> contentLine = new();
                check = false;
                for (int columns = 1; columns < totalColumn; columns++)
                {
                    var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                    contentLine.Add(conteudo);
                    check = true;
                }
                if (check)
                    content.Add(contentLine);
            }
            var removeRepeatService = listService;
            var listaSemDuplicidade = removeRepeatService.Distinct();
            headers = header;
            routes = content;
            serviceList = listaSemDuplicidade;
            return RedirectToAction(nameof(SelecionarCidadeEServico));
        }



        public async Task<IActionResult> SelecionarCidadeEServico()
        {

            var procuraCidade = FrontCidadeService.GetListaCidades();

            ViewBag.TodasCidades =  await procuraCidade;

            ViewBag.PegarServicoExcel = serviceList;

            return View();
        }

        public async Task<IActionResult> SelecionarInformacoesCabecalhos()
        {

            city = Request.Form["cidadeNome"].ToString();
            service = Request.Form["servicoNome"].ToString();

            var buscaEquipes = await FrontEquipeService.GetEquipesCidadesId(city);

            ViewBag.EquipesCidades = buscaEquipes;

            ViewBag.CabecalhoExcel = headers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GerarDoc()
        {
            List<Equipe> equipes = new();

            var selecionarEquipe = Request.Form["ChecarEquipe"].ToList();
            var selecionarCabecalho = Request.Form["ChecarCabecalho"].ToList();

            foreach(var equip in selecionarEquipe)
            {
                var procuraTime = await FrontEquipeService.GetIdEquipes(equip);
                equipes.Add(procuraTime);
            }


            var procuraCidade = await FrontCidadeService.GetIdCidades(city);

            var retornoDoc = await GerarDocument.CreateDocumento(equipes, selecionarCabecalho, routes, service, procuraCidade, _hostEnvironment.WebRootPath);

            downloadFile = $"{_hostEnvironment.WebRootPath}//files//{retornoDoc}";



            return View();

        }

        public FileContentResult RealizaDown()
        {

            var arquivoDown = downloadFile.Split("//").ToList();
            var arq = System.IO.File.ReadAllBytes(downloadFile);
            return File(arq, "application/octet-stream", arquivoDown.Last().ToString());

        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendFile(IFormFile file)
        //{
        //    if (ReadFile.IsValid("Plan", ".xlsx", _appEnvironment.WebRootPath))
        //        RemoveFiles.RemoveFromFolder("Plan", ".xlsx", _appEnvironment.WebRootPath);

        //    var pathFile = Path.GetTempFileName();
        //    string pathWebRoot = _appEnvironment.WebRootPath;

        //    if (CheckExcelFile.IsExcel(file))
        //    {
        //        if (!await WriteFiles.WriteFileInFolder(file, pathWebRoot))
        //            return BadRequest(new { message = "Houve um erro na gravação do arquivo. Por favor, tente novamente." });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Apenas arquivos com extensão .xls ou .xlsx" });
        //    }

        //    ViewData["Resultado"] = $"Um arquivo foi enviado ao servidor, com tamanho total de {file.Length} bytes!";

        //    ReadFile.ReOrderExcel("Plan", ".xlsx", _appEnvironment.WebRootPath);

        //    return View(ViewData);
        //}



    }
}
