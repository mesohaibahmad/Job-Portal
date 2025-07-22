using AutoMapper;
using JobPortal.EntityFrameworkCore.JobApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication.Dtos
{
  
    public class CandidateMapProfile : Profile
    {
        public CandidateMapProfile()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CreateUpdateCandidateDto, Candidate>();
            CreateMap<GetJobPositionInput, JobPosition>();


        }
    }
}
