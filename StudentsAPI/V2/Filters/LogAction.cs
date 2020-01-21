using Microsoft.AspNetCore.Mvc.Filters;
using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;

namespace StudentsAPI.V2.Filters
{
    public class LogAction : IResultFilter
    {
        private readonly IEventsService eventsService;

        public LogAction(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Event action = new Event
            {
                Path = context.HttpContext.Request.Path,
                StatusCode = context.HttpContext.Response.StatusCode,
                Type = context.HttpContext.Request.Method
            };
            eventsService.Add(action);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }

    }
}
