using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Book> Book { get; set; }

        public DbSet<WebApp.Models.Panier> Panier { get; set; }

        public DbSet<WebApp.Models.User> User { get; set; }
    }
}
