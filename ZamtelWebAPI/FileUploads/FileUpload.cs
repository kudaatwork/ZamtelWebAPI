using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

using System.IO;

namespace ZamtelWebAPI.FilesUpload
{
    public class FileUpload
    {
        public async Task<string> Post(List<IFormFile> formFile)
        {           
            try
            { 
                
            }
            catch (Exception exception)
            {
                return "Failed";
            }

            return "Failed";

            // return "Tasvikako kuserver kwacho";
        }

        public string Post(string imageBase64)
        {
            try
            {
                //byte[] imageBytes = Convert.FromBase64String(imageBase64);

                //Image image;

                //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                //ms.Write(imageBytes, 0, imageBytes.Length);

                //image = Image.FromStream(ms, true);

                //string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Uploads/");
                ////string imageName = "test.png";

                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}

                //var date = DateTime.Now.ToString("ddMMyyHHmmss");

                //var fileName = "/image_" + date + ".png";

                //string filePath = folderPath + fileName;
                ////File.WriteAllBytes(filePath, image);

                //image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                //return "/Content/Uploads/" + fileName;
            }
            catch (Exception ex)
            {

                throw;
            }

            return "";
        }
    }
    
}
