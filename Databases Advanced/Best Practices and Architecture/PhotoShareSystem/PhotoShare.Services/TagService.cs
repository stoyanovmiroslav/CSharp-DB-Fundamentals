using System;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace PhotoShare.Services
{
    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Tag AddTag(string name)
        {
            Tag tag = new Tag { Name = name };

            context.Tags.Add(tag);

            context.SaveChanges();

            return tag;
        }

        public TModel ById<TModel>(int id) => By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => By<TModel>(x => x.Name == name).SingleOrDefault();

        public bool Exists(int id) => ById<Tag>(id) != null;

        public bool Exists(string name) => ByName<Tag>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Tag, bool> predicate) =>
          this.context.Tags
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

    }
}