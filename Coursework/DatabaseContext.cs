namespace Coursework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Coursework.Entities;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=CourseDatabase")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Estimate> Estimates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
