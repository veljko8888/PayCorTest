using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreAPI.Models
{
    [Table(nameof(vProductAndDescription), Schema = "Production")]
    public class vProductAndDescription
    {
        [Key, Column(Order = 0)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public DateTime SellStartDate { get; set; }
        public string ProductModel { get; set; }
        [Key, Column(Order = 1)]
        public string CultureID { get; set; }
        public string Description { get; set; }
    }
}
