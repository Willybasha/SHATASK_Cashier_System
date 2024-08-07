using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Services.AuthService;
using BusinessLayer.Services.BranchesServices;
using BusinessLayer.Services.CashierServices;
using BusinessLayer.Services.InvoiceDetailServices;

namespace BusinessLayer.Services.ServiceManager
{
    public interface IServiceManager
    {
        ICashierServices CashierServices { get; }

        IInvoiceServices InvoiceServices { get; }

        IBranchesService BranchService { get; }

        IAuthService AuthService { get; }
    }
}
