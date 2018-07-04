using _01.InitialSetup;
using System;
using System.Data.SqlClient;

namespace _09._Increase_Age_Stored_Procedure
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("EXEC usp_GetOlder @minionId", connection);
                command.Parameters.AddWithValue("@minionId", minionId);
                command.ExecuteNonQuery();

                command = new SqlCommand("SELECT m.Name, m.Age FROM Minions AS m WHERE Id = @minionId", connection);
                command.Parameters.AddWithValue("@minionId", minionId);
                var reader = command.ExecuteReader();

                reader.Read();
                Console.WriteLine($"{reader[0]} – {reader[1]} years old");
            }
        }
    }
}