using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
   
        public class BaseContext : DbContext
        {
            public DbSet<Message> Messages { get; set; }
            public DbSet<TasksUser> TasksUsers { get; set; }
            public DbSet<Company> Companies { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Dialog> Dialogs { get; set; }
            public DbSet<Document> Documents { get; set; }
            public BaseContext(DbContextOptions<BaseContext> options)
                : base(options)
                {
              //  Database.EnsureCreated();
                }
        }
}
