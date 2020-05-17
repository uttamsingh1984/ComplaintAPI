using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ComplaintApi.Models;

namespace ComplantApi.DB
{
    public class ComplaintDbContext: IdentityDbContext<AppUser>{
        public ComplaintDbContext(DbContextOptions complaintDbContext):base(complaintDbContext){}

        public DbSet<User> Users {get;set;}
        public DbSet<Complaint> Complaints {get;set;}

        public DbSet<Organization> Organizations {get;set;}

        public DbSet<Country> Countries {get;set;}

        public DbSet<Category> Categories {get;set;}

        
    }    
}
