using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using JobPortal.Authorization.Roles;
using JobPortal.Authorization.Users;
using JobPortal.MultiTenancy;
using JobPortal.EntityFrameworkCore.JobApplication;

namespace JobPortal.EntityFrameworkCore
{
    public class JobPortalDbContext : AbpZeroDbContext<Tenant, Role, User, JobPortalDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public JobPortalDbContext(DbContextOptions<JobPortalDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobPosition>(b =>
            {
                b.ToTable("AppJobPositions");
                b.Property(x => x.Title).IsRequired().HasMaxLength(128);
                b.Property(x => x.Description).HasMaxLength(1024);
                b.Property(x => x.IsActive);
            });

            modelBuilder.Entity<Candidate>(b =>
            {
                b.ToTable("AppCandidates");
                b.Property(x => x.FullName).IsRequired().HasMaxLength(128);
                b.Property(x => x.Email).IsRequired().HasMaxLength(256);
                b.Property(x => x.ResumePath).HasMaxLength(512);

                b.HasOne(x => x.JobPosition)
                 .WithMany(x => x.Candidates)
                 .HasForeignKey(x => x.JobPositionId);
            });
        }

    }
}
