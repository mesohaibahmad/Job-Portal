using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.EntityFrameworkCore.JobApplication
{
    public class JobPosition : FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public JobPosition()
        {
            Candidates = new List<Candidate>();
        }
    }
}
