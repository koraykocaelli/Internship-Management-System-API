using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace InternshipManagementSystem.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipWriteRepository _internshipWriteRepository;
        private readonly IInternshipDocumentReadRepository _internshipDocumentReadRepository;
        private readonly IInternshipDocumentWriteRepository _internshipDocumentWriteRepository;
        private readonly IInternshipApplicationFormWriteRepository _internshipApplicationFormWriteRepository;
        private readonly IInternshipApplicationFormReadRepository _internshipApplicationFormReadRepository;
        private readonly IInternshipBookWriteRepository _internshipBookWriteRepository;
        private readonly IInternshipBookReadRepository _internshipBookReadRepository;

        public FileService(IWebHostEnvironment webHostEnvironment, IInternshipReadRepository internshipReadRepository, IInternshipWriteRepository internshipWriteRepository, IInternshipDocumentReadRepository internshipDocumentReadRepository, IInternshipDocumentWriteRepository internshipDocumentWriteRepository, IInternshipApplicationFormReadRepository internshipApplicationFormReadRepository, IInternshipApplicationFormWriteRepository internshipApplicationFormWriteRepository, IInternshipBookReadRepository internshipBookReadRepository, IInternshipBookWriteRepository internshipBookWriteRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _internshipReadRepository = internshipReadRepository;
            _internshipWriteRepository = internshipWriteRepository;
            _internshipDocumentReadRepository = internshipDocumentReadRepository;
            _internshipDocumentWriteRepository = internshipDocumentWriteRepository;
            _internshipApplicationFormReadRepository = internshipApplicationFormReadRepository;
            _internshipApplicationFormWriteRepository = internshipApplicationFormWriteRepository;
            _internshipBookReadRepository = internshipBookReadRepository;
            _internshipBookWriteRepository = internshipBookWriteRepository;

        }
        private readonly string azureWwwRootPath = "C:\\home\\site\\wwwroot";
        public async Task<bool> CopyFileAsync(string sourcePath, IFormFile formFile)
        {
            try
            {
                await using var fileStream = new FileStream(sourcePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
                {

                    string extension = Path.GetExtension(fileName);
                    string oldname = Path.GetFileNameWithoutExtension(fileName);
                    string newFileName = $"{NameOperation.CharacterRegulator(oldname)}{extension}";

                    if (first)
                    {
                        string oldName = Path.GetFileNameWithoutExtension(fileName);
                        newFileName = $"{NameOperation.CharacterRegulator(oldName)}{extension}";
                    }
                    else
                    {
                        newFileName = fileName;
                        int indexNo1 = newFileName.IndexOf("-");
                        if (indexNo1 == -1)
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                        else
                        {
                            int lastIndex = 0;
                            while (true)
                            {
                                lastIndex = indexNo1;
                                indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                                if (indexNo1 == -1)
                                {
                                    indexNo1 = lastIndex;
                                    break;
                                }
                            }

                            int indexNo2 = newFileName.IndexOf(".");
                            string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
                            if (int.TryParse(fileNo, out int _fileNo))
                            {
                                _fileNo++;
                                newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1).Insert(indexNo1 + 1, _fileNo.ToString());
                            }
                            else
                            {
                                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                            }
                        }
                    }


                    if (File.Exists($"{path}/{newFileName}"))
                    {
                        return await FileRenameAsync(path, newFileName, false);
                    }
                    else
                    {
                        return newFileName;
                    }

                });
            return newFileName;


        }

       



        public async Task<bool> UploadAsync(Guid internShipId, IFormFile file, filetypes @enum)
        {
            if (file == null)
            {
                return false;
            }
            var path = GetPath(internShipId).Result;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (path == null)
            {
                return false;
            }
            var fileNewName = await FileRenameAsync(path, file.FileName);
            var filePath = $"{path}\\{fileNewName}";
            var result = await CopyFileAsync(filePath, file);
            var initialize = false;

            try
            {
                if (@enum == filetypes.InternshipBook)
                {
                    InternshipBook document = new InternshipBook()
                    {
                        InternshipID = internShipId,
                        FileName = fileNewName,
                        FilePath = filePath,
                        FileType = Path.GetExtension(fileNewName)
                    };
                    initialize = true;
                    if (initialize)
                    {
                        var sonucWrite = await _internshipBookWriteRepository.AddAsync(document);
                        await _internshipBookWriteRepository.SaveAsync();
                        var intern = await _internshipReadRepository.GetSingleAsync(e => e.ID == internShipId);
                        intern.InternshipBookID = document.ID;
                        await _internshipWriteRepository.SaveAsync();   
                    }

                }
                else if (@enum == filetypes.InternshipApplicationForm)
                {
                    InternshipApplicationForm document = new InternshipApplicationForm()
                    {
                        InternshipID = internShipId,
                        FileName = fileNewName,
                        FilePath = filePath,
                        FileType = Path.GetExtension(fileNewName)
                    };
                    initialize = true;
                    if (initialize)
                    {
                       var  sonucWrite = await _internshipApplicationFormWriteRepository.AddAsync(document);
                        await _internshipApplicationFormWriteRepository.SaveAsync();
                        var intern = await _internshipReadRepository.GetSingleAsync(e => e.ID == internShipId);
                        intern.InternshipApplicationFormID = document.ID;
                        await _internshipWriteRepository.SaveAsync();
                    }
                }
                else
                {

                    throw new Exception("Invalid file type");
                }
                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
            return default;
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> GetPath(Guid internShipId)
        {
            await _internshipBookWriteRepository.SaveAsync();
            var check = await _internshipReadRepository.GetSingleAsync(e => e.ID == internShipId);
            if (check == null)
            {
                return null;
            }
            string basePath = "";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                basePath = _webHostEnvironment.WebRootPath;
            }
            else { 
            basePath = azureWwwRootPath;
            
            }
            var path = Path.Combine(basePath, $"Students\\{check.StudentID.ToString()}\\{check.ID.ToString()}");

            return path;
        }

    }

}

