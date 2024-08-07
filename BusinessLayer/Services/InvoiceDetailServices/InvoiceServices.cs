using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOS.InvoiceDetailsModels;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.InvoiceDetailServices
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public InvoiceServices(IRepositoryManager invoiceRepository,IMapper mapper)
        {
            _repository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task CreateInvoiceAsync(InvoiceViewModel viewModel)
        {
            try
            {
                var invoiceHeader = viewModel.InvoiceHeader;
                invoiceHeader.InvoiceDetails = viewModel.InvoiceDetails;
                _repository.InvoiceHeadersRepo.Create(invoiceHeader);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task DeleteInvoiceAsync(long id)
        {
            try
            {
                var invoice = await _repository.InvoiceHeadersRepo.FindByCondition(x => x.Id == id, false).Include(i => i.InvoiceDetails).FirstOrDefaultAsync();

                if (invoice == null)
                {
                    throw new KeyNotFoundException($"Invoice with ID {id} not found.");
                }

                _repository.InvoiceDetailsRepo.DeleteRange(invoice.InvoiceDetails);

                
                _repository.InvoiceHeadersRepo.Delete(invoice);

                
                await _repository.SaveAsync();
            }

            catch (Exception ex)
            { 
                throw new InvalidOperationException("Error occurred while deleting the invoice.", ex);
            }

        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllInvoicesAsync()
        {
            var invoices= await _repository.InvoiceHeadersRepo.FindAll(false).Include(i => i.InvoiceDetails).ToListAsync();

            var invoiceview = invoices.Select(i => new InvoiceViewModel
            {
                InvoiceHeader = i,
                InvoiceDetails = i.InvoiceDetails.ToList()
            });


            return invoiceview;
        }

        public async Task<InvoiceViewModel> GetInvoiceByIdAsync(long id)
        {
            try
            {
                var invoice = await _repository.InvoiceHeadersRepo.FindByCondition(x => x.Id == id, false)
                    .Include(i=>i.Cashier).Include(i=>i.Branch).Include(i => i.InvoiceDetails).FirstOrDefaultAsync();
                if (invoice == null)
                {
                   throw new Exception("invoice is not found");
                }


                return new InvoiceViewModel
                {
                    InvoiceHeader = invoice,
                    InvoiceDetails = invoice.InvoiceDetails.ToList(),

                };
            }
            catch (Exception ex) 
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task<bool> HasInvoicesForCashier(int cashierId)
            => await _repository.InvoiceHeadersRepo.AnyAsync(i => i.CashierId == cashierId);

        public async Task RemoveCashierReferenceFromInvoices(int cashierId)
        {
            var invoices = await _repository.InvoiceHeadersRepo.FindByCondition(i => i.CashierId == cashierId,false).ToListAsync();
            foreach (var invoice in invoices)
            {
                invoice.CashierId = null; // Or set it to a default cashier ID
            }
            await _repository.SaveAsync();
        }

        public async Task UpdateInvoiceAsync(long id, InvoiceViewModel viewModel)
        {
            try
            {
                var invoiceHeader = await _repository.InvoiceHeadersRepo
                    .FindByCondition(i => i.Id == id, trackChanges: true)
                    .Include(i => i.InvoiceDetails) 
                    .FirstOrDefaultAsync();

                if (invoiceHeader == null)
                {
                    throw new ArgumentException("This Invoice doesn't exist");
                }

                _mapper.Map(viewModel.InvoiceHeader, invoiceHeader);
                
                var existingDetails = invoiceHeader.InvoiceDetails.ToList();
               
                var viewModelDetailIds = viewModel.InvoiceDetails.Select(d => d.Id).ToList();

                var detailsToRemove = existingDetails
                    .Where(ed => !viewModelDetailIds.Contains(ed.Id))
                    .ToList();

                _repository.InvoiceDetailsRepo.DeleteRange(detailsToRemove);

                foreach (var detail in viewModel.InvoiceDetails)
                {
                    if (detail.Id == 0)
                    {
                        // New detail
                        detail.InvoiceHeaderId = id;
                        _repository.InvoiceDetailsRepo.Create(detail);
                    }
                    else
                    {
                        // Existing detail
                        var existingDetail = existingDetails
                            .FirstOrDefault(ed => ed.Id == detail.Id);

                        if (existingDetail != null)
                        {
                            // Map updated values to the existing detail
                            existingDetail.ItemName = detail.ItemName;
                            existingDetail.ItemCount = detail.ItemCount;
                            existingDetail.ItemPrice = detail.ItemPrice;
                            _repository.InvoiceDetailsRepo.Update(existingDetail);
                        }
                    }
                }
                
                _repository.InvoiceHeadersRepo.Update(invoiceHeader);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong", ex);
            }
        }



    }
}
