using DAL.Model.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class ApplicationDbContext : IdentityDbContext<Tbl_User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseSqlServer("Server =.; Database = SellBoard; User Id = pe2131; Password = 2131; MultipleActiveResultSets = true");
            optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database = SellBoard; User Id = sa; Password = 12345678; MultipleActiveResultSets = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Tbl_User> tbl_Users { get; set; }
        public DbSet<Tbl_ApiSetting> tbl_ApiSettings { get; set; }
    }
}
