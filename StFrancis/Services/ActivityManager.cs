using StFrancis.Data;
using StFrancis.Interfaces;
using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StFrancis.Extensions;

namespace StFrancis.Services
{
    public class ActivityManager: IActivityManager
    {
        private readonly AppDbContext _context;

        public ActivityManager(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateActivity(Event model)
        {
            try
            {
                _context.Events.Add(model);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<DataModel> GetEVents()
        {
            DataModel data = new DataModel();
            var events = _context.Events.OrderByDescending(x => x.CreatedAt).ToList();
            if(events.Count != 0)
            {
                var sunday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "sunday").ToList();
                data.SUN = sunday.ToEventListVM();

                var monday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "monday").ToList();
                data.MON = monday.ToEventListVM();

                var tuesday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "tuesday").ToList();
                data.TUE = tuesday.ToEventListVM();

                var wednesday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "wednesday").ToList();
                data.WED = wednesday.ToEventListVM();

                var thursday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "thursday").ToList();
                data.THUR = thursday.ToEventListVM();

                var friday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "friday").ToList();
                data.FRI = friday.ToEventListVM();

                var saturday = events.OrderByDescending(x => x.CreatedAt).Where(p => p.Day.ToLower() == "saturday").ToList();
                data.SAT = saturday.ToEventListVM();

                return data;

            }

            return data;
        }
    }
}
