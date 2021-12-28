using RaquetShop.Data.Entities;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data.Repository
{
    public interface IRaquetRepository
    {
        //Brands
        public Task<ICollection<BrandEntity>> GetBrandsAsync();
        public Task<BrandEntity> GetBrandAsync(long brandId);
        public void CreateBrand(BrandEntity newBrand);
        public Task UpdateBrandAsync(long brandId, BrandEntity updatedBrand);
        public Task DeleteBrandAsync(long brandId);
        //Raquets
        public Task<ICollection<RaquetEntity>> GetRaquetsAsync(long brandId, string orderBy = "id");
        public Task<RaquetEntity> GetRaquetAsync(long brandId, long RaquetId); 
        public void CreateRaquet(long brandId, RaquetEntity newRaquet);
        public Task UpdateRaquetAsync(long brandId, long raquetId, RaquetEntity updatedRaquet);
        public Task DeleteRaquetAsync(long brandId, long raquetId); 
        Task<bool> SaveChangesAsync();



    }
}
