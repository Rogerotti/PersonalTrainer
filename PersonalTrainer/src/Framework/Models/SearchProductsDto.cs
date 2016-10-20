using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class SearchProductsDto
    {
        public IEnumerable<ProductDto> AllProducts { get; set; }

        public IEnumerable<ProductDto> UserProducts { get; set; }

        public IEnumerable<ProductDto> RecentProducts { get; set; }
    }
}
