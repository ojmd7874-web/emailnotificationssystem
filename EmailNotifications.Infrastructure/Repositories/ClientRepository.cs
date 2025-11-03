using EmailNotifications.Application.Interfaces;
using EmailNotifications.Domain.Entities;
using EmailNotifications.Infrastructure.Data;
using EmailNotifications.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Client?> GetByNameAsync(string name)
        {
            return await _table.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
