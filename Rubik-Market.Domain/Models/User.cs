﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Domain.Models
{
    public class User:BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool isAdmin { get; set; } = false;
        public string? ConfirmCode { get; set; }
        public bool isActive { get; set; } = false;
    }
}
