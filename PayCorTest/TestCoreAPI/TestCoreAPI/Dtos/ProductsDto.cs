using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Dtos
{
    public class ProductsDto
    {
        public int TotalPages { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
