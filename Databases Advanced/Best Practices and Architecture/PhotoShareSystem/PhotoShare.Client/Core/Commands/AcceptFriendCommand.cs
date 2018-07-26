namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Services.Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public AcceptFriendCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string userName = data[0];
            string friendName = data[1];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            bool isUserExist = userService.Exists(userName);
            bool isFriendExist = userService.Exists(friendName);

            if (!isUserExist)
            {
                throw new ArgumentException($"{userName} not found!");
            }

            if (!isFriendExist)
            {
                throw new ArgumentException($"{friendName} not found!");
            }
         
            UserFriendsDto userDto = userService.ByUsername<UserFriendsDto>(userName);
            UserFriendsDto FriendsDto = userService.ByUsername<UserFriendsDto>(friendName);

            bool isUserRequestSend = userDto.Friends.Any(x => x.Username == friendName);
            bool isFriendRequestSend = FriendsDto.Friends.Any(x => x.Username == userName);

            if (isUserRequestSend && isFriendRequestSend)
            {
                throw new InvalidOperationException($"{friendName} is already a friend to {userName}");
            }
            else if (!isFriendRequestSend)
            {
                throw new InvalidOperationException($"{friendName} has not added {userName} as a friend");
            }

            int userId = userService.ByUsername<UserDto>(userName).Id;
            int friendId = userService.ByUsername<UserDto>(friendName).Id;

            userService.AcceptFriend(userId, friendId);

            return $"{userName} accepted {friendName} as a friend";
        }
    }
}