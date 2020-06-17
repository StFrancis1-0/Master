using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class EventVM
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Activity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
