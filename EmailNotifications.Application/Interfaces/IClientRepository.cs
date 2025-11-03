using EmailNotifications.Domain.Entities;
using EmailNotifications.SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client?> GetByNameAsync(string name);
    }
}
