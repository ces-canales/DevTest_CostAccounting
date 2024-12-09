using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataContext
{
    public class CADbContext : DbContext
    {
        public CADbContext(DbContextOptions<CADbContext> options) 
            : base(options) {}
        public DbSet<Client> Clients { get; set; }

        public DbSet<Investment> Investments { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<Company> Companies { get; set; }

    }
}
