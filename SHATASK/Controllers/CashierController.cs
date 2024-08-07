using System.Diagnostics;
using BusinessLayer.DTOS.BranchesModels;
using BusinessLayer.DTOS.CashierModels;
using BusinessLayer.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SHATASK.Models;

namespace SHATASK.Controllers
{
    public class CashierController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public CashierController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            
        }
        public async Task<IActionResult> Index()
        {
            var cashiers = await _serviceManager.CashierServices.GetAllAsync();
            return View(cashiers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cashier = await _serviceManager.CashierServices.GetByIdAsync(id);
            if (cashier == null) return NotFound();

            return View(cashier);
        }


        public async Task<IActionResult> Create()
        {
            var branches = await _serviceManager.BranchService.GetAllBranches();
            ViewBag.Branches = new SelectList(branches, "Id", "BranchName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CashierViewModel viewModel)
        {
            
             await _serviceManager.CashierServices.CreateAsync(viewModel);
             return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var cashier = await _serviceManager.CashierServices.GetByIdAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }

            var branches = await _serviceManager.BranchService.GetAllBranches();
            ViewBag.Branches = new SelectList(branches, "Id", "BranchName", cashier.BranchId);

            var viewModel = new CashierViewModel
            {
                Id = cashier.Id,
                CashierName = cashier.CashierName,
                BranchId = cashier.BranchId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CashierViewModel viewModel)
        {
             await _serviceManager.CashierServices.UpdateAsync(id, viewModel);
             return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var hasRelatedInvoices = await _serviceManager.InvoiceServices.HasInvoicesForCashier(id);
            if (hasRelatedInvoices)
            {
                // Optionally, remove the reference to the cashier from the invoices before deleting the cashier
                await _serviceManager.InvoiceServices.RemoveCashierReferenceFromInvoices(id);
            }

            await _serviceManager.CashierServices.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        

    }
}
