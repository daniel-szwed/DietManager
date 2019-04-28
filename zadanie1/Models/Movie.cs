using System;
using System.Text.RegularExpressions;

namespace zadanie1.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int AgeLimit { get; set; }
        public virtual Gener Gener { get; set; }

        public int GetMoviAge()
        {
            return DateTime.Now.Year - Year;
        }
               
        private string _website;

        public string Website
        {
            get { return _website; }
            set {
                var url = value.ToLower();
                var regex = new Regex(@"$http://imdb.com/|imdb.com|http://www.imdb.com/|www.imdb.com/*");
                if (regex.IsMatch(url))
                    _website = url;
                else
                    throw new ArgumentException("Podany adres nie jest poprawną stroną internetową z domeny Imdb.com");
            }
        }
    }
}