using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Dtos
{
    public class PurchaseOrderDetailDto
    {
        public DateTime Day { get; set; }
        public decimal LineTotal { get; set; }
        public int OrderQty { get; set; }
        public decimal TotalSum { get; set; }
    }
}
