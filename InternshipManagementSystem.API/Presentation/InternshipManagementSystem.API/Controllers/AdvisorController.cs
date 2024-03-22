using InternshipManagementSystem.API.Constants;
using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Application.ViewModels.AdvisorViewModels;
using InternshipManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace InternshipManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        readonly private IAdvisorReadRepository _advisorReadRepository;
        readonly private IAdvisorWriteRepository _advisorWriteRepository;
        readonly private IStudentReadRepository _studentReadRepository;
        readonly private IStudentWriteRepository _studentWriteRepository;

        public AdvisorController(IAdvisorReadRepository advisorReadRepository, IAdvisorWriteRepository advisorWriteRepository, IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository)
        {
            _advisorReadRepository = advisorReadRepository;
            _advisorWriteRepository = advisorWriteRepository;
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var advisor = await _advisorReadRepository.GetByIdAsync(id, false);
            return Ok(
               new ResponseModel(true, "Successful", advisor, 200)
               );

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = _advisorReadRepository.GetAll(false);
            return Ok(
            new ResponseModel(true, "Successful", data, 200)
         );
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudentsOfAllAdvisors()
        {
            var data = _advisorReadRepository.GetAll().Include(x => x.Students);
            var students = data.SelectMany(x => x.Students).ToList();
            return Ok(
                           new ResponseModel(true, "Successful", students, 200)
                                   );


        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetStudentsOfAdvisor(string id)
        {
            var data = await _advisorReadRepository.Table.Include(x => x.Students).FirstOrDefaultAsync(x => x.ID == Guid.Parse(id));
            var students = data.Students;
            return Ok(
                                          new ResponseModel(true, "Successful", students, 200)
                                                                            );
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Advisor model)
        {
            Advisor e = await _advisorReadRepository.GetSingleAsync(x => x.TC_NO == model.TC_NO, false);
            if (e is not null)
            {
                var str = e.TC_NO == model.TC_NO ? "Student Number" : "";
                return Ok(new ResponseModel()
                {
                    IsSuccess = false,
                    Message = $"There is a student with same {str} number",
                    Data = null,
                    StatusCode = 400

                });

            }

            Advisor advisor = new()
            {
                Address = model.Address,
                Email = model.Email,
                AdvisorName = model.AdvisorName,
                AdvisorSurname = model.AdviserSurname,
                TC_NO = model.TC_NO,
                DepartmentName = model.DepartmentName,
                ProgramName = model.ProgramName,
                FacultyName = model.FacultyName,
            };



            try
            {
                await _advisorWriteRepository.AddAsync(advisor);
                await _advisorWriteRepository.SaveAsync();
                return Ok(new ResponseModel()
                {
                    IsSuccess = true,
                    Message = "Successful",
                    Data = advisor,
                    StatusCode = 200

                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null,
                    StatusCode = 400

                });
            }


            return Ok(new ResponseModel()
            {
                IsSuccess = false,
                Message = "Some problems",
                Data = null,
                StatusCode = 499

            });

        }
        [HttpPut]
        public async Task<IActionResult> Update(VM_Update_Advisor model)
        {

            var advisor = await _advisorReadRepository.GetByIdAsync(model.AdvisorID);

            if (advisor is null)
            {
                return Ok(new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Advisor not found",
                    Data = null,
                    StatusCode = 400

                });
            }

            advisor.TC_NO = model.TC_NO;
            advisor.Email = model.Email;
            advisor.Address = model.Address;
            advisor.AdvisorName = model.AdvisorName;
            advisor.AdvisorSurname = model.AdviserSurname;
            advisor.DepartmentName = model.DepartmentName;
            advisor.FacultyName = model.FacultyName;
            advisor.ProgramName = model.ProgramName;
            await _advisorWriteRepository.SaveAsync();
            return Ok(
               new ResponseModel(true, "Successful", advisor, 200)
               );
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var advisor = _advisorReadRepository.GetSingleAsync(x => x.ID == id);

            if (advisor is null)
            {
                return Ok(new ResponseModel(false, "Advisor not found", null, 400));
            }
            else
            {
                advisor.Result.Students.Clear();
            }

            if (await _advisorWriteRepository.RemoveAsync(id))
            {
                await _advisorWriteRepository.SaveAsync();

                return Ok(
                    new ResponseModel()
                    {
                        IsSuccess = true,
                        Message = "Successfuly advisor deleted",
                        Data = null,
                        StatusCode = 200
                    }
                    );
            }
            else
            {
                return Ok(new ResponseModel(false, "Delete is not complete possible id value is not correct", null, 400));
            }

        }




    }
}
