using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Common.Infrastructure
{
    public class Book
    {
            public int Id { get; set; }
            public string Author { get; set; }
            public string Title { get; set; }
            [Column(TypeName = "decimal(10.2")]
            public decimal Price { get; set; }
            public List<Order> Orders { get; set; }
        
    }
}
