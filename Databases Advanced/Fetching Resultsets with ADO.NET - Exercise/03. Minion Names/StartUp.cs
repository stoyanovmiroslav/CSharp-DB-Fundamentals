using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _03._Minion_Names
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
                SqlCommand command = new SqlCommand($"SELECT Name FROM Villains WHERE Id = {villainId} ", connection);
                var villainName = (string)command.ExecuteScalar();
                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");

                    command = new SqlCommand($"SELECT m.Name, m.Age, ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum FROM MinionsVillains AS mv JOIN Minions As m ON mv.MinionId = m.Id WHERE mv.VillainId = {villainId} ORDER BY m.Name", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            string minionName = (string)reader["Name"];
                            long rowNum = (long)reader["RowNum"];
                            int minionAge = (int)reader["Age"];

                            Console.WriteLine($"{rowNum}. {minionName} {minionAge}");
                        }
                    }
                }
            }
        }
    }
}