using _01.InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07._Print_All_Minion_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                List<string> names = new List<string>();

                SqlCommand command = new SqlCommand("SELECT m.Name FROM Minions AS m", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader[0].ToString());
                }

                int counterFirstRecord = 0;
                int counterLastRecord = names.Count - 1;

                for (int i = 0; i < names.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine(names[counterFirstRecord]);
                        counterFirstRecord++;
                    }
                    else
                    {
                        Console.WriteLine(names[counterLastRecord]);
                        counterLastRecord--;
                    }
                }
            }
        }
    }
}
