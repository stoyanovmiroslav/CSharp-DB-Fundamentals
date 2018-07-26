namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Services.Contracts;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public AddFriendCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string userName = data[0];
            string friendName = data[1];

            bool isUserExist = userService.Exists(userName);
            bool isFriendExist = userService.Exists(friendName);

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            if (!isUserExist)
            {
                throw new ArgumentException($"{userName} not found!");
            }

            if (!isFriendExist)
            {
                throw new ArgumentException($"{friendName} not found!");
            }

            int userId = userService.ByUsername<UserDto>(userName).Id;
            int friendId = userService.ByUsername<UserDto>(friendName).Id;

            UserFriendsDto userFriendsDto = userService.ById<UserFriendsDto>(userId);

            bool isFriendAdded = userFriendsDto.Friends.Any(x => x.Username == friendName);

            if (isFriendAdded)
            {
                throw new InvalidOperationException($"{friendName} is already a friend to {userName}");
            }

            userService.AddFriend(userId, friendId);

            return $"Friend {friendName} added to {userName}";
        }
    }
}