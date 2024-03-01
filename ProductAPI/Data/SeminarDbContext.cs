using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeminarAPI.Data.Dto;
using SeminarAPI.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarAPI.Data
{
    /// <summary>
    /// SeminarDbContext
    /// </summary>
    public class SeminarDbContext : DbContext
    {
        /// <summary>
        /// SeminarDbContext
        /// </summary>
        /// <param name="options"></param>
        public SeminarDbContext(DbContextOptions<SeminarDbContext> options): base(options)
        {

        }

        /// <summary>
        /// </summary>
        public DbSet<DailyTasks> DailyTasks { get; set; }
        /// <summary>
        /// </summary>
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<Ranking> Ranking { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        /// <summary>
        /// </summary>
        public DbSet<ChallengeTasks> ChallengeTasks { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default collation for the entire database
            modelBuilder.UseCollation("Vietnamese_CI_AS");
        }
    }
}
