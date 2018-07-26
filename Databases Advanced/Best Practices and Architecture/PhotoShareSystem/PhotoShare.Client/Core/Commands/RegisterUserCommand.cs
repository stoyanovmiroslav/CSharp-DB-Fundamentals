namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public RegisterUserCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are logged in!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            RegisterUserDto userDto = new RegisterUserDto { Username = username, Password = password, Email = email };

            if (!IsValid(userDto))
            {
                throw new ArgumentException("Invalid user!");
            }

            var isUsernameExist = userService.Exists(username);

            if (isUsernameExist)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            userService.Register(username, password, email);

            return $"User {username} was registered successfully!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}