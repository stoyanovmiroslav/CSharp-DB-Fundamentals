namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public DeleteUserCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string username = data[0];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            var userExists = this.userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var user = this.userService.ByUsername<UserDto>(username);

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            userService.Delete(username);

            return $"User {username} was deleted from the database!";
        }
    }
}