using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;
using System;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class PrintFriendsListCommand : ICommand
    {
        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            string username = args[0];

            bool isUserExist = userService.Exists(username);

            if (!isUserExist)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friends = userService.ByUsername<UserFriendsDto>(username).Friends;

            StringBuilder stringBuilder = new StringBuilder(); 

            if (friends.Count == 0)
            {
                stringBuilder.AppendLine( "No friends for this user. :(");
            }
            else
            {
                stringBuilder.AppendLine("Friends:");
            }

            foreach (var friend in friends)
            {
                stringBuilder.AppendLine($"-{friend.Username}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}