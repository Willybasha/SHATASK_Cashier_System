using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOS.InvoiceDetailsModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.InvoiceDetailServices
{
    public interface IInvoiceServices
    {
        Task<IEnumerable<InvoiceViewModel>> GetAllInvoicesAsync();
        Task<InvoiceViewModel> GetInvoiceByIdAsync(long id);
        Task CreateInvoiceAsync(InvoiceViewModel viewModel);
        Task UpdateInvoiceAsync(long id, InvoiceViewModel viewModel);
        Task DeleteInvoiceAsync(long id);
        Task<bool> HasInvoicesForCashier(int cashierId);
        Task RemoveCashierReferenceFromInvoices(int cashierId);




    }
}
