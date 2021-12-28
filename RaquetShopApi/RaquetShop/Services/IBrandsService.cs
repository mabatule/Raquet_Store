using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Services
{
    public interface IBrandsService
    {
        public Task<IEnumerable<BrandModel>> GetBrandsAsync();
        public Task<BrandModel> GetBrandAsync(long brandId);
        public Task<BrandModel> CreateBrandAsync(BrandModel newBrand);
        public Task<BrandModel> UpdateBrandAsync(long brandId, BrandModel updatedBrand);
        public Task<bool> DeleteBrandAsync(long brandId);
    }
}
