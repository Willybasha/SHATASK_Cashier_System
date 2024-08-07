using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOS.AuthModels;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Services.AuthService
{
    public class AuthServices : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public AuthServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                throw new Exception("UserName is already Used , Try to Add more numbers or digits");

            var newUser = _mapper.Map<ApplicationUser>(model);
            newUser.Id = Guid.NewGuid().ToString(); // Ensure the Id is set

            var user = new ApplicationUser
             { 
                 UserName=model.UserName,
                 Email = model.Email
             };

             return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
        }
    }
}

