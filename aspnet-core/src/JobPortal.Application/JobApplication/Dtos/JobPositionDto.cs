using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JobPortal.Authorization.Users;
using JobPortal.EntityFrameworkCore.JobApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication.Dtos
{
    [AutoMapTo(typeof(JobPosition))]
    public class JobPositionDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
