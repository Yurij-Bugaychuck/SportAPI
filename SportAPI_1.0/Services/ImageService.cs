using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportAPI.Services
{
    public class ImageService
    {
        public static async Task<string> NewImage(String startPath, IFormFile uploadedFile, int avatarCount)
        {
            if (!Directory.Exists(startPath))
            {
                Directory.CreateDirectory(startPath);
            }
            
            string fileExt = Path.GetExtension(uploadedFile.FileName).ToLower();
            
            string path = $"{startPath}/avatar-{avatarCount}{fileExt}";

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            
            return path;
        }
    }
}
