using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace  Mc2.CrudTest.Application.Common.Interface
{
    public interface IApplicationDBContext
    {
        DbSet<Customer> Customers { get; set; }
     
        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
