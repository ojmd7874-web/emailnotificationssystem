using EmailNotifications.Application.Interfaces;
using EmailNotifications.Domain.Entities;
using EmailNotifications.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Infrastructure.Repositories
{
    public class TemplateRepository : Repository<Template>, ITemplateRepository
    {
        public TemplateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
