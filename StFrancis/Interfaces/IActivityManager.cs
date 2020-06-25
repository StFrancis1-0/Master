using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Interfaces
{
    public interface IActivityManager
    {
        Task<(bool, Event)> CreateActivity(Event model);
        Task<DataModel> GetEVents();
    }
}
