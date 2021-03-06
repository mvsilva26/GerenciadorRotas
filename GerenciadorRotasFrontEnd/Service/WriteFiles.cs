using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GerenciadorRotasFrontEnd.Service
{
    public class WriteFiles
    {

        public static async Task<bool> WriteFileInFolder(IFormFile file, string pathWebRoot)
        {
            bool isSaveSuccess = false;
            string fileName;

            try
            {
                //var extension = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
                fileName = "Plan.xlsx";

                string folder = "\\File\\";
                string pathFinal = pathWebRoot + folder + fileName;

                using (var stream = new FileStream(pathFinal, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                isSaveSuccess = true;

                return isSaveSuccess;
            }
            catch (Exception exception)
            {
                return isSaveSuccess;
            }
        }



    }
}
