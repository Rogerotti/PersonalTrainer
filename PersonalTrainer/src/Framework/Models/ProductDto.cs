using System;

namespace Framework.Models
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public String Name { get; set; }

        public String Manufacturer { get; set; }

        public ProductType Type { get; set; }
        
        public Macro Macro { get; set; }

        public ProductState State { get; set; }
    }
}
