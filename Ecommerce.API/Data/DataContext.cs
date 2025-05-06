using Microsoft.EntityFrameworkCore;
using Ecommerce.Shared.Entities;

namespace Ecommerce.API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Country>

    }
}
