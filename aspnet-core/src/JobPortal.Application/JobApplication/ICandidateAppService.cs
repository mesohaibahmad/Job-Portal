using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JobPortal.JobApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication
{
    public interface ICandidateAppService : IApplicationService
    {
        Task<ListResultDto<JobPositionDto>> GetJobPositionsAsync();

        Task<PagedResultDto<CandidateDto>> GetAllAsync(GetJobPositionInput input);
        Task<CandidateDto> GetAsync(EntityDto<Guid> input);
        Task CreateAsync(CreateUpdateCandidateDto input);
        Task DeleteAsync(EntityDto<Guid> input);
    }
}
