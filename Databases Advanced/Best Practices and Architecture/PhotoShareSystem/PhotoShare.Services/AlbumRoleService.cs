namespace PhotoShare.Services
{
    using System;

    using Models;
    using Models.Enums;
    using Data;
    using Contracts;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;

    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, Role role)
        {
            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = role
            };

            this.context.AlbumRoles.Add(albumRole);

            this.context.SaveChanges();

            return albumRole;
        }

        public bool Exists(int albumId, int userId) => ByIds<AlbumTag>(albumId, userId) != null;

        public TModel ByIds<TModel>(int albumId, int userId) => By<TModel>(a => a.UserId == userId && a.AlbumId == albumId).SingleOrDefault();

        private IEnumerable<TModel> By<TModel>(Func<AlbumRole, bool> predicate) =>
            this.context.AlbumRoles
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}
