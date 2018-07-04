using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _02._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)", connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = (string)reader[0];
                    var numberOfMonions = (int)reader[1];

                    Console.WriteLine($"{name} - {numberOfMonions}");
                }
            }
        }
    }
}