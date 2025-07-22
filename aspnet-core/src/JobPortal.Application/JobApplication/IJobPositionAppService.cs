using Abp.Application.Services.Dto;
using Abp.Application.Services;
using JobPortal.JobApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication
{
    public interface IJobPositionAppService : IAsyncCrudAppService<
       JobPositionDto,
       Guid,
       PagedAndSortedResultRequestDto,
       CreateUpdateJobPositionDto,
       CreateUpdateJobPositionDto>
    {
        Task<PagedResultDto<JobPositionDto>> GetAllJobsAsync(GetJobPositionInput input);

    }
}
