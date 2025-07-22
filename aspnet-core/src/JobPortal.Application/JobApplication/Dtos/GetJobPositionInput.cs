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
    [AutoMapFrom(typeof(JobPosition))]
    public class GetJobPositionInput : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
