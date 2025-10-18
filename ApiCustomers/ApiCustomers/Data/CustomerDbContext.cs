using ApiCustomers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApiCustomers.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> opts): base(opts) { }
        public DbSet<Customer> Customers { get; set; }

    }
}
