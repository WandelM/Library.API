using AutoMapper;
using Library.API.DbContexts;
using Library.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    /// <summary>
    /// Base repository for other repositories
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T:class
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds entity to db
        /// </summary>
        /// <param name="value">Entity object</param>
        public void Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _context.Set<T>().Add(value);
        }

        /// <summary>
        /// Adds list of entities to db
        /// </summary>
        /// <param name="entitiesList">Collection of entities</param>
        public void AddRange(IEnumerable<T> entitiesList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all of entities
        /// </summary>
        /// <returns>List of entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();

            return entities;
        }

        public async Task<PaginatedList<T>> GetPaginatedListAsync(int pageSize, int pageNumber)
        {
            var entities = _context.Set<T>() as IQueryable<T>;

            return await PaginatedList<T>.GetPaginatedList(entities, pageSize, pageNumber);
        }

        /// <summary>
        /// Gets one entity with specified id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Entity with this id or null if it was not found</returns>
        public async Task<T> GetAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            return entity;
        }

        /// <summary>
        /// Removes entity from db
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Removes entity with specified id
        /// </summary>
        /// <param name="entityId">Id of entity</param>
        public void Remove(Guid entityId)
        {
            var entity = _context.Set<T>().Find(entityId);

            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Removes collection of entities
        /// </summary>
        /// <param name="entitiesList">Collection to be removed</param>
        public void RemoveRange(IEnumerable<T> entitiesList)
        {
            if (entitiesList == null)
            {
                throw new ArgumentNullException(nameof(entitiesList));
            }

            _context.RemoveRange(entitiesList);
        }

        /// <summary>
        /// Saves changes in db
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
