using EmailNotifications.Infrastructure.Data;
using EmailNotifications.SharedLibrary.Entities;
using EmailNotifications.SharedLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id) 
        {
            return await _table.FindAsync(id);
        } 
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
