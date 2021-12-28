using AutoMapper;
using RaquetShop.Data.Entities;
using RaquetShop.Data.Repository;
using RaquetShop.Exceptions;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Services
{
    public class BrandsService:IBrandsService
    {
        private IRaquetRepository _repository;
        private IMapper _mapper;

        public BrandsService(IRaquetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BrandModel> CreateBrandAsync(BrandModel newBrand)
        {
            var brandEntity = _mapper.Map<BrandEntity>(newBrand);
            _repository.CreateBrand(brandEntity);
            var result = await _repository.SaveChangesAsync();

            if (result)
            {
                return _mapper.Map<BrandModel>(brandEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteBrandAsync(long brandId)
        {
            await ValidateBrandAsync(brandId);
            var lengthFigures = await _repository.GetRaquetsAsync(brandId);
            if (lengthFigures.Count() > 0)
                throw new ExistElementsException("Existen Raquetas dentro de esta Marca.");
            await _repository.DeleteBrandAsync(brandId);
            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<BrandModel> GetBrandAsync(long brandId)
        {
            var brand = await _repository.GetBrandAsync(brandId);
            if (brand == null)
            {
                throw new NotFoundItemException($"La marca con el id: {brandId}, No existe!.");
            }
            return _mapper.Map<BrandModel>(brand);
        }

        public async Task<IEnumerable<BrandModel>> GetBrandsAsync()
        {
            var entityList = await _repository.GetBrandsAsync();
            var modelList = _mapper.Map<IEnumerable<BrandModel>>(entityList);
            return modelList;
        }

        public async Task<BrandModel> UpdateBrandAsync(long brandId, BrandModel updatedBrand)
        {
            await GetBrandAsync(brandId);
            updatedBrand.id = brandId;
            await _repository.UpdateBrandAsync(brandId, _mapper.Map<BrandEntity>(updatedBrand));
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return _mapper.Map<BrandModel>(updatedBrand);
        }

        private async Task ValidateBrandAsync(long brandId)
        {
            await GetBrandAsync(brandId);
        }
    }
}
