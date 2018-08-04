using System;
using System.IO;

using Instagraph.Data;
using Instagraph.DataProcessor;
using System.Text;

namespace Instagraph.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ResetDatabase());

            Console.WriteLine(ImportData());

            ExportData();
        }

        private static string ImportData()
        {
            StringBuilder sb = new StringBuilder();

            using (var context = new InstagraphContext())
            {
                string picturesJson = File.ReadAllText("../../../../Instagraph.App/files/input/pictures.json");
                sb.AppendLine(Deserializer.ImportPictures(context, picturesJson));

                string usersJson = File.ReadAllText("../../../../Instagraph.App/files/input/users.json");
                sb.AppendLine(Deserializer.ImportUsers(context, usersJson));

                string followersJson = File.ReadAllText("../../../../Instagraph.App/files/input/users_followers.json");
                sb.AppendLine(Deserializer.ImportFollowers(context, followersJson));

                string postsXml = File.ReadAllText("../../../../Instagraph.App/files/input/posts.xml");
                sb.AppendLine(Deserializer.ImportPosts(context, postsXml));

                string commentsXml = File.ReadAllText("../../../../Instagraph.App/files/input/comments.xml");
                sb.AppendLine(Deserializer.ImportComments(context, commentsXml));
            }

            string result = sb.ToString().Trim();
            return result;
        }

        private static void ExportData()
        {
            using (var context = new InstagraphContext())
            {
                string uncommentedPostsOutput = Serializer.ExportUncommentedPosts(context);
                File.WriteAllText("../../../../Instagraph.App/files/output/UncommentedPosts.json", uncommentedPostsOutput);

                string usersOutput = Serializer.ExportPopularUsers(context);
                File.WriteAllText("../../../../Instagraph.App/files/output/PopularUsers.json", usersOutput);

                string commentsOutput = Serializer.ExportCommentsOnPosts(context);
                File.WriteAllText("../../../../Instagraph.App/files/output/CommentsOnPosts.xml", commentsOutput);
            }
        }
        
        private static string ResetDatabase()
        {
            using (var context = new InstagraphContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return $"Database reset succsessfully.";
        }
    }
}