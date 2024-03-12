using System;
using AkademetreMVC.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AkademetreMVC.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext()
		{
		}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AkademetreInMemory");

        }
    }
}

