using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Transactions;
using WebAPIExample2.Models;

namespace WebAPIExample2.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> User => Set<User>();
        public DbSet<Part> Part => Set<Part>();
        public DbSet<PartRequest> PartRequest => Set<PartRequest>();
        public DbSet<RequestedPart> RequestRequestedPart => Set<RequestedPart>();
        public DbSet<Order> Order => Set<Order>();
        public DbSet<Service> Service => Set<Service>();
        public DbSet<ServiceOrder> ServiceOrder => Set<ServiceOrder>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Forename = "Admin", Surname = "Admin", Email = "admin@example.com", Password = "password", Role = "admin" },
                new User { UserId = 2, Forename = "Mechanic", Surname = "Mechanic", Email = "mechanic@example.com", Password = "password", Role = "mechanic" },
                new User { UserId = 3, Forename = "Warehouse", Surname = "Warehouse", Email = "warehouse@example.com", Password = "password", Role = "warehouse" }
            );

            // Seed Services
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "Wymiana oleju", Description = "Szybka wymiana oleju", Price = 100 },
                new Service { ServiceId = 2, ServiceName = "Wymiana opon", Description = "Zmienimy opony z letnich na zimowe lub z zimowych na letnie", Price = 75 },
                new Service { ServiceId = 3, ServiceName = "Przygotowanie do przeglądu", Description = "Sprawdzimy auto przed zbliżającym się przeglądem", Price = 90 }
            );

            // Seed Parts
            modelBuilder.Entity<Part>().HasData(
                new Part { PartId = 1, Name = "Opony", Amount = 40 },
                new Part { PartId = 2, Name = "Filtr powietrza", Amount = 100 },
                new Part { PartId = 3, Name = "Olej silnikowy", Amount = 25 },
                new Part { PartId = 4, Name = "Misa olejowa", Amount = 10 },
                new Part { PartId = 5, Name = "Hamulce", Amount = 30 }
            );
            {
                modelBuilder.Entity<PartRequest>()
                    .HasMany(pr => pr.RequestedParts)
                    .WithOne(rp => rp.PartRequest)
                    .HasForeignKey(rp => rp.PartRequestId);

                modelBuilder.Entity<RequestedPart>()
                    .HasOne(rp => rp.Part)
                    .WithMany()
                    .HasForeignKey(rp => rp.PartId);
            }
            {
                modelBuilder.Entity<ServiceOrder>()
                    .Property(so => so.ServiceName)
                    .IsRequired();  
            }
            modelBuilder.Entity<ServiceOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ServiceId });
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.ServiceOrders)
                      .HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Service)
                      .WithMany(s => s.ServiceOrders)
                      .HasForeignKey(e => e.ServiceId);
            });
        }
    }
}

