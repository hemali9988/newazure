using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCoreAPI.Models;
using System.Threading.Tasks;

namespace DotNetCoreAPI.Infrastructure
{
    public class EventDbContext:DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options):base(options)
        {

        }

        public DbSet<EventInfo> Events { get; set; }
    }
}
