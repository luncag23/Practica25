using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practica25.Domain.Entities;
using System.Security.Cryptography;

namespace Practica25.Infrastructure.Data
{

     public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
     {
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
          {
          }

          public DbSet<Product> Products { get; set; }

     }
}
