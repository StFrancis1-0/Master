using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Models
{
    public class Blog: BaseModel
    {
        public string Title { get; set; }
        public int Views { get; set; } = 0;
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public string CoverImagePath { get; set; }
        public bool Public { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
