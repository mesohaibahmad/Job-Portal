using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JobPortal.Authorization.Users;
using JobPortal.EntityFrameworkCore.JobApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication.Dtos
{
    [AutoMapFrom(typeof(JobPosition))]
    public class CreateUpdateJobPositionDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
