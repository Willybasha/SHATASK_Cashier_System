using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IRepositoryManager
    {
        ICashierRepo CashierRepo { get; }

        IInvoiceHeaderRepo InvoiceHeadersRepo { get; }
        IInvoiceDetailRepo InvoiceDetailsRepo { get; }
        IBranchesRepo BranchesRepo { get; }
        Task SaveAsync();
    }
}
