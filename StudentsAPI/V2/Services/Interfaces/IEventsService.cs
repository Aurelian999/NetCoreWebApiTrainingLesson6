using StudentsAPI.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.V2.Services.Interfaces
{
    public interface IEventsService
    {
        void Add(Event action);
        IEnumerable<Event> Get();
    }
}
