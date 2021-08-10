using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankingWebApp.Models;

namespace BankingWebApp.Data
{
    public class BankingWebAppContext : DbContext
    {
        public BankingWebAppContext (DbContextOptions<BankingWebAppContext> options)
            : base(options)
        {
        }

       // public DbSet<BankingWebApp.Models.User> User { get; set; }
        public DbSet<BankingWebApp.Models.Datee> Datee { get; set; }
       // public DbSet<BankingWebApp.Models.FixedDeposit> FixedDeposit { get; set; }

        //public DbSet<BankingWebApp.Models.Transaction> Transaction { get; set; }


        public DbSet<BankingWebApp.Models.AccountUser> AccountUser { get; set; }
       // public DbSet<BankingWebApp.Models.FixedDeposit> FixedDeposit { get; set; }

        //public DbSet<BankingWebApp.Models.Transaction> Transaction { get; set; }


        public DbSet<BankingWebApp.Models.FD> FD { get; set; }
       // public DbSet<BankingWebApp.Models.FixedDeposit> FixedDeposit { get; set; }

        //public DbSet<BankingWebApp.Models.Transaction> Transaction { get; set; }


        public DbSet<BankingWebApp.Models.Tr> Tr { get; set; }
    }
}
