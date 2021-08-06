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
        public IActionResult Get()
        {
            try
            {
                var owner = _ownerRepository.GetOwner();
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
        public IActionResult Get(string id)
        {
            try
            {
                var user = _ownerRepository.GetOwnerById(id);
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<OwnerController>
        [HttpPost]
        public IActionResult Post([FromBody] Owner owner)
        {
            try
            {
                _ownerRepository.InsertOwner(owner);
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
                    _ownerRepository.UpdateOwner(id,owner);
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
        public IActionResult Delete(int id)
        {
            try
            {
                _ownerRepository.DeleteOwner(id);
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
