using InternshipManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Services
{
    public interface IFileService
    {
        //Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files, string StudentID, string InternshipID);

        Task<bool> UploadAsync(Guid internShipId, IFormFile file, filetypes @enum);

        Task<bool> CopyFileAsync(string sourcePath, IFormFile formFile);

        Task<bool> DeleteFileAsync(string path);

        Task<string> GetPath(Guid id);
    }
    public enum filetypes
    {
        InternshipApplicationForm = 1,
        InternshipBook = 2
    }


}
