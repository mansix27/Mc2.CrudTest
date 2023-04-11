using Mc2.CrudTest.Application.Common.Interface;
using Mc2.CrudTest.Domain.Common;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Mc2.CrudTest.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        #region Properties
        private readonly DateTime _currentDateTime;

        #endregion
        #region Ctor
        public ApplicationDBContext()
        {
            _currentDateTime = DateTime.Now;
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {
            _currentDateTime = DateTime.Now;

        }
        #endregion

        #region Master
        public DbSet<Customer> Customers { get; set; }
        #endregion

        public async Task<int> SaveChangesAsync()
        {

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                var currentUserEmail = "Initiator";
                entry.Entity.Editor = currentUserEmail; //Get Current UsereID
                entry.Entity.Modified = _currentDateTime;

            }
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            modelBuilder.Entity<Customer>().HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth }).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(b => b.Email).IsUnique();
        }

    }
}
