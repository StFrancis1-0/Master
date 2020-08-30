using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class BlogDto
    {
        public class CreatedBlogResponse
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Views { get; set; } 
            public string Content { get; set; }
            public string Excerpt { get; set; }
            public string CoverImagePath { get; set; }
            public bool Public { get; set; } 
            public bool Deleted { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

        }

        public class BlogRequest
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Excerpt { get; set; }
            public string CoverImagePath { get; set; }
           
        }

        public class Status
        {
            public bool Public { get; set; } 
        }
    }
}
