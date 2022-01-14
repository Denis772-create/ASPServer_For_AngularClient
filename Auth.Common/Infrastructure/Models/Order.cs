using System;
using System.Collections.Generic;

namespace Auth.Common.Infrastructure
{
    public class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public Guid UserId { get; set; } 
        public  virtual  User User { get; set; }
    }
}
