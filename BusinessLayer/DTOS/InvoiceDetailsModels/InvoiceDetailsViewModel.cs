using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOS.InvoiceDetailsModels
{
    public class InvoiceDetailsViewModel
    {
       public long Id { get; set; } 
       public string ItemName { get; set; } 
       public double ItemCount { get; set; }
       public double ItemPrice { get; set; }
    }
}
