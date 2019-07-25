using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetCoreAPI.Infrastructure;
using DotNetCoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAPI.Controllers
{
    //[EnableCors("MSPolicy")]
    [Produces("text/json","text/xml")]
    [Route("api/[controller]")]//route prefix
    [ApiController]
    public class EventsController : ControllerBase
    {
        private EventDbContext db;

        public EventsController(EventDbContext dbContext)
        {
            db = dbContext;
        }

        //GET /api/events
        [HttpGet(Name ="GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<List<EventInfo>> GetEvents()
        {
            var events = db.Events.ToList();
            return Ok(events);
        }

        ////POST /api/events
        //[HttpPost]
        //public ActionResult<EventInfo> AddEvent([FromBody]EventInfo eventInfo)
        //{
        //    var result = db.Events.Add(eventInfo);
        //    db.SaveChanges();
        //    // return Created("", result.Entity);//return statuscode 201
        //    //return CreatedAtAction(nameof(GetEvent),new { id=result.Entity.Id}, result.Entity);
        //    return CreatedAtRoute("GetById", new { id=result.Entity.Id}, result.Entity);

        //}

        //POST /api/events
        [Authorize]//token base restrict user 
        [HttpPost(Name ="AddEvent")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult<EventInfo>> AddEventAsync([FromBody]EventInfo eventInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await db.Events.AddAsync(eventInfo);
                await db.SaveChangesAsync();
                // return Created("", result.Entity);//return statuscode 201
                //return CreatedAtAction(nameof(GetEvent),new { id=result.Entity.Id}, result.Entity);
                return CreatedAtRoute("GetById", new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //GET /api/events/{id}
        [HttpGet("{id}",Name ="GetById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<EventInfo>> GetEventAsync([FromRoute] int id)
        {
            var eventInfo =await db.Events.FindAsync(id);
            if (eventInfo != null)
            {
                return eventInfo;
            }
            else
            {
                return NotFound("Item you are searching not found");
            }
        }

        //[HttpGet]
        //public ActionResult<string> GetMessage()
        //{
        //    if (Request.Headers["XYZ"].Contains("abc"))
        //    {
        //        return Created("", "Done");//return type must be 'IActionResult'
        //    }
        //    else
        //    {
        //        return "Hello"; //return type must be 'string'
        //    }
        //}
    }
}