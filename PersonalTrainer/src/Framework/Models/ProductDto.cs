﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Framework.Models
{
    public class ProductDto
    {
        public Guid UserId { get; set; }

        public String Name { get; set; }

        public String Manufacturer { get; set; }

        public ProductType Type { get; set; }
        
        public Macro Macro { get; set; }
    }
}
