using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    class LoginCommand : ICommand
    {
        private readonly IUserSessionService userSessionService;
        private readonly IUserService userService;

        public LoginCommand(IUserSessionService userSessionService, IUserService userService)
        {
            this.userSessionService = userSessionService;
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            string username = args[0];
            string password = args[1];

            var user = userService.ByUsernameAndPassword<UserDto>(username, password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            bool IsUserLoggedIn = userSessionService.IsLoggedIn();

            if (IsUserLoggedIn)
            {
                throw new ArgumentException("You should logout first!");
            }

            userSessionService.LogIn(username);

            return $"User {username} successfully logged in!";
        }
    }
}