namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Dtos;
    using Contracts;
    using Services.Contracts;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;
        private readonly IUserSessionService userSessionService;

        public AddTownCommand(ITownService townService, IUserSessionService userSessionService)
        {
            this.townService = townService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("Invalid credentials!");
            }

            var townExists = this.townService.Exists(townName);

            if (townExists)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            var town = this.townService.Add(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}