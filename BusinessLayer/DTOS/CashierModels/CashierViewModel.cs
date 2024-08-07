using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOS.BranchesModels;

namespace BusinessLayer.DTOS.CashierModels
{
    public class CashierViewModel
    {
        public int Id { get; set; }
        public string CashierName { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }


        public IEnumerable<BranchResponse> Branches { get; set; }    
    }
}
