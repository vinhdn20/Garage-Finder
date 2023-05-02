using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GarageFinderDBContext : DbContext
    {
        public GarageFinderDBContext(DbContextOptions<GarageFinderDBContext> options) : base(options)
        {

        }
    }
}