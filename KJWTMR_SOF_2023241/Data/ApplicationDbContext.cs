using KJWTMR_SOF_2023241.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KJWTMR_SOF_2023241.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Alcohol> Alcohols { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}