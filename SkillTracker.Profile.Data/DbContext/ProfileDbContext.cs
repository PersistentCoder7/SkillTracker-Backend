using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Data.DbContext
{
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

            //Seed some data that can be used for testing
            modelBuilder.Entity<Domain.Models.Profile>(p =>
                {

                    p.HasData(
                        new Domain.Models.Profile()
                        {
                            AssociateId = "1",
                            AddedOn = DateTime.Now,
                            Email = "1@cts.com",
                            Mobile = "810586001",
                            Name = "Prabhu"
                        });

                    p.OwnsMany(s => s.Skills).HasData(
                        new Skill() { ProfileAssociateId = "1", SkillId = 1, IsTechnical = true, Name = "HTML", Proficiency = 5 }

                    );
                }

            );
            //modelBuilder.Entity<Domain.Models.Profile>().

        }

        public static async Task SeedInitalDataSync(DbContextOptions options)
        {
            using var context = new ProfileDbContext(options);
            var _ = await context.Database.EnsureDeletedAsync();

            if (await context.Database.EnsureCreatedAsync())
            {
                context.Profiles?.AddRange(SeedData());
                await context.SaveChangesAsync();
            }
        }

        private static List<Domain.Models.Profile> SeedData()
        {
            return new List<Domain.Models.Profile>()
            {
                new Domain.Models.Profile()
                {
                    AssociateId = "1",
                    AddedOn = DateTime.Now,
                    Email = "1@cts.com",
                    Mobile = "810586001",
                    Name = "Prabhu",
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            ProfileAssociateId = "1", SkillId = 1, IsTechnical = true, Name = "HTML", Proficiency = 5
                        }
                    }
                }
            };
        }
    }
}
