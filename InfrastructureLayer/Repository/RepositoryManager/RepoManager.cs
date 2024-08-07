using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Interfaces;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repository.EntitiesRepos.BranchesRepo;
using InfrastructureLayer.Repository.EntitiesRepos.CashierRepo;
using InfrastructureLayer.Repository.EntitiesRepos.InvoiceDetailRepo;
using InfrastructureLayer.Repository.EntitiesRepos.InvoiceDetailsRepo;

namespace InfrastructureLayer.Repository.RepositoryManager
{
    public class RepoManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICashierRepo> _cashierRepo;
        private readonly Lazy<IInvoiceHeaderRepo> _invoiceHeadersRepo;
        private readonly Lazy<IInvoiceDetailRepo> _invoiceDetailRepo;
        private readonly Lazy<IBranchesRepo> _branchesRepo;
        public RepoManager(AppDbContext context)
        {
            _context = context;
            _cashierRepo = new Lazy<ICashierRepo>(() => new CashierRepo(context));
            _invoiceHeadersRepo = new Lazy<IInvoiceHeaderRepo>(() => new InvoiceRepo(context));
            _invoiceDetailRepo = new Lazy<IInvoiceDetailRepo>(() => new InvoiceDetailRepo(context));
            _branchesRepo = new Lazy<IBranchesRepo>(() => new BranchesRepo(context));
        }
        public ICashierRepo CashierRepo => _cashierRepo.Value;
        public IInvoiceHeaderRepo InvoiceHeadersRepo => _invoiceHeadersRepo.Value;
        public IInvoiceDetailRepo InvoiceDetailsRepo => _invoiceDetailRepo.Value;
        public IBranchesRepo  BranchesRepo => _branchesRepo.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
