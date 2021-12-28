using Microsoft.EntityFrameworkCore;
using RaquetShop.Data.Entities;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data.Repository
{
    public class RaquetRepository : IRaquetRepository
    {
        private RaquetShopDBContext _dbContext;

        public RaquetRepository(RaquetShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateBrand(BrandEntity newBrand)
        {
            _dbContext.Brands.Add(newBrand);
        }

        public void CreateRaquet(long brandId, RaquetEntity newRaquet)
        {
            _dbContext.Entry(newRaquet.Brand).State = EntityState.Unchanged;
            _dbContext.Raquets.Add(newRaquet);
        }

        public async Task DeleteBrandAsync(long brandId)
        {
            var brandToDelete = await _dbContext.Brands.FirstAsync(b => b.id == brandId);
            _dbContext.Brands.Remove(brandToDelete); 
            
        }

        public async Task DeleteRaquetAsync(long brandId, long raquetId)
        {
            var raquetToDelete = await _dbContext.Raquets.FirstOrDefaultAsync(f => f.id == raquetId);
            _dbContext.Remove(raquetToDelete);
        }

        public async Task<BrandEntity> GetBrandAsync(long brandId)
        {
            IQueryable<BrandEntity> query = _dbContext.Brands;
            query = query.AsNoTracking();
            query = query.Include(b => b.raquets);
            return await query.FirstOrDefaultAsync(b => b.id == brandId);
        }

        public async Task<ICollection<BrandEntity>> GetBrandsAsync()
        {
            IQueryable<BrandEntity> query = _dbContext.Brands;
            query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<RaquetEntity> GetRaquetAsync(long brandId, long RaquetId)
        {
            IQueryable<RaquetEntity> query = _dbContext.Raquets;
            query = query.AsNoTracking();
            //query = query.Include(f => f.Brand);
            return await query.FirstOrDefaultAsync(p => p.Brand.id == brandId && p.id == RaquetId);
        }

        public async Task<ICollection<RaquetEntity>> GetRaquetsAsync(long brandId, string orderBy = "id")
        {
            IQueryable<RaquetEntity> query = _dbContext.Raquets;
            query = query.AsNoTracking();
            query = query.Where(f => f.Brand.id == brandId);

            query = query.Include(f => f.Brand);
            switch (orderBy.ToLower())
            {
                case "modelo":
                    query = query.OrderBy(f => f.modelo);
                    break;
                case "peso":
                    query = query.OrderBy(f => f.peso);
                    break;
                case "precio":
                    query = query.OrderBy(f => f.precio);
                    break;
                default:
                    query = query.OrderBy(f => f.id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateBrandAsync(long brandId, BrandEntity updatedBrand)
        {
            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.id == brandId);
            brand.nombre = updatedBrand.nombre ?? brand.nombre;
            brand.pais = updatedBrand.pais ?? brand.pais;
            brand.descripcion = updatedBrand.descripcion ?? brand.descripcion;
        }

        public async Task UpdateRaquetAsync(long brandId, long raquetId, RaquetEntity updatedRaquet)
        {
            var raquetToUpdate = await _dbContext.Raquets.FirstOrDefaultAsync(r => r.id == raquetId);
            raquetToUpdate.modelo = updatedRaquet.modelo ?? raquetToUpdate.modelo;
            raquetToUpdate.peso = updatedRaquet.peso ?? raquetToUpdate.peso;
            raquetToUpdate.precio = updatedRaquet.precio ?? raquetToUpdate.precio;
            raquetToUpdate.descripcion = updatedRaquet.descripcion ?? raquetToUpdate.descripcion;
            raquetToUpdate.cantidad = updatedRaquet.cantidad ?? raquetToUpdate.cantidad;
            raquetToUpdate.MainPhoto = updatedRaquet.MainPhoto ?? raquetToUpdate.MainPhoto;

        }
    }
}
