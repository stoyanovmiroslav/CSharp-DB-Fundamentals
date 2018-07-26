namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;
        private readonly IUserSessionService userSessionService;

        public ModifyUserCommand(IUserService userService, ITownService townService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.townService = townService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string value = data[2];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            bool isUserExist = userService.Exists(username);

            if (!isUserExist)
            {
                throw new ArgumentException("User [username] not found!");
            }

            int userId = userService.ByUsername<User>(username).Id;

            if (property == "Password")
            {
                SetPassword(userId, value);
            }
            else if (property == "BornTown")
            {
                SetBornTown(userId, value);
            }
            else if (property == "CurrentTown")
            {
                SetCurrentTown(userId, value);
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            return $"User {username} {property} is {value}.";
        }

        private void SetCurrentTown(int userId, string town)
        {
            bool isTownExist = townService.Exists(town);

            if (!isTownExist)
            {
                throw new ArgumentException($"Value {town} not valid.\nTown {town} not found!");
            }

            int townId = townService.ByName<Town>(town).Id;

            userService.SetCurrentTown(userId, townId);
        }

        private void SetBornTown(int userId, string town)
        {
            bool isTownExist = townService.Exists(town);

            if (!isTownExist)
            {
                throw new ArgumentException($"Value {town} not valid.\nTown {town} not found!");
            }

            int townId = townService.ByName<Town>(town).Id;

            userService.SetBornTown(userId, townId);
        }

        private void SetPassword(int userId, string value)
        {
            bool isContainsLowercase = value.Any(x => char.IsLower(x));
            bool isContainsDigit = value.Any(x => char.IsDigit(x));

            if (!isContainsLowercase || !isContainsDigit)
            {
                throw new ArgumentException($"Value {value} not valid.\nInvalid Password");
            }

            userService.ChangePassword(userId, value);
        }
    }
}