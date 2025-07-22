using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.EntityFrameworkCore.JobApplication
{
    public class Candidate : FullAuditedEntity<Guid>
    {
        [Required]
        public Guid JobPositionId { get; set; }

        [Required]
        [StringLength(128)]
        public string FullName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(512)]
        public string ResumePath { get; set; }

        public virtual JobPosition JobPosition { get; set; }
    }
}
