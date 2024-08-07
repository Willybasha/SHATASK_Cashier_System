using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOS.BranchesModels;

namespace BusinessLayer.Services.BranchesServices
{
    public interface IBranchesService
    {
        Task<IEnumerable<BranchResponse>> GetAllBranches();
        Task<BranchResponse> GetBranchById(int id);

    }
}
