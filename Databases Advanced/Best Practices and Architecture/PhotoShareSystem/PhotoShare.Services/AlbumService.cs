using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => By<TModel>(x => x.Name == name).SingleOrDefault();

        public bool Exists(int id) => ById<Album>(id) != null;

        public bool Exists(string name) => ByName<Album>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate) =>
            this.context.Albums
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

        public Album Create(int userId, string albumTitle, Color backgroundColor, string[] tags)
        {
            Tag[] tagsEntyties = context.Tags.Where(x => tags.Contains(x.Name)).ToArray();

            Album album = new Album { Name = albumTitle, BackgroundColor = backgroundColor };

            context.Albums.Add(album);

            for (int i = 0; i < tagsEntyties.Count(); i++)
            {
                AlbumTag albumTag = new AlbumTag { AlbumId = album.Id, TagId = tagsEntyties[i].Id };
                album.AlbumTags.Add(albumTag);
            }

            context.SaveChanges();
            return album;
        }
    }
}