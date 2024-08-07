using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOS.BranchesModels;
using CoreLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.BranchesServices
{
    public class BranchesService : IBranchesService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public BranchesService(IRepositoryManager invoiceRepository,IMapper mapper)
        {
            _repository = invoiceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BranchResponse>> GetAllBranches()
        {
            try
            {
                var branches = await _repository.BranchesRepo.FindAll(false).ToListAsync();

                var mapped = _mapper.Map<IEnumerable<BranchResponse>>(branches);

                return mapped;
            }
            catch (Exception ex) 
            {
                throw new Exception("Something went wrong ");
            }
        }

        public async Task<BranchResponse> GetBranchById(int id)
        {
            try 
            {
                var branch = await _repository.BranchesRepo.FindByCondition(b => b.Id == id,false).FirstOrDefaultAsync();

                return _mapper.Map<BranchResponse>(branch);
            }
            catch (Exception ex)
            {
                throw new Exception("Somthing Went Wrong");
            }
        }
    }
}
