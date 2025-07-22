using Abp.AutoMapper;
using JobPortal.EntityFrameworkCore.JobApplication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.JobApplication.Dtos
{
    [AutoMapTo(typeof(Candidate))]
    public class CreateUpdateCandidateDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string FullName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public Guid JobPositionId { get; set; }

        // We’ll use this only for uploads from UI
        public IFormFile ResumeFile { get; set; }
    }
}
