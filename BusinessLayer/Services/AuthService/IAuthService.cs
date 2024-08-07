using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOS.AuthModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
    }
}
