using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos.Export;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var post = context.Posts.Where(x => x.Comments.Count == 0)
                                     .Select(x => new
                                     {
                                         Id = x.Id,
                                         Picture = x.Picture.Path,
                                         User = x.User.Username
                                     })
                                     .OrderBy(x => x.Id)
                                     .ToArray();

            var jsonString = JsonConvert.SerializeObject(post);

            return jsonString;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users.Include(x => x.Posts).ThenInclude(a => a.Comments)
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                 .Select(u => new
                 {
                     u.Username,
                     Followers = u.Followers.Count
                 })
                 .ToArray();

            var jsonString = JsonConvert.SerializeObject(users);

            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users.Select(x => new UserDto
            {
                Username = x.Username,
                MostComments = x.Posts.Count > 0 ? x.Posts.Max(a => a.Comments.Count) : 0
            })
            .OrderByDescending(x => x.MostComments)
            .ThenBy(x => x.Username)
            .ToArray();

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));

            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");

            StringBuilder sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }
    }
}