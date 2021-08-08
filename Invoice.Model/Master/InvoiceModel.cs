using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Model.Master
{
    public class InvoiceModel
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public string SupplierName { get; set; }
        public string BillAddress { get; set; }
        public string ShipAddress { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public string ZipCode { get; set; }
        public DateTimeOffset IssuDate { get; set; }
    }

    public class InvoiceItemModel
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public float Rate { get; set; }
        public float Amount { get; set; }
    }

    public class InvoiceItemsModel
    {
        public InvoiceModel InvoiceDetails { get; set; }
        public HomeModel CartItems { get; set; }
    }
}
