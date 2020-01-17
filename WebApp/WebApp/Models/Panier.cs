using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
        public String Adress { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public String Genre { get; set; }
        public int Price { get; set; }

        public Panier( string author, string title, string adress, DateTime releaseDate, string genre, int price)
        {
           
            Author = author;
            Title = title;
            Adress = adress;
            ReleaseDate = releaseDate;
            Genre = genre;
            Price = price;
        }
    }
}
