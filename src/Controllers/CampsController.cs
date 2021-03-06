using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PureWebApi.Models;
using PureWebApiCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampRepository repository, 
            IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> Get11(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                var models = _mapper.Map<CampModel[]>(results);
                return Ok(models);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                var models = _mapper.Map<CampModel[]>(results);
                return Ok(models);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{moniker}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                if (result == null)
                    return NotFound();

                return _mapper.Map<CampModel>(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{moniker}")]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult<CampModel>> Get11(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker, true);

                if (result == null)
                    return NotFound();

                return _mapper.Map<CampModel>(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //[HttpGet]
        //public IActionResult GetSimple()
        //{
        //    return Ok(new { Moniker = "ATL2018", Name = "Atlanat Code" });
        //}

        //public IActionResult Index()
        //{
        //    return null;
        //    //return View();
        //}

        [HttpGet("search/{theDate}")]
        public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(theDate, includeTalks);

                if (!results.Any())
                    return NotFound();

                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                var existingCamp = await _repository.GetCampAsync(model.Moniker);
                if (existingCamp != null)
                    return BadRequest("Moniker exists.");

                var location = _linkGenerator
                    .GetPathByAction("Get", "Camps", new { moniker = model.Moniker });

                if (string.IsNullOrWhiteSpace(location))
                    return BadRequest("Could not use current moniker.");


                var camp = _mapper.Map<Camp>(model);
                _repository.Add(camp);

                if (await _repository.SaveChangesAsync())
                    return Created(location, _mapper.Map<CampModel>(camp));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel campModel)
        {
            try
            {
                var existingCamp = await _repository.GetCampAsync(moniker);
                if (existingCamp == null)
                    return BadRequest($"Could not find camp by this Moniker: {moniker}");

                _mapper.Map(campModel, existingCamp);

                if (await _repository.SaveChangesAsync())
                    return _mapper.Map<CampModel>(existingCamp);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var existingCamp = await _repository.GetCampAsync(moniker);
                if (existingCamp == null)
                    return BadRequest($"Could not find camp by this Moniker: {moniker}");

                _repository.Delete(existingCamp);

                if (await _repository.SaveChangesAsync())
                    return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return BadRequest();
        }
    }
}
