using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services;
using StudentsAPI.V2.Services.Interfaces;

namespace StudentsAPI.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }
        
        // GET: api/Events
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return eventsService.Get();
        }
    }
}
