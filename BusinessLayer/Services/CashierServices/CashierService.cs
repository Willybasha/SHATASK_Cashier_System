using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOS.BranchesModels;
using BusinessLayer.DTOS.CashierModels;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.CashierServices
{
    public class CashierService : ICashierServices
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CashierService(IRepositoryManager invoiceRepository,IMapper mapper)
        {
            _repository = invoiceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CashierViewModel>> GetAllAsync()
        {
            var cashiers = await _repository.CashierRepo.FindAll(false).Include(b=>b.Branch).ToListAsync();
            return cashiers.Select(c => new CashierViewModel
            {
                Id = c.Id,
                CashierName = c.CashierName,
                BranchName = c.Branch.BranchName
            });
        }
        public async Task<IEnumerable<CashierResponse>> GetCashierByBranchId(int branchId)
        {
            try
            {
                var cashiersPerBranch = await _repository.CashierRepo.FindByCondition(c => c.BranchId == branchId, false).ToListAsync();

                return _mapper.Map<IEnumerable<CashierResponse>>(cashiersPerBranch);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task<CashierViewModel> GetByIdAsync(int id)
        {
            var cashier = await _repository.CashierRepo.FindByCondition(c=>c.Id==id,false).Include(b=>b.Branch).FirstOrDefaultAsync();
            if (cashier == null)
                throw new Exception("Not found");
            return new CashierViewModel
            {
                Id = cashier.Id,
                CashierName = cashier.CashierName,
                BranchName = cashier.Branch.BranchName
            };
        }

        public async Task CreateAsync(CashierViewModel cashierdto)
        {
            var cashier = new Cashier
            {
                CashierName = cashierdto.CashierName,
                BranchId = cashierdto.BranchId
            };
            _repository.CashierRepo.Create(cashier);

            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, CashierViewModel cashierDto)
        {
            var cashier = await _repository.CashierRepo.FindByCondition(c => c.Id==id, false).FirstOrDefaultAsync();
            if (cashier == null)
                throw new Exception("Not Found");

            cashier.CashierName = cashierDto.CashierName;
            cashier.BranchId = cashierDto.BranchId;

            _repository.CashierRepo.Update(cashier);
            await _repository.SaveAsync();
        }

        
        public async Task DeleteAsync(int id)
        {
            var cashier = await _repository.CashierRepo.FindByCondition(c => c.Id==id, false).FirstOrDefaultAsync();
            if (cashier == null)
                throw new Exception("Not Found");

            _repository.CashierRepo.Delete(cashier);
            await _repository.SaveAsync();

            
        }
    }
}
