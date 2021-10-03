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
    [ApiController]
    [Route("api/camps/{moniker}/talks")]
    public class TalksController : Controller // ControllerBase  - too primitive
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TalksController(
            ICampRepository repository, 
            IMapper mapper, 
            LinkGenerator linkGenerator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker)
        {
            try
            {
                var talks = await _repository.GetTalksByMonikerAsync(moniker);
                var talksModel = _mapper.Map<TalkModel[]>(talks);
                return talksModel;

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id);
                var talkModel = _mapper.Map<TalkModel>(talk);
                return talkModel;

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null)
                    return BadRequest("Camp doesn't exists");

                var talk = _mapper.Map<Talk>(model);
                talk.Camp = camp;

                if (model.Speaker == null) return BadRequest("Speaker ID is required.");

                var speaker = await _repository.GetSpeakerAsync(model.Speaker.SpeakerId);

                if (speaker == null) return BadRequest("Speaker could not be found.");
                talk.Speaker = speaker;

                _repository.Add(talk);

                if (await _repository.SaveChangesAsync())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext, "Get",
                        values: new { moniker, id = talk.TalkId });

                    return Created(url, _mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return BadRequest("Failed to save new Talk");
                }


                var talkModel = _mapper.Map<TalkModel>(talk);
                return talkModel;

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null)
                    return BadRequest("Camp doesn't exists");


                _mapper.Map(model, talk);


                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<TalkModel>(talk);
                }
                else
                {
                    return BadRequest("Failed to update Talk");
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TalkModel>> Delete(string moniker, int id)
        {
            try
            {
                var talk = await _repository.GetTalkByMonikerAsync(moniker, id, true);
                
                if (talk == null) return BadRequest("Doesn't exists");

                _repository.Delete(talk);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to update Talk");
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
