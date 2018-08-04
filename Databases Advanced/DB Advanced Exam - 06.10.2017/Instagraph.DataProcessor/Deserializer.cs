using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.Dtos.Import;


namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var pictures = JsonConvert.DeserializeObject<List<Picture>>(jsonString);

            StringBuilder stringBuilder = new StringBuilder();

            List<Picture> validPictures = new List<Picture>();

            foreach (var picture in pictures)
            {
                if (!IsValid(picture))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var isPathExist = context.Pictures.Any(x => x.Path == picture.Path);

                if (isPathExist || validPictures.Any(x => x.Path == picture.Path))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                stringBuilder.AppendLine($"Successfully imported Picture {picture.Path}.");
                validPictures.Add(picture);
            }

            context.Pictures.AddRange(validPictures);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);

            StringBuilder stringBuilder = new StringBuilder();

            List<User> users = new List<User>();

            foreach (var userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var profilePicture = context.Pictures.FirstOrDefault(x => x.Path == userDto.ProfilePicture);

                var isUserExist = context.Users.Any(x => x.Username == userDto.Username);

                var isUserAdded = users.Any(x => x.Username == userDto.Username);

                if (profilePicture == null || isUserAdded || isUserExist)
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = new User
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    ProfilePicture = profilePicture
                };

                users.Add(user);
                stringBuilder.AppendLine($"Successfully imported User {user.Username}.");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var userFollowerDtos = JsonConvert.DeserializeObject<List<UserFollowerDto>>(jsonString);

            List<UserFollower> userFollowers = new List<UserFollower>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var userFollowerDto in userFollowerDtos)
            {
                if (!IsValid(userFollowerDto))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.FirstOrDefault(x => x.Username == userFollowerDto.User);
                var follower = context.Users.FirstOrDefault(x => x.Username == userFollowerDto.Follower);

                if (user == null || follower == null)
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var isUserFollowerExist = context.UserFollowers.Any(x => x.UserId == user.Id && x.FollowerId == follower.Id);

                if (isUserFollowerExist || userFollowers.Any(x => x.User.Username == user.Username && x.Follower.Username == follower.Username))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var userFollower = new UserFollower { Follower = follower, User = user };

                userFollowers.Add(userFollower);
                stringBuilder.AppendLine($"Successfully imported Follower {follower.Username} to User {user.Username}.");
            }

            context.UserFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PostDto[]), new XmlRootAttribute("posts"));

            var postDtos = (PostDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Post> validPosts = new List<Post>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var postDto in postDtos)
            {
                if (!IsValid(postDto))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.FirstOrDefault(x => x.Username == postDto.User);
                var picture = context.Pictures.FirstOrDefault(x => x.Path == postDto.Picture);

                if (user == null || picture == null)
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var post = new Post { User = user, Picture = picture, Caption = postDto.Caption };
                validPosts.Add(post);

                stringBuilder.AppendLine($"Successfully imported Post {post.Caption}.");
            }

            context.Posts.AddRange(validPosts);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CommentDto[]), new XmlRootAttribute("comments"));

            var commentDtos = (CommentDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Comment> validComments = new List<Comment>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var commentDto in commentDtos)
            {
                if (!IsValid(commentDto))
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.FirstOrDefault(x => x.Username == commentDto.User);

                var post = context.Posts.FirstOrDefault(x => x.Id == int.Parse(commentDto.post.Id));

                if (user == null || post == null)
                {
                    stringBuilder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var comment = new Comment { User = user, Post = post, Content = commentDto.Content };

                validComments.Add(comment);
                stringBuilder.AppendLine($"Successfully imported Comment {comment.Content}.");
            }

            context.Comments.AddRange(validComments);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}