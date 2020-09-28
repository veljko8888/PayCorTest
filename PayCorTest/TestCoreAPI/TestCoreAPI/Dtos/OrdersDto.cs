using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Dtos
{
    public class OrdersDto
    {
        public int TotalPages { get; set; }
        public List<PurchaseOrderDetailDto> Orders { get; set; }
    }
}
