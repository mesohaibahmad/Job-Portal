using Abp.Authorization.Roles;
using Abp.Authorization;
using AutoMapper;
using JobPortal.Authorization.Roles;
using JobPortal.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.EntityFrameworkCore.JobApplication;

namespace JobPortal.JobApplication.Dtos
{
  
    public class JobPostionMapProfile : Profile
    {
        public JobPostionMapProfile()
        {
            CreateMap<JobPosition, JobPositionDto>();
            CreateMap<CreateUpdateJobPositionDto, JobPosition>();
            CreateMap<GetJobPositionInput, JobPosition>();

           
        }
    }
}
