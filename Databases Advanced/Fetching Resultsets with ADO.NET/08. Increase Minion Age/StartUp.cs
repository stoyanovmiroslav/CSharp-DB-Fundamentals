using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08._Increase_Minion_Age
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionIdsInput = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var parameters = new string[minionIdsInput.Length];

            SqlCommand command = new SqlCommand();
            for (int i = 0; i < minionIdsInput.Length; i++)
            {
                parameters[i] = string.Format("@minionId{0}", i);
                command.Parameters.AddWithValue(parameters[i], minionIdsInput[i]);
            }
            command.CommandText = string.Format("SELECT m.Id, m.Name, m.Age FROM Minions AS m WHERE m.Id IN({0})", string.Join(", ", parameters));
            command.Connection = new SqlConnection(Configuration.ConnectionString);

            command.Connection.Open();

            List<int> minionIds = new List<int>();
            List<string> minionNames = new List<string>();

            using (command.Connection)
            {
                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionIds.Add((int)reader[0]);
                        string name = (string)reader[1];
                        minionNames.Add(name.First().ToString().ToUpper() + name.Substring(1));
                    }
                }

                for (int i = 0; i < minionIds.Count; i++)
                {
                    command = new SqlCommand("UPDATE Minions SET Name = @minionName , Age += 1 WHERE Id = @minionId", command.Connection);
                    command.Parameters.AddWithValue("@minionName", minionNames[i]);
                    command.Parameters.AddWithValue("@minionId", minionIds[i]);
                    command.ExecuteNonQuery();
                }

                command = new SqlCommand("SELECT m.Name, m.Age FROM Minions AS m", command.Connection);
                reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                }
            }
        }
    }
}