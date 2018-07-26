namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Models.Enums;
    using PhotoShare.Services.Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumRoleService albumRoleService;
        private readonly IUserService userService;
        private readonly IAlbumService albumService;
        private readonly IUserSessionService userSessionService;

        public ShareAlbumCommand(IAlbumRoleService albumRoleService, IUserService userService, IAlbumService albumService, IUserSessionService userSessionService)
        {
            this.albumRoleService = albumRoleService;
            this.userService = userService;
            this.albumService = albumService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            if (!userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You are not logged in!");
            }

            bool isAlbumExist = albumService.Exists(albumId);

            if (!isAlbumExist)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            bool isUserExist = userService.Exists(username);

            if (!isUserExist)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            int userId = userService.ByUsername<UserDto>(username).Id;

            bool isPermissionExist = Enum.TryParse(permission, out Role role);

            if (!isPermissionExist)
            {
                throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
            }

            bool isRoleExist = albumRoleService.Exists(albumId, albumId);

            if (isRoleExist)
            {
                throw new ArgumentException("The Role already exist!");
            }

            albumRoleService.PublishAlbumRole(albumId, userId, role);

            string albumName = albumService.ById<AlbumDto>(albumId).Name;

            return $"Username {username} added to album\n{albumName} ({permission})";
        }
    }
}