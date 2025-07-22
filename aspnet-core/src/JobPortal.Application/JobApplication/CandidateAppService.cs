using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JobPortal.Authorization;
using JobPortal.EntityFrameworkCore.JobApplication;
using JobPortal.JobApplication.Dtos;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;


namespace JobPortal.JobApplication
{
    [AbpAuthorize(PermissionNames.Pages_Candidates)]

    public class CandidateAppService : ApplicationService, ICandidateAppService
    {
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly IRepository<JobPosition, Guid> _jobPositionRepository;
        private readonly IWebHostEnvironment _env;

        public CandidateAppService(
            IRepository<Candidate, Guid> candidateRepository,
            IRepository<JobPosition, Guid> jobPositionRepository,
            IWebHostEnvironment env)
        {
            _candidateRepository = candidateRepository;
            _jobPositionRepository = jobPositionRepository;
            _env = env;
        }

        public async Task<ListResultDto<JobPositionDto>> GetJobPositionsAsync()
        {
            var jobs = await _jobPositionRepository.GetAllListAsync();
            return new ListResultDto<JobPositionDto>(
                ObjectMapper.Map<List<JobPositionDto>>(jobs)
            );
        }

        public async Task<PagedResultDto<CandidateDto>> GetAllAsync(GetJobPositionInput input)
        {
            // Step 1: Get queryable from repository, including JobPosition relation
            var query =  _candidateRepository.GetAllIncluding(x => x.JobPosition);

            // Step 2: Apply filtering
            if (!input.Keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x =>
                    x.FullName.Contains(input.Keyword) ||
                    x.Email.Contains(input.Keyword));
            }

            // Step 3: Get total count
            var totalCount = await query.CountAsync();

            // Step 4: Apply sorting
            query = query.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "CreationTime DESC" : input.Sorting);

            // Step 5: Apply paging
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);

            // Step 6: Execute query
            var candidates = await query.ToListAsync();

            // Step 7: Map to DTOs
            var candidateDtos = candidates.Select(c => new CandidateDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                ResumePath = c.ResumePath,
                JobPositionId = c.JobPositionId,
                JobPositionTitle = c.JobPosition?.Title
            }).ToList();

            // Step 8: Return paged result
            return new PagedResultDto<CandidateDto>(totalCount, candidateDtos);
        }


        public async Task<CandidateDto> GetAsync(EntityDto<Guid> input)
        {
            var candidate = await _candidateRepository.GetAsync(input.Id);
            var dto = ObjectMapper.Map<CandidateDto>(candidate);
            dto.JobPositionTitle = (await _jobPositionRepository.GetAsync(candidate.JobPositionId)).Title;
            return dto;
        }

        public async Task CreateAsync([FromForm] CreateUpdateCandidateDto input)
        {
            // Check if file is uploaded
            string resumePath = null;

            if (input.ResumeFile != null && input.ResumeFile.Length > 0)
            {
                var ext = Path.GetExtension(input.ResumeFile.FileName).ToLowerInvariant();
                var allowed = new[] { ".pdf", ".docx", ".jpg", ".jpeg", ".png" };

                if (!allowed.Contains(ext))
                    throw new UserFriendlyException("Only .pdf, .docx, .jpg/.jpeg, .png files are allowed");

                var fileName = $"{Guid.NewGuid()}{ext}";
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads/resumes");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.ResumeFile.CopyToAsync(stream);
                }

                resumePath = $"/uploads/resumes/{fileName}";
            }

            var candidate = new Candidate
            {
                FullName = input.FullName,
                Email = input.Email,
                JobPositionId = input.JobPositionId,
                ResumePath = resumePath
            };

            await _candidateRepository.InsertAsync(candidate);
        }

        public async Task UpdateAsync([FromForm] CreateUpdateCandidateDto input)
        {
            // Check if file is uploaded
            string resumePath = null;

            if (input.ResumeFile != null && input.ResumeFile.Length > 0)
            {
                var ext = Path.GetExtension(input.ResumeFile.FileName).ToLowerInvariant();
                var allowed = new[] { ".pdf", ".docx", ".jpg", ".jpeg", ".png" };

                if (!allowed.Contains(ext))
                    throw new UserFriendlyException("Only .pdf, .docx, .jpg/.jpeg, .png files are allowed");

                var fileName = $"{Guid.NewGuid()}{ext}";
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads/resumes");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await input.ResumeFile.CopyToAsync(stream);
                }

                resumePath = $"/uploads/resumes/{fileName}";
            }

            var entity = await _candidateRepository.GetAsync(input.Id);

            ObjectMapper.Map(input, entity); // Map updated fields
            await _candidateRepository.UpdateAsync(entity);

       
        }

        public async Task DeleteAsync(EntityDto<Guid> input)
        {
            await _candidateRepository.DeleteAsync(input.Id);
        }
    }
}
