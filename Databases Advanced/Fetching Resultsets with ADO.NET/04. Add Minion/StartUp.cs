using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _04._Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split();
            string minionName = tokens[1];
            int minionAge = int.Parse(tokens[2]);
            string minionTown = tokens[3];

            tokens = Console.ReadLine().Split();
            string villainName = tokens[1];

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Towns AS t WHERE t.Name = @townName", connection);
                command.Parameters.AddWithValue("@townName", minionTown);
                int isTownExist = (int)command.ExecuteScalar();

                if (isTownExist == 0)
                {
                    command = new SqlCommand("INSERT INTO Towns VALUES (@townName, NULL)", connection);
                    command.Parameters.AddWithValue("@townName", minionTown);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionTown} was added to the database.");\
                }
                command = new SqlCommand("SELECT t.Id FROM Towns AS t WHERE t.Name = @townName", connection);
                command.Parameters.AddWithValue("@townName", minionTown);
                int townId = (int)command.ExecuteScalar();


                command = new SqlCommand("SELECT COUNT(*) FROM villains as v WHERE v.Name = @villainName", connection);
                command.Parameters.AddWithValue("@villainName", villainName);
                int isVillainValid = (int)command.ExecuteScalar(); 

                if (isVillainValid == 0)
                {
                    command = new SqlCommand("INSERT INTO Villains VALUES(@villainName, 4)", connection);
                    command.Parameters.AddWithValue("@villainName", villainName);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database."); 
                }
                command = new SqlCommand("SELECT v.Id FROM villains as v WHERE v.Name = @villainName", connection);
                command.Parameters.AddWithValue("@villainName", villainName);
                int villainId = (int)command.ExecuteScalar();

                command = new SqlCommand("INSERT INTO Minions VALUES (@name, @age, @townId)", connection);
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", minionAge);
                command.Parameters.AddWithValue("@townId", townId);
                command.ExecuteNonQuery();

                command = new SqlCommand("SELECT m.Id FROM minions AS m WHERE m.Name = @minionsName AND m.Age = @minionsAge", connection);
                command.Parameters.AddWithValue("@minionsName", minionName);
                command.Parameters.AddWithValue("@minionsAge", minionAge);
                int minionsId = (int)command.ExecuteScalar();

                command = new SqlCommand("INSERT INTO MinionsVillains VALUES (@minionsId, @villainId)", connection);
                command.Parameters.AddWithValue("@minionsId", minionsId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }
    }
}