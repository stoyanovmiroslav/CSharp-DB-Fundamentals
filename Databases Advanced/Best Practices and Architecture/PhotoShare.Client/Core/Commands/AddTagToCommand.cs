namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using PhotoShare.Services.Contracts;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private readonly IAlbumTagService albumTagService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService, IAlbumTagService albumTagService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumTagService = albumTagService;
        }

        public string Execute(string[] args)
        {
            string albumName = args[0];
            string tag = args[1];

            tag = tag.ValidateOrTransform();

            bool isAlbumExist = albumService.Exists(albumName);
            bool isTagExist = tagService.Exists(tag);

            if (!isAlbumExist || !isTagExist)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            int albumId = albumService.ByName<Album>(albumName).Id;
            int tagId = tagService.ByName<Tag>(tag).Id;

            bool isTadgAdded = albumTagService.Exists(albumId, tagId);

            if (isTadgAdded)
            {
                throw new ArgumentException($"Tag {tag} is already added to album {albumName}!");
            }

            albumTagService.AddTagTo(albumId, tagId);

            return $"Tag {tag} added to {albumName}!";
        }
    }
}