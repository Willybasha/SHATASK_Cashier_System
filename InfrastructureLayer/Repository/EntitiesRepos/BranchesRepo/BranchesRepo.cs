using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repository.GenericRepo;

namespace InfrastructureLayer.Repository.EntitiesRepos.BranchesRepo
{
    public class BranchesRepo : RepositoryBase<Branch>,IBranchesRepo
    {
        public BranchesRepo(AppDbContext context) : base(context)
        {
        }
    }
}
