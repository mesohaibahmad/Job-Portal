using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using JobPortal.JobApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using JobPortal.Authorization;
using JobPortal.EntityFrameworkCore.JobApplication;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
 // for WhereIf and PageBy
using System.Linq.Dynamic.Core; // for dynamic OrderBy


namespace JobPortal.JobApplication
{
    [AbpAuthorize(PermissionNames.Pages_JobPositions)]

    public class JobPositionAppService : AsyncCrudAppService<
         JobPosition,
         JobPositionDto,
         Guid,
         PagedAndSortedResultRequestDto,
         CreateUpdateJobPositionDto,
         CreateUpdateJobPositionDto>,
         IJobPositionAppService
    {
        private readonly IRepository<JobPosition, Guid> _jobPositionRepository;
        public JobPositionAppService(IRepository<JobPosition, Guid> jobPositionRepository)
            : base(jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }
      

        public override async Task<JobPositionDto> CreateAsync(CreateUpdateJobPositionDto input)
        {
            CheckCreatePermission();
          
                var jobPosition = ObjectMapper.Map<JobPosition>(input);

          
                await _jobPositionRepository.InsertAsync(jobPosition);
                return MapToEntityDto(jobPosition);
          

         
        }

        public override Task<JobPositionDto> UpdateAsync(CreateUpdateJobPositionDto input) => base.UpdateAsync(input);


        public override Task DeleteAsync(EntityDto<Guid> input) => base.DeleteAsync(input);



        public async Task<PagedResultDto<JobPositionDto>> GetAllJobsAsync(GetJobPositionInput input)
        {
           
                // Step 1: Get a queryable from the repository
                var query = _jobPositionRepository.GetAll();

                // Step 2: Apply filtering
                if (!input.Keyword.IsNullOrWhiteSpace())
                {
                    query = query.Where(x => x.Title.Contains(input.Keyword) || x.Description.Contains(input.Keyword));
                }

                // Step 3: Get total count (before paging)
                var totalCount = await query.CountAsync();

                // Step 4: Apply sorting
                query = query.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "CreationTime DESC" : input.Sorting);

                // Step 5: Apply paging
                query = query.Skip(input.SkipCount).Take(input.MaxResultCount);

                // Step 6: Execute query
                var items = await query.ToListAsync();

                // Step 7: Map to DTOs
                var itemDtos = ObjectMapper.Map<List<JobPositionDto>>(items);

                // Step 8: Return paged result
                return new PagedResultDto<JobPositionDto>(totalCount, itemDtos);
         
        }

    }

}