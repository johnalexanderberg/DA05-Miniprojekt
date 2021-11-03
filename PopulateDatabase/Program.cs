using DA05_Miniprojekt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;



namespace PopulateDatabase
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;


    
            using var database = new AppDbContext();
           

            string[] lines = File.ReadAllLines("Users.csv");

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string userName = parts[0];
                string password = parts[1];
                var user = new User
                {
                    Name = userName,
                    Password = password
                };
                database.Users.Add(user);
            }
            database.SaveChanges();

            Console.WriteLine();
        }
    }
}