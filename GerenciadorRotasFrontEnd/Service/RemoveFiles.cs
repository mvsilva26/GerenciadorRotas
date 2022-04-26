﻿using System.IO;

namespace GerenciadorRotasFrontEnd.Service
{
    public class RemoveFiles
    {

        public static void RemoveFromFolder(string title, string extension, string pathWebRoot)
        {
            string fileName = title + extension;
            string folder = "\\File\\";
            string pathFinal = pathWebRoot + folder + fileName;

            if (File.Exists(pathFinal))
                File.Delete(pathFinal);
        }


    }
}
