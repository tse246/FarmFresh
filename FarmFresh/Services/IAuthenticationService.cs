using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using FarmFresh.Model;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Services
{
    public interface IAuthenticationService
    {
        public string GenerateJwtToken(User user);

        public User AuthenticateUser(UserLogin userLogin);

        public bool ValidateJwtToken(string token);
    }
}
