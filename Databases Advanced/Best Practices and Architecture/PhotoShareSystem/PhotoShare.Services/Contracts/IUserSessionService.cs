using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services.Contracts
{
    public interface IUserSessionService
    {
        void LogIn(string username);

        void LogOut();

        bool IsLoggedIn();

        string WhoAmI();
    }
}