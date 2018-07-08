using P03_FootballBetting.Data;
using System;

namespace P03_FootballBetting
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (FootballBettingContext contex = new FootballBettingContext())
            {
                contex.Database.EnsureDeleted();
            }
        }
    }
}
