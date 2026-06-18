### Problem Statement
Prompt the user to input a list of their favorite movies (one per line).Saves the list into a text file named FavoriteMovies.txt. Now read the file content and display all the movies to the user in uppercase.

### Code
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace CSharpFundamentalsAssign
{
    public static class Assignment6
    {

        public static void PrintList( List<string> movies)
        {
            Console.WriteLine("\nFavorite Movies (Uppercase):");

            foreach (var item in movies)
            {
                Console.WriteLine(item.Trim().ToUpper());
            }
        }
        public static void MakeFile()
        {
            Console.WriteLine("Save your Favorite Movies:");
            var movieList = new List<string>();
            int movieCount = 1;


            //FileInfo file = new FileInfo("FavoriteMovies.txt");

            while (true)
            {
                Console.Write($"Enter Your Favorite Movie Number {movieCount} - type \'exit\' to stop : ");
                var movieName = Console.ReadLine();

                // Check valid input
                if (string.IsNullOrWhiteSpace(movieName))
                {
                    Console.WriteLine("Invalid Input .. Try again");
                    continue;
                }

                // exit condition
                if (movieName.Trim().ToLower() == "exit")
                {
                    Console.WriteLine($"You have entered {movieCount-1} movies..!");
                    Console.WriteLine();
                    break;
                }

  
                movieList.Add(movieName);

                movieCount++;

            }
            //PrintList(movieList);

            string basePath = @"D:\CSharpFundamentalsAssign\CSharpFundamentalsAssign";
            string filePath = basePath + @"\FavoriteMovies.txt";

            File.WriteAllLines(filePath, movieList);
            //File.WriteAllLines(file.FullName, movieList);

            Console.WriteLine("Movies saved succesfully!");

            string[] savedMovies = File.ReadAllLines(filePath);

            //print
            PrintList(new List<string>(savedMovies));

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Assignment1.CalculateTotal();
            //Assignment2.FindMax();
            //Assignment3.GetThreelargestNums();
            //Assignment4.PrintDescending();
            //Assignment5.GetAge();
            Assignment6.MakeFile();
        }
    }
}

```
