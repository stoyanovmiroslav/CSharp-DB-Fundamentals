using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username) => By<TModel>(a => a.Username == username).SingleOrDefault();

        public TModel ByUsernameAndPassword<TModel>(string username, string password) => By<TModel>(a => a.Username == username && a.Password == password).SingleOrDefault();

        public bool Exists(int id) => ById<User>(id) != null;

        public bool Exists(string name) => ByUsername<User>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate) =>
             this.context.Users
                 .Include(x => x.FriendsAdded)
                 .Where(predicate)
                 .AsQueryable()
                 .ProjectTo<TModel>();

        public Friendship AcceptFriend(int userId, int friendId)
        {
            Friendship friendship = new Friendship { UserId = userId, FriendId = friendId };

            context.Friendships.Add(friendship);

            context.SaveChanges();

            return friendship;
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            Friendship friendship = new Friendship { UserId = userId, FriendId = friendId };

            context.Friendships.Add(friendship);

            context.SaveChanges();

            return friendship;
        }

        public void ChangePassword(int userId, string password)
        {
            User user = context.Users.SingleOrDefault(x => x.Id == userId);

            user.Password = password;
            context.SaveChanges();
        }

        public void Delete(string username)
        {
            User user = context.Users.FirstOrDefault(x => x.Username == username);

            user.IsDeleted = true;
            context.SaveChanges();
        }

        public User Register(string username, string password, string email)
        {
            User user = new User { Username = username, Password = password, Email = email , IsDeleted = false };

            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }

        public void SetBornTown(int userId, int townId)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == userId);

            user.BornTownId = townId;
            context.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == userId);

            user.CurrentTownId = townId;
            context.SaveChanges();
        }
    }
}