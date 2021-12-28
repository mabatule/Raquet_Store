using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaquetShop.Exceptions;
using RaquetShop.Models;
using RaquetShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Controllers
{
    //[Authorize()] //aqui es para validar la seguridad
    //[Authorize(Roles= "Admin")] solo para el admin
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        public IBrandsService _service;
        public BrandsController(IBrandsService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<  ActionResult<ICollection<BrandModel> > > GetBrands()
        {
            try
            {
                var user = User;// aqui saca los roles del usuario que capte y tambien que este permitido.

                var brands = await _service.GetBrandsAsync();
                return Ok(brands);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [HttpGet("{brandId:long}")]
        public async Task<ActionResult<BrandModel>> GetBrands(long brandId)
        {
            try
            {
                var brands = await _service.GetBrandAsync(brandId);
                return Ok(brands);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<BrandModel>> CreateBrand([FromBody] BrandModel newBrand)
        {
            try
            {
                var brand = await _service.CreateBrandAsync(newBrand);
                return Created($"api/brands/{brand.id}", brand);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [HttpPut("{brandId:long}")]
        public async Task<ActionResult<BrandModel>> UpdateBrand(long brandId, [FromBody] BrandModel updatedBrand)
        {
            try
            {
                var brand = await _service.UpdateBrandAsync(brandId, updatedBrand);
                return Ok(brand);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something unexpected happened. {ex.Message}");
            }
        }
        [HttpDelete("{brandId:long}")]
        public async Task<ActionResult<bool>> DeleteBrandAsync(long brandId)
        {
            try
            {
                var result = await _service.DeleteBrandAsync(brandId);
                return Ok(result);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExistElementsException ex)
            {
                ///406 not aceptable status code
                return StatusCode(406, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
    }
}
