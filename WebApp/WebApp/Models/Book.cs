using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
        //[DisplayName("Upload Fille")]
        public String Adress { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public String Genre { get; set; }
        public int Price { get; set; }

        public Book(string author, string title, string adress, DateTime releaseDate, string genre, int price)
        {
            Author = author;
            Title = title;
            Adress = adress;
            ReleaseDate = releaseDate;
            Genre = genre;
            Price = price;
        }
        //public HttpStyleUriParser ImageFile { get; set; }




    }
}
