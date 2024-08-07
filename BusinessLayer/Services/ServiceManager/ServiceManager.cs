using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services.AuthService;
using BusinessLayer.Services.BranchesServices;
using BusinessLayer.Services.CashierServices;
using BusinessLayer.Services.InvoiceDetailServices;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Services.ServiceManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICashierServices> _CashierServices;
        private readonly Lazy<IInvoiceServices> _InvoiceServices;
        private readonly Lazy<IBranchesService> _BranchesServices;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _CashierServices= new Lazy<ICashierServices>(() => new CashierService(repositoryManager,mapper));
            _InvoiceServices= new Lazy<IInvoiceServices>(()=> new InvoiceServices(repositoryManager,mapper));
            _BranchesServices = new Lazy<IBranchesService>(() => new BranchesService(repositoryManager,mapper));
            _authService = new Lazy<IAuthService>(() => new AuthServices(userManager, signInManager,mapper));
        }

        public ICashierServices CashierServices => _CashierServices.Value;
        public IInvoiceServices InvoiceServices => _InvoiceServices.Value;
        public IBranchesService BranchService => _BranchesServices.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
