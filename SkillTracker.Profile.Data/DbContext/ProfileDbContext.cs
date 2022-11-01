using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkillTracker.Profile.Data.DbContext
{
    public class ProfileDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public ProfileDbContext( DbContextOptions options) : base(options)
        {

        }
        public DbSet<Domain.Models.Profile> Profiles { get; set; }
    }
}
