using CampaignMgmt.Models;
using CampaignMgmt.Repository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CampaignMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILogger _logger;
        public OwnerController(IOwnerRepository ownerRepository, ILogger<OwnerController> logger)
        {
            _ownerRepository = ownerRepository;
            _logger = logger;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> Get()
        {
            try
            {
                var owner = await _ownerRepository.GetAll();
                return new OkObjectResult(owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var owner = await _ownerRepository.Get(id);
                return new OkObjectResult(owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<OwnerController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Owner owner)
        {
            try
            {
                await _ownerRepository.Add(owner);
                return StatusCode(StatusCodes.Status201Created, owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Owner owner)
        {
            try
            {
                if (owner != null)
                {
                    _ownerRepository.Update(owner);
                }
                return StatusCode(StatusCodes.Status200OK, owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _ownerRepository.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
