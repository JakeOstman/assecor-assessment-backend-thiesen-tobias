using System.Collections.Generic;
using FavoriteColorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColorApi.Data
{
    public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Person { get; set; }
    }
}
