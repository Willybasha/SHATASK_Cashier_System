using BusinessLayer.DTOS.InvoiceDetailsModels;
using BusinessLayer.Services.InvoiceDetailServices;
using BusinessLayer.Services.ServiceManager;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SHATASK.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public InvoiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            
        }
        public async Task<IActionResult> Index()
        {
            var invoices = await _serviceManager.InvoiceServices.GetAllInvoicesAsync();
            return View(invoices);
        }
        public async Task<IActionResult> Details(long id)
        {
            var viewModel = await _serviceManager.InvoiceServices.GetInvoiceByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Create()
        {
            var branches=await _serviceManager.BranchService.GetAllBranches();
            ViewBag.Branches = new SelectList(branches, "Id", "BranchName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceHeader,InvoiceDetails")] InvoiceViewModel viewModel)
        {   
            await _serviceManager.InvoiceServices.CreateInvoiceAsync(viewModel);
            return RedirectToAction(nameof(Details), new { id = viewModel.InvoiceHeader.Id });

        }

        public async Task<IActionResult> Edit(long id)
        {
            var viewModel = await _serviceManager.InvoiceServices.GetInvoiceByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("InvoiceHeader,InvoiceDetails")] InvoiceViewModel viewModel)
         {
            await _serviceManager.InvoiceServices.UpdateInvoiceAsync(id, viewModel);   
            return RedirectToAction(nameof(Details), new { id = viewModel.InvoiceHeader.Id });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _serviceManager.InvoiceServices.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            await _serviceManager.InvoiceServices.DeleteInvoiceAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<JsonResult> GetCashiersByBranch(int branchId)
        {
            var cashiers = await _serviceManager.CashierServices.GetCashierByBranchId(branchId);
            var x = Json(cashiers);
            return x;
        }

    }
}
