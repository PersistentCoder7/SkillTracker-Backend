using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Data.DbContext;

public class ProfileDbContext: Microsoft.EntityFrameworkCore.DbContext
{
    public ProfileDbContext( DbContextOptions options) : base(options)
    {

    }
    public DbSet<Domain.Models.Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Models.Profile>(p =>
        {
            p.ToContainer("SkillTrackerContainer");
                
            p.HasKey(x => x.AssociateId);
            p.OwnsMany(s => s.Skills,d=> d.WithOwner().HasForeignKey("ProfileAssociateId"));
        });
    }
}