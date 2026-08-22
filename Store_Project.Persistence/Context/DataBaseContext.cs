using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store_Project.Domain.Entities.Users;
using Store_Project_Application.Interfaces.Contexts;

namespace Store_Project.Persistence.Context
{
    public class DataBaseContext : DbContext , IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
    }
}
