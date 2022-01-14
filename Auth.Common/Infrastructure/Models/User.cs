using System;
using System.Collections.Generic;

namespace Auth.Common.Infrastructure
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<Order> Orders { get; set; }
    }
    public enum Role
    {
        User,
        Admin
    }
}
