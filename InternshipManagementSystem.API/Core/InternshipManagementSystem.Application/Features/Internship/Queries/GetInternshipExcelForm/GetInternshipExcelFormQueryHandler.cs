using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.ViewModels;
using InternshipManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Features.Internship
{
    public class GetInternshipExcelFormQueryHandler : IRequestHandler<GetInternshipExcelFormQueryRequest, GetInternshipExcelFormQueryResponse>
    {
        private readonly IInternshipReadRepository _internshipReadRepository;
        private readonly IInternshipApplicationExcelFormReadRepository _internshipApplicationExcelFormReadRepository;

        public GetInternshipExcelFormQueryHandler(IInternshipReadRepository internshipReadRepository, IInternshipApplicationExcelFormReadRepository internshipApplicationExcelFormReadRepository)
        {
            _internshipReadRepository = internshipReadRepository;
            _internshipApplicationExcelFormReadRepository = internshipApplicationExcelFormReadRepository;
        }

        public async Task<GetInternshipExcelFormQueryResponse> Handle(GetInternshipExcelFormQueryRequest request, CancellationToken cancellationToken)
        {
            var internship = await _internshipReadRepository.GetByIdAsync(request.InternshipId);
            if (internship == null)
            {

                return new GetInternshipExcelFormQueryResponse
                {
                    Response = new ResponseModel
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Internship not found",
                        StatusCode = 404
                    }
                };
            }
            try
            {
                var excelForm = internship.InternshipApplicationExelFormID == null ? null : 
                    await _internshipApplicationExcelFormReadRepository.GetByIdAsync(internship.InternshipApplicationExelFormID.Value);

                return new GetInternshipExcelFormQueryResponse
                {
                    Response = new ResponseModel
                    {
                        Data=excelForm,
                        IsSuccess = true,
                        Message = "Internship excel form found",
                        StatusCode = 200
                    }
                };

            }
            catch (Exception exexption)
            {
                return new GetInternshipExcelFormQueryResponse
                {
                    Response = new ResponseModel
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = exexption.Message,
                        StatusCode = 500
                    }
                };

            }
        }
    } }
