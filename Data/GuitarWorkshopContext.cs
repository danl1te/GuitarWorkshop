using GuitarWorkshopApp.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace GuitarWorkshopApp.Data
{
    public class GuitarWorkshopContext : DbContext
    {
        public DbSet<Guitars> Guitars { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Consumables> Consumables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=sql7.freesqldatabase.com;" +
                                   "Database=sql7782969;" +
                                   "User=sql7782969;" +
                                   "Password=dwcLMrb4eV;" +
                                   "Port=3306;" +
                                   "ConnectionTimeout=30;" +
                                   "AllowPublicKeyRetrieval=true;" +
                                   "SslMode=None;" +
                                   "CharSet=utf8mb4;";
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 21)),
                options => options.EnableRetryOnFailure()
            );
        }
    }
}