using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class DataModel
    {
        public List<EventVM> SUN { get; set; }
        public List<EventVM> MON { get; set; }
        public List<EventVM> TUE { get; set; }
        public List<EventVM> WED { get; set; }
        public List<EventVM> THUR { get; set; }
        public List<EventVM> FRI { get; set; }
        public List<EventVM> SAT { get; set; }
    }
}
