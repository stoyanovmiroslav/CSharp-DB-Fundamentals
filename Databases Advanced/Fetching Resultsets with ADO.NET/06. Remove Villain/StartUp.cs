using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _06._Remove_Villain
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT Name FROM Villains WHERE Id = @villainId", connection);
                command.Parameters.AddWithValue("@villainId", villainId);
                string villainName = (string)command.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }

                command = new SqlCommand("DELETE MinionsVillains WHERE VillainId = @villainId", connection);
                command.Parameters.AddWithValue("@villainId", villainId);
                int releasedMinions = (int)command.ExecuteNonQuery();
                
                command = new SqlCommand("DELETE Villains WHERE Id = @villainId", connection);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();  

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{releasedMinions} minions were released.");
            }
        }
    }
}