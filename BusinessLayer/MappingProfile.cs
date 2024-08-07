using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOS.AuthModels;
using BusinessLayer.DTOS.BranchesModels;
using BusinessLayer.DTOS.CashierModels;
using BusinessLayer.DTOS.InvoiceDetailsModels;
using CoreLayer.Entities;
using InfrastructureLayer.Repository.EntitiesRepos.BranchesRepo;

namespace BusinessLayer
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoiceViewModel, InvoiceHeader>().ReverseMap();
            //CreateMap<InvoiceViewModel, InvoiceDetail>().ReverseMap().ForMember(dest => dest., opt => opt.Ignore());
            CreateMap<Branch, BranchResponse>().ReverseMap();
            CreateMap<Cashier, CashierResponse>().ReverseMap();

            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
        }

    }
}
