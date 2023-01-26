using System;
using System.Collections.Generic;

namespace seminar_API.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
