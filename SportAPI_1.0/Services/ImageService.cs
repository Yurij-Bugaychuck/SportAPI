using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


namespace SportAPI
{
    public class ImageService
    { 
        
        
        public async Task<string> newImage(String StartPath, IFormFile uploadedFile)
        {
            
            if (!Directory.Exists(StartPath))
            {
                Directory.CreateDirectory(StartPath);
            }

            string _Path = StartPath + "/" + uploadedFile.FileName;



            using (var fileStream = new FileStream(_Path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            
            return Path.GetFullPath(_Path).ToString();
        }
    }
}
