using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace DA05_Miniprojekt
{

    public class Program
    {

        private static AppDbContext database;
        private static User currentUser;
        

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            using (database = new AppDbContext())
            {
                Utils.WriteHeading("Welcome to Console Chat 3000");

                int selected = Utils.ShowMenu("", new string[] { "Sign In", "Create Account" });

                if (selected == 0) SignIn("Sign In:");
                if (selected == 1) CreateAccount();

            }




        }

        private static void CreateAccount()
        {
            var userName = Utils.ReadString("Username:");

            string password = Utils.ReadPassword("Password:");

            var user = new User
            {
                Name = userName,
                Password = password
            };


            try
            {
                database.Users.Add(user);
                database.SaveChanges();
            }
            catch 
            {
                Console.WriteLine();
                Console.WriteLine("Username not available. Please try another.");
                Console.WriteLine();
                CreateAccount();
            }
          


            Console.Clear();
            Console.WriteLine("Account Created.");
            SignIn("Sign In:");
        }



        private static void SignIn(string prompt)
        {
            Console.Clear();
            Utils.WriteHeading(prompt);
            string userName = Utils.ReadString("Username:");
          
            try
            {
                currentUser = database.Users.Single(u => u.Name == userName);
            }
            catch
            {
                SignIn("User name \"" + userName + "\" not found, please try again:");
            }
            

            string password = Utils.ReadPassword("Password:");
            if (password == currentUser.Password)
            {
                Console.Clear();
                Console.WriteLine("You have logged in.");
                StartBulletin();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Incorrect password.");
                Main();
            }
        }

        private static void StartBulletin()
        {
            bool running = true;
            while (running)
            {
                int selected = Utils.ShowMenu("What would you like to do?", new[]
                {
                    "Create Post",
                    "List All Posts",
                    "Exit"
                });

                if (selected == 0)
                {
                    AddBulletin();
                }
                else if (selected == 1)
                {
                    ListBulletin();
                }
                else
                {
                    running = false;
                    Console.WriteLine("Goodbye!");
                }
            }
        }

        private static void ListBulletin()
        {
            Console.Clear();
            Utils.WriteHeading("All Posts");

            if (!database.Posts.Any())
            {
                Console.WriteLine("There are no posts to display.");
            }

            var posts = database.Posts.Include(p => p.User).OrderBy(p => p.DateTime).ToArray();

            int selected = Utils.ShowMenu("", posts.Select(p => p.Title + " by: " + p.User.Name).ToArray());

            var post = posts[selected];

            Console.Clear();
            Utils.WriteHeading(post.Title);
            Console.WriteLine(post.Text);
            Console.WriteLine();
            Utils.WriteHeading("Posted by " + post.User.Name +" at " +post.DateTime.ToString("G"));

            
        }



        private static void AddBulletin()
        {
            Utils.WriteHeading("Add Bulletin");

            string title = Utils.ReadString("Title:");
            string text = Utils.ReadString("Text:");
            var post = new Post
            {
                Title = title,
                Text = text,
                User = currentUser,
                DateTime = DateTime.Now
            };
            database.Posts.Add(post);
            database.SaveChanges();

            Console.WriteLine("Your post has been added!");
        }

    }


}


