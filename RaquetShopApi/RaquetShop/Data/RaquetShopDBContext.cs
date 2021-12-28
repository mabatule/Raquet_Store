using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaquetShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data
{
    public class RaquetShopDBContext: IdentityDbContext
    {
        public DbSet<BrandEntity> Brands{ get; set; }
        public DbSet<RaquetEntity> Raquets { get; set;}
        public RaquetShopDBContext(DbContextOptions<RaquetShopDBContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BrandEntity>().ToTable("Brands");
            modelBuilder.Entity<BrandEntity>().Property(b => b.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BrandEntity>().HasMany(b => b.raquets).WithOne(f => f.Brand);

            modelBuilder.Entity<RaquetEntity>().ToTable("Raquets");
            modelBuilder.Entity<RaquetEntity>().Property(f => f.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RaquetEntity>().HasOne(f => f.Brand).WithMany(b => b.raquets);

            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add {name}                       es para actualizar los datos que estan aqui y si aumentas cosas se va a ir aumentando
            //dotnet ef database update     esto usa la carpeta migrations y es para la creacion o actualizacion de la base de datos
        }
    }
}
