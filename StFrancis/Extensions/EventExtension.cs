using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Extensions
{
    public static class EventExtension
    {
        public static List<EventVM> ToEventListVM(this List<Event> events)
        {
            return events.ConvertAll(x => new EventVM
            {
                Day = x.Day,
                Date = x.Date,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Activity = x.Activity,
                CreatedAt = x.CreatedAt,
            });
        }
    }
}
