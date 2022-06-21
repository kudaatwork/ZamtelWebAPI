using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost, Route("Upload")]        
        public async Task<string> Upload([FromForm] UploadFile uploadFile)
        {
            List<IFormFile> formFiles = new List<IFormFile>();

            if (uploadFile.Signature.Length > 0 && uploadFile.Portrait.Length > 0 && uploadFile.IdFront.Length > 0 && uploadFile.IdBack.Length > 0)
            {
                formFiles.Add(uploadFile.Signature);
                formFiles.Add(uploadFile.Portrait);
                formFiles.Add(uploadFile.IdBack);
                formFiles.Add(uploadFile.IdFront);
                
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Uploads\\");
                    }

                    foreach (var item in formFiles)
                    {
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Uploads\\" + item.FileName))
                        {
                            item.CopyTo(fileStream);
                            fileStream.Flush();                          
                        }
                    }

                    return "Success";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Upload Failed";
            }
        }
    }
}
