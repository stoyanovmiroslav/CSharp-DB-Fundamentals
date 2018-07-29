using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IUserSessionService userSessionService;

        public LogoutCommand(IUserSessionService userSessionService)
        {
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            bool isUserLoggedIn = userSessionService.IsLoggedIn();

            if (!isUserLoggedIn)
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }

            string username = userSessionService.WhoAmI();

            userSessionService.LogOut();

            return $"User {username} successfully logged out!";
        }
    }
}