using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace PhotoShare.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Town Add(string townName, string countryName)
        {
            bool isTownExist = context.Towns.Any(x => x.Name == townName && x.Country == countryName);

            if (isTownExist)
            {   
                throw new ArgumentException($"Town {townName} was already added!");
            }

            Town town = new Town { Name = townName, Country = countryName };

            this.context.Towns.Add(town);
            context.SaveChanges();

            return town;
        }

        public TModel ById<TModel>(int id) => By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => By<TModel>(x => x.Name == name).SingleOrDefault();

        public bool Exists(int id) => ById<Town>(id) != null;

        public bool Exists(string name) => ByName<Town>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate) =>
            this.context.Towns
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();
    }
}