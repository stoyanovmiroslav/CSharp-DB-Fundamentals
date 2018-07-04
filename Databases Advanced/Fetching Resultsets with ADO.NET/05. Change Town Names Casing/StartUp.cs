using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05._Change_Town_Names_Casing
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string country = Console.ReadLine();

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @country", connection);
                command.Parameters.AddWithValue("@country", country);
                int isCoutryValid = (int)command.ExecuteScalar();

                if (isCoutryValid == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    command = new SqlCommand("SELECT t.Id, t.Name FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @country", connection);
                    command.Parameters.AddWithValue("@country", country);
                    var reader = command.ExecuteReader();

                    var cities = new Dictionary<int, string>();

                    while (reader.Read())
                    {2
                        int coutryId = (int)reader[0];
                        string cityName = reader[1].ToString().ToUpper();

                        cities[coutryId] = cityName;
                    }
                    reader.Close();
                   
                    foreach (var city in cities)
                    {
                        command = new SqlCommand("UPDATE Towns SET Name = @cityName WHERE Id = @cityId", connection);
                        command.Parameters.AddWithValue("@cityName", city.Value);
                        command.Parameters.AddWithValue("@cityId", city.Key);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine($"{cities.Values.Count} town names were affected.");

                    Console.WriteLine("[" + string.Join(", ", cities.Values) + "]");
                }
            }
        }
    }
}