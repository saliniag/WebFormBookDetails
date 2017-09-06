using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog260_project
{
    public class Book
    {
        private int _ISBN;
        private string _title;
        private string _author;
        private int _rating;
        private int _year;

        public int ISBN
        {

            get { return _ISBN; }

            set { _ISBN = value; }
        }

        //for the title
        public string Title
        {

            get { return _title; }

            set {_title = value; }
        }

        //author

        public string Author
        {

            get { return _author; }

            set { _author = value; }
        }

        //rating
        public int Rating
        {

            get { return _rating; }

            set { _rating = value; }
        }

        //year
        public int Year
        {

            get { return _year; }

            set {_year = value; }
        }


    }
}
