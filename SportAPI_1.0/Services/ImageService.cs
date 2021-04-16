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
        
        
        public async Task<string> newImage(String StartPath, IFormFile uploadedFile, int avatarCount)
        {
            
            if (!Directory.Exists(StartPath))
            {
                Directory.CreateDirectory(StartPath);
            }
            string FileExt = System.IO.Path.GetExtension(uploadedFile.FileName).ToLower();
            string _Path = StartPath + "/avatar-" + avatarCount.ToString() + FileExt;



            using (var fileStream = new FileStream(_Path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            
            return _Path.ToString();
        }
    }
}
