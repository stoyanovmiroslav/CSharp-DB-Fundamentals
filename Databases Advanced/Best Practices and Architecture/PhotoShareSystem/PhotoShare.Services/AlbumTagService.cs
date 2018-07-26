using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class AlbumTagService : IAlbumTagService
    {
        private readonly PhotoShareContext context;

        public AlbumTagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumTag AddTagTo(int albumId, int tagId)
        {
            AlbumTag albumTag = new AlbumTag { AlbumId = albumId, TagId = tagId };

            context.AlbumTags.Add(albumTag);

            context.SaveChanges();

            return albumTag;
        }

        public bool Exists(int albumId, int tagId) => ByIds<AlbumTag>(albumId, tagId) != null;

        public TModel ByIds<TModel>(int albumId, int tagId) => By<TModel>(a => a.TagId == tagId && a.AlbumId == albumId).SingleOrDefault();

        private IEnumerable<TModel> By<TModel>(Func<AlbumTag, bool> predicate) =>
            this.context.AlbumTags
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}