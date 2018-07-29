namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using Services.Contracts;
    using PhotoShare.Client.Utilities;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly IUserSessionService userSessionService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).ToArray();

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            bool isUserExist = userService.Exists(username);

            if (!isUserExist)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            int userId = userService.ByUsername<User>(username).Id;

            bool isAlbumExist = albumService.Exists(albumTitle);

            if (isAlbumExist)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var isBackgroundColorExist = Enum.TryParse(bgColor, out Color backgroundColor);

            if (!isBackgroundColorExist)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].ValidateOrTransform();
                TagDto tag = new TagDto { Name = tags[i] };

                bool isTagExist = tagService.Exists(tags[i]);

                if (!isTagExist)
                {
                    throw new ArgumentException("Invalid tags!");
                }
            }

            albumService.Create(userId, albumTitle, backgroundColor, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}