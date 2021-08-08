using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Model.Master
{
    public class HomeModel
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductImagePath { get; set; }
        public int CurrentQuantity { get; set; }
        public Guid ProductGuid { get; set; }
    }
}
