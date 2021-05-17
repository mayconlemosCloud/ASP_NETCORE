

using META.DOMAIN;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace META.INFRA
{
    public class Context : DbContext
    {
        private IDbConnection _connection;

     

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Emissoras> Emissoras { get; set; }
        public DbSet<Audiencia> Audiencia { get; set; }

  

    }
}
