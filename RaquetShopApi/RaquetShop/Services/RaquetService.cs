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
    public class RaquetService : IRaquetService
    {
        private IRaquetRepository _repository;
        private IMapper _mapper;
        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "modelo",
            "peso",
            "precio"
        };
        public RaquetService(IRaquetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private async Task ValidateBrandAsync(long brandId)
        {
            var team = await _repository.GetBrandAsync(brandId);
            if (team == null)
            {
                throw new NotFoundItemException($"La marca con el id: {brandId}, No existe!.");
            }
        }
        public async Task<RaquetModel> CreateRaquet(long brandId, RaquetModel newRaquet)
        {
            await ValidateBrandAsync(brandId);
            newRaquet.BrandId = brandId;
            var raquetEntity = _mapper.Map<RaquetEntity>(newRaquet);

            _repository.CreateRaquet(brandId, raquetEntity);

            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return _mapper.Map<RaquetModel>(raquetEntity);
        }
        private async Task ValidateBrandAndRaquetAsync(long brandId, long figureId)
        {
            var player = await GetRaquetAsync(brandId, figureId);
        }
        public async Task<bool> DeleteRaquetAsync(long brandId, long raquetId)
        {
            await ValidateBrandAndRaquetAsync(brandId, raquetId);
            await _repository.DeleteRaquetAsync(brandId, raquetId);

            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return true;
        }

        public async Task<RaquetModel> GetRaquetAsync(long brandId, long RaquetId)
        {
            await ValidateBrandAsync(brandId);
            var raquetEntity = await _repository.GetRaquetAsync(brandId, RaquetId);
            if (raquetEntity == null)
            {
                throw new NotFoundItemException($"La raqueta con el id: {RaquetId} no existe en la Marca con el id:{brandId}.");
            }

            var raquetModel = _mapper.Map<RaquetModel>(raquetEntity);

            raquetModel.BrandId = brandId;
            return raquetModel;
        }

        public async Task<IEnumerable<RaquetModel>> GetRaquetsAsync(long brandId, string orderBy = "id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidElementOperationExeception($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            await ValidateBrandAsync(brandId);
            var raquets = await _repository.GetRaquetsAsync(brandId, orderBy);
            return _mapper.Map<IEnumerable<RaquetModel>>(raquets);
        } 

        public async Task<RaquetModel> UpdateRaquetAsync(long brandId, long raquetId, RaquetModel updatedRaquet)
        {
            await ValidateBrandAndRaquetAsync(brandId, raquetId);
            await _repository.UpdateRaquetAsync(brandId, raquetId, _mapper.Map<RaquetEntity>(updatedRaquet));
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }

            return updatedRaquet;
        }
    }
}
