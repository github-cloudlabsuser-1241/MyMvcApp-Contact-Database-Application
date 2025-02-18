using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcApp.Models
{
    public class User
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // Name of the user
        public string Name { get; set; } = string.Empty;

        // Email of the user
        public string Email { get; set; } = string.Empty;
    }
}
