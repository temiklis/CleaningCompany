using Microsoft.EntityFrameworkCore;

namespace CleaningCompany.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
