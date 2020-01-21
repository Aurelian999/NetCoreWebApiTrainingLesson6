using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System.Collections.Generic;

namespace StudentsAPI.V2.Services
{
    public class EventsService : IEventsService
    {
        private readonly List<Event> events;

        public EventsService()
        {
            events = new List<Event>();
        }

        public void Add(Event action)
        {
            lock (events)
            {
                action.Id = events.Count + 1;
                events.Add(action);
            }
        }

        public IEnumerable<Event> Get()
        {
            return events;
        }
    }
}
