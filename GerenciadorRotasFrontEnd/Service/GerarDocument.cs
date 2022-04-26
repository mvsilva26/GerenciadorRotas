using GerenciadorRotasFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class GerarDocument
    {

        public static async Task<string> CreateDocumento(List<Equipe> equipe, List<string> checarOpcao, List<List<string>> rotas, string service, Cidade cidade, string fileDownl)
        {
            var rotasQuant = rotas.Count;
            var todasColunas = rotas[0];

            var serviceColumn = rotas[0].FindIndex(column => column == "SERVIÇO" || column == "serviço");
            var cidadeColuna = rotas[0].FindIndex(column => column == "CIDADE" || column == "cidade");
            var cepColumn = rotas[0].FindIndex(column => column == "CEP" || column == "cep");
            for (int i = 0; i < rotasQuant; i++)
            {
                rotas.Remove(rotas.Find(route => route[cidadeColuna].ToUpper() != cidade.Nome.ToUpper()));
                //routes.Remove(routes.Find(route => route[serviceColumn].ToUpper() != serviceSelect.ToUpper()));
            }
            var divisionTeam = rotas.Count / equipe.Count;
            var restDivision = rotas.Count % equipe.Count;
            var index = 0;


            var caminhoFile = $"{fileDownl}//files";

            if (!Directory.Exists(caminhoFile))
                Directory.CreateDirectory(caminhoFile);


            var arquivoNome = $"rotas-{service}-{DateTime.Now:dd-MM-yyyy}.docx";

            var criarFile = $"{caminhoFile}//{arquivoNome}";

            using (FileStream fileStream = new(criarFile, FileMode.Create))
            {
                await using (StreamWriter writer = new(fileStream, Encoding.UTF8))
                {
                    writer.WriteLine($"{service} - {DateTime.Now:dd/MM/yyyy}\t {cidade.Nome}\n\n");


                    foreach (var item in equipe)
                    {

                        writer.WriteLine("Equipe: " + item.Nome + "\nRotas:\n");

                        for (int i = 0; i < divisionTeam; i++)
                        {
                            if (i == 0 && restDivision > 0)
                                divisionTeam++;
                            if (i == 0)
                                restDivision--;
                            foreach (var check in checarOpcao)
                            {
                                writer.WriteLine($"{todasColunas[int.Parse(check)]}: {rotas[i + index][int.Parse(check)]}");
                            }
                            if ((i + 1) >= divisionTeam)
                            {
                                index += 1 + i;
                            }
                            writer.WriteLine("\t");
                        }

                        if(restDivision >= 0)                     
                            divisionTeam--;

                        writer.WriteLine("--------------------------------------------------");

                    }

                    writer.Close();


                }

                fileStream.Close();
            }

            return arquivoNome;

        }

    }
}
