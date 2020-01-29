using System;

namespace Queries
{
    public class Movies
    {
        public string Title { get; set; }
        public float Rate { get; set; }

        private int _year;
        public int Year
        {
            get
            {
                Console.WriteLine($"Returning {_year} for {Title}");
                return _year;
            }
            set { _year = value; }
        }

    }
}