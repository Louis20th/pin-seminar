using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;

namespace seminar_API.Models
{
    public class ApiResponse
    {
        public object Result { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<String> ErrorMessages { get; set; }

        public ApiResponse()
        {
            ErrorMessages = new();
        }
    }
}
