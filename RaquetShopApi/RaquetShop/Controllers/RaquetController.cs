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
    [Route("api/brands/{brandId:long}/[controller]")]
    public class RaquetController : Controller
    {
        private IRaquetService _raquetService;
        private IFileService _fileService;
        public RaquetController(IRaquetService raquetService, IFileService fileService)
        {
            _raquetService = raquetService;
            _fileService = fileService;
        }

        // GET 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaquetModel>>> GetRaquets(long brandId, string orderBy = "id") 
        {
            try
            {
                var raquets =await _raquetService.GetRaquetsAsync(brandId, orderBy);
                return Ok(raquets);
            }
            catch (InvalidElementOperationExeception ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }

        }

        // GET 
        [HttpGet("{raquetId}")]
        public async Task<ActionResult<RaquetModel>> GetRaquet(long brandId, long raquetId)
        { 
            try
            {

                var raquet = await _raquetService.GetRaquetAsync(brandId, raquetId);
                return Ok(raquet);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);//si no existe el mensaje 
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Something happend");// se va a esto
            }
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<RaquetModel>> CreatedRaquet(long brandId,[FromBody] RaquetModel newraquet)
        { 
            try
            {
                /*if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }*/
                var raquet = await _raquetService.CreateRaquet(brandId, newraquet);
                return Created($"/api/brands/{brandId}/figures/{raquet.id}", raquet);
            }
            catch (NotFoundItemException ex)// aqui agarramos el mensaje del servidor
            {
                return NotFound(ex.Message);//si no existe el mensaje 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happend");// se va a esto

            }
        }
        //Create Image
        [HttpPost("Form")]
        public async Task<ActionResult<RaquetModel>> CreateBrandFormAsync(long brandId, [FromForm] RaquetFormModel newRaquet)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                newRaquet = _fileService.createFilesToRaquet(newRaquet);

                var raquet = await _raquetService.CreateRaquet(brandId, newRaquet);   

                return Created($"/api/brands/{brandId}/figure/${newRaquet.id}", raquet);
            }
            catch (NotFoundFileException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        //PUT Image
        [HttpPut("Form/{figureId:long}")] 
        public async Task<ActionResult<RaquetModel>> UpdateFigureFormAsync(long brandId, long figureId, [FromForm] RaquetFormModel newRaquet)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                newRaquet = _fileService.updateFilesToRaquet(newRaquet);
                var raquet = await _raquetService.UpdateRaquetAsync(brandId, figureId, newRaquet);
                return Ok(raquet);
            }
            catch (NotFoundFileException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        // DELETE 
        [HttpDelete("{raquetId}")]
        public async Task<ActionResult<bool>> DeleteRaquet(long brandId,int raquetId)
        {
            try
            {
                var result =await _raquetService.DeleteRaquetAsync(brandId, raquetId);
                return Ok(result);
            }
            catch (NotFoundItemException ex)// aqui agarramos el mensaje del servidor
            {
                return NotFound(ex.Message);//si no existe el mensaje 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happend");// se va a esto

            }

        }
        // PUT 
        [HttpPut("{raquetId}")]
        public async Task<ActionResult<RaquetModel>> UpdateRaquet(long brandId, int raquetId, [FromBody] RaquetModel raquetToUpdate)
        { 
            try 
            {
                if (raquetToUpdate!=null)
                {
                    var updatedRaquet = await _raquetService.UpdateRaquetAsync(brandId, raquetId, raquetToUpdate);
                    return Ok(updatedRaquet);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "null :'v ");// se va a esto
                }
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);//si no existe el mensaje 
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Something happend");// se va a esto
            }
        }
 
    }
}
