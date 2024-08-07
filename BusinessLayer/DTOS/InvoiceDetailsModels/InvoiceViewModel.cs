using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Entities;

namespace BusinessLayer.DTOS.InvoiceDetailsModels
{
    public class  InvoiceViewModel
    {
        public InvoiceHeader InvoiceHeader { get; set; } = null!;
        public List<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public double TotalPrice => InvoiceDetails.Sum(d => d.ItemCount * d.ItemPrice);
    }
}
