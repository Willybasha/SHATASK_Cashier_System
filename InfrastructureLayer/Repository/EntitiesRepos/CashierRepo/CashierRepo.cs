using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrastructureLayer.Context;
using CoreLayer.Entities;
using InfrastructureLayer.Repository.GenericRepo;
using CoreLayer.Interfaces;

namespace InfrastructureLayer.Repository.EntitiesRepos.CashierRepo
{
    public class CashierRepo : RepositoryBase<Cashier>, ICashierRepo
    {
        public CashierRepo(AppDbContext context) : base(context)
        {
        }

    }
}
