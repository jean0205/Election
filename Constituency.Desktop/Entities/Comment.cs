﻿using System.ComponentModel.DataAnnotations;

namespace Constituency.Desktop.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
    }
}
