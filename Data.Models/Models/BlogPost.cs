﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Models
{
    public class BlogPost
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public Category? Category { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }
}
