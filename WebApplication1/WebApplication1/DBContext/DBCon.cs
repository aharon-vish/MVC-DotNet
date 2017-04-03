using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.Models
{
    public class DBCon :DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Receipt> ReceiptS { get; set; }
        public DbSet<StoreUser> StoreUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

            Database.SetInitializer<DBCon>(new GiftCardDBInitializer());

            //one-to-many 
            modelBuilder.Entity<User>()
                        .HasMany<GiftCard>(u => u.GiftCards)
                        .WithRequired(g => g.User)
                        .HasForeignKey(g => g.UserId);

            //one-to-many 
            modelBuilder.Entity<GiftCard>()
                        .HasMany<Receipt>(r => r.Receipts)
                        .WithRequired(r => r.GiftCard)
                        .HasForeignKey(r => r.GiftCardID);


        }
}
}