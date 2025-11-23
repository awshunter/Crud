using Microsoft.EntityFrameworkCore;
using InvoiceMVC.Models;

namespace InvoiceMVC.Services
{
    public class ApplicatonDbContext : DbContext
    {
        public ApplicatonDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Invoice> Invoices { get; set; } = null!;
    }
}
