using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JobPortal.EntityFrameworkCore.JobApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication.Dtos
{
    [AutoMapTo(typeof(Candidate))]
    public class CandidateDto : EntityDto<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ResumePath { get; set; }

        public Guid JobPositionId { get; set; }
        public string JobPositionTitle { get; set; }
    }
}
