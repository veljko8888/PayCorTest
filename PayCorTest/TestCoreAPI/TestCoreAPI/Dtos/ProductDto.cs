using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Dtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public DateTime SellStartDate { get; set; }
        public string ProductModel { get; set; }
        public string CultureID { get; set; }
        public string Description { get; set; }
    }
}
