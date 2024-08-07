using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOS.CashierModels;

namespace BusinessLayer.Services.CashierServices
{
    public interface ICashierServices
    {
        Task<IEnumerable<CashierResponse>> GetCashierByBranchId(int branchId);
        Task<IEnumerable<CashierViewModel>> GetAllAsync();
        Task<CashierViewModel> GetByIdAsync(int id);
        Task CreateAsync(CashierViewModel cashier);
        Task UpdateAsync(int id, CashierViewModel cashierDto);
        Task DeleteAsync(int id);
    }
}
