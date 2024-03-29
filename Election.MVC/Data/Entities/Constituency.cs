﻿using System.ComponentModel.DataAnnotations;

namespace Election.MVC.Data.Entities
{
    public class Constituency
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [MaxLength(50)]
        public string SGSE { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Name of the place")]
        [MaxLength(150)]
        public string Name { get; set; }

        public ICollection<PollingDivision>? PollingDivisions { get; set; }
    }
}
