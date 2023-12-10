using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file,string FolderName)
        {
            string FolderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",FolderName);

            string FileName=$"{Guid.NewGuid()}{file.FileName}";

            string FilePath=Path.Combine(FolderPath,FileName);

            using var fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fs);

            return FileName;
        }
        public static void DeleteImage(string file, string FolderName)
        {
            if (file != null && FolderName is not null) 
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName,file);
                //string FilePath = Path.Combine(FolderPath, file);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            
        }

    }
}
