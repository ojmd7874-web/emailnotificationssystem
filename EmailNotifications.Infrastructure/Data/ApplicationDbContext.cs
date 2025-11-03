using EmailNotifications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Template> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Client 1", Email = "client1@example.com" },
                new Client { Id = 2, Name = "Client 2", Email = "client2@example.com" },
                new Client { Id = 3, Name = "Client 3", Email = "client3@example.com" },
                new Client { Id = 4, Name = "Client 4", Email = "client4@example.com" },
                new Client { Id = 5, Name = "Client 5", Email = "client5@example.com" }
            );

            modelBuilder.Entity<Template>().HasData(
                new Template
                {
                    Id = 1,
                    Name = "Template 1",
                    EmailSubject = "Subject Template 1",
                    IsHtmlContent = true,
                    EmailContent = @"<!DOCTYPE html>
                                    <html>
                                    <body>
                                        <h2>Hello</h2>
                                        <p>This is a notification from our system.</p>
                                        <p>Promotional data from Template 1:</p>
                                        <p>{{TemplateContent}}</p>
                                        <p><a href=""{{Url}}"">View Details</a></p>
                                        <p><a href=""{{UnsubscribeUrl}}"">Unsubscribe</a></p>
                                    </body>
                                    </html>"
                },
                new Template
                {
                    Id = 2,
                    Name = "Template 2",
                    EmailSubject = "Subject Template 2",
                    IsHtmlContent = true,
                    EmailContent = @"<!DOCTYPE html>
                                    <html>
                                    <body>
                                        <h2>Hello</h2>
                                        <p>This is a notification from our system.</p>
                                        <p>We would like to promote something exciting:</p>
                                        <p>{{TemplateContent}}</p>
                                        <p><a href=""{{Url}}"">View Details</a></p>
                                        <p><a href=""{{UnsubscribeUrl}}"">Unsubscribe</a></p>
                                    </body>
                                    </html>"
                }
            );
        }
    }
}
