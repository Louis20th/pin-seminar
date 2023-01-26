using System;
using System.ComponentModel.DataAnnotations;

namespace seminar_API.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
