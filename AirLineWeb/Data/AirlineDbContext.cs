using AirLineWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AirLineWeb.Data
{
    public class AirlineDbContext:DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options):base(options)
        {
            
        }
        public DbSet<WebHookSubscription> WebHookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}
