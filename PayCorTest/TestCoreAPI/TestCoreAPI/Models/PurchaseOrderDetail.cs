using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCoreAPI.Models
{
    [Table(nameof(PurchaseOrderDetail), Schema = "Purchasing")]
    public class PurchaseOrderDetail
    {
        [Key, Column(Order = 0)]
        public int PurchaseOrderID { get; set; }
        [Key, Column(Order = 1)]
        public int PurchaseOrderDetailID { get; set; }
        public int ProductID { get; set; }
        public Int16 OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectedQty { get; set; }
        public decimal StockedQty { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
