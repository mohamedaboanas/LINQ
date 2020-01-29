using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    internal class Program
    {
        private static void Main()
        {
            // streeming, non streeming
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Movies>
            {
                new Movies {Rate = 8.9f,Title = "The Dark Night", Year = 2008},
                new Movies {Rate = 9.8f, Title = "Cazablanca", Year = 1942},
                new Movies {Rate = 8.9f,Title = "The King's Speach", Year = 2010},
                new Movies {Rate = 9.8f, Title = "Star Ware V", Year = 1980}
            };

            var query = movies.Filter(m => m.Year > 2000).ToList();

            Console.WriteLine(query.Count);

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }

            //var enumrator = query.GetEnumerator();
            //while (enumrator.MoveNext())
            //{
            //    Console.WriteLine(enumrator.Current.Title);
            //}
        }
    }
}
