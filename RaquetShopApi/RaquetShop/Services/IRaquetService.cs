using RaquetShop.Data.Entities;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Services
{
    public interface IRaquetService
    {
        public Task<IEnumerable<RaquetModel>> GetRaquetsAsync(long brandId, string orderBy = "id");
        public Task<RaquetModel> GetRaquetAsync(long brandId, long RaquetId);
        public Task<RaquetModel> CreateRaquet(long brandId, RaquetModel newRaquet);
        public Task<RaquetModel> UpdateRaquetAsync(long brandId, long raquetId, RaquetModel updatedRaquet);
        public Task<bool> DeleteRaquetAsync(long brandId, long raquetId);
    }
}
