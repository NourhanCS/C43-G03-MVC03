﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtensions = new List<string>() { ".jpg " , ".png " , ".jpeg "};
        private const int FileMaximumSize = 2_097_152;
        public string UploadImage(IFormFile File, string FolderName)
        {
            var fileExtension = Path.GetExtension(File.FileName);

            if (!AllowedExtensions.Contains(fileExtension))
                throw new Exception("Invalid File Extension !!");

            if (File.Length > FileMaximumSize)
                throw new Exception("Invalid File Size , Over Our Page !!");

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files", FolderName);

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FileName = $"{Guid.NewGuid()}_{File.FileName}";

            var FilePath= Path.Combine(FolderPath,FileName);

            //Upload Server

            using var fs = new FileStream(FilePath,FileMode.Create);

            File.CopyTo(fs);

            return FileName;

            
        }

        public bool DeleteImage(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);  
                return true;
            }

            return false;
        }

       
    }
}
