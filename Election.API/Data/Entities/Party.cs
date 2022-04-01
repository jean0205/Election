﻿using System.ComponentModel.DataAnnotations;

namespace Election.API.Data.Entities
{
    public class Party
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        public bool Active { get; set; }
        
        [Required(ErrorMessage = "{0} is a required field.")]
        public int Code { get; set; }


        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Surname")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
