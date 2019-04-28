using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zadanie1.Models;

namespace zadanie1
{
    class Program
    {
        //programie stwórz listę filmów (obiektów typu Movie), do listy dodaj 5 filmów (dowolnych), w tym trzech obiektów klasy ChildMovie. 

        static void Main(string[] args)
        {
            var movies = new List<Movie>() {
                new Movie() {Name = "Droga do szczęścia", Gener = Gener.Drama, Year = 2008, AgeLimit = 12},
                new Movie() {Name = "Apartament", Gener = Gener.Drama, Year = 2004, AgeLimit = 12},
                new Movie() {Name = "Zielona mila", Gener = Gener.Drama, Year = 1999, AgeLimit = 12},
                new Movie() {Name = "Lot nad kukułczym gniazdem", Gener = Gener.Drama, Year = 1975, AgeLimit = 16},
                new Movie() {Name = "Świat dzikiego zachodu", Gener = Gener.Drama, Year = 1973, AgeLimit = 16}
            };
            //Console.WriteLine("Podaj nazwę filmu: ");
            //var name = Console.ReadLine();
            //Console.WriteLine("Podaj rok produkcji");
            //var year = Console.ReadLine();
            //Console.WriteLine("Podaj limit wieku:");
            //var ageLimit = Console.ReadLine();
            //Console.WriteLine("Podaj gatunek");
            //var gener = Console.ReadLine();

            //ValidYear(year);
            //ValidAgeLimit(ageLimit);
            //ValidGener(gener);

            /**
             * Ćwiczenia 2
             */
            
            int childAgeInt;
            string childAgeString = string.Empty;

            while(!GetAgeLimit(childAgeString, out childAgeInt))
            {
                Console.WriteLine("Podaj wiek dziecka:");
                childAgeString = Console.ReadLine();
            }

            if(GetAgeLimit(childAgeString, out childAgeInt))
            {
                var movies4children = movies.Where(m => m.AgeLimit <= childAgeInt).Select(m => m.Name).ToList();
                movies4children.ForEach(m =>
                {
                    Console.WriteLine(m.ToString());
                });
                var moviesJson = JsonConvert.SerializeObject(movies4children);
                using (StreamWriter writer = new StreamWriter("movies.txt"))
                {
                    writer.WriteLine(moviesJson);
                }
            }

            using(StreamReader reader = new StreamReader("noweFilmy.txt"))
            {
                movies.AddRange(JsonConvert.DeserializeObject<List<Movie>>(reader.ReadToEnd()));
            }

            Console.ReadKey();
        }

        private static bool ValidYear(string year)
        {
            int yearInt = 0;
            var parsingSucceded = int.TryParse(year, out yearInt);
            if (!parsingSucceded)
                return false;
            var date = new DateTime(yearInt, 1, 1);
            return date.Year >= 1990;
        }

        private static bool ValidAgeLimit(string ageLimit)
        {
            int age;
            var parsingSucceded = int.TryParse(ageLimit, out age);
            if (!parsingSucceded)
                return false;
            return (age >= 0) && (age <= 100);
        }

        private static bool GetAgeLimit(string ageLimit, out int ageLimitInt)
        {
            ageLimitInt = 0;
            int age;
            bool parsingSucceded = int.TryParse(ageLimit, out age);
            if (parsingSucceded)
            {
                ageLimitInt = age;
                return true;
            }
            else
                return false;
        }

        private static bool ValidGener(string gener)
        {
            foreach (var item in Enum.GetValues(typeof(Gener)))
            {
                if (item.ToString() == gener)
                    return true;
            }
            return false;
        }

        private static bool IfUserCanWatchMovie(int personAge, Movie movie)
        {
            return personAge >= movie.AgeLimit;
        }
    }
}
