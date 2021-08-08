using Invoice.Model.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Utilities.DbContextModel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<InvoiceModel> Invoice { get; set; }
        public DbSet<InvoiceItemModel> InvoiceItem { get; set; }
        public DbSet<HomeModel> Product { get; set; }
        public DbSet<CartModel> Cart { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
        public DbSet<UserModel> UserTbl { get; set; }
        public DbSet<CartProductsModel> Products { get; set; }
    }
}
