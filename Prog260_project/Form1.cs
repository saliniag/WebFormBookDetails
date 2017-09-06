using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prog260_project
{
    public partial class Form1 : Form
    {
        BST newClass = new BST();
        Book addThisBook;
       
        public Form1()
        {
            InitializeComponent();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //create a new book
             addThisBook = new Book();
            addThisBook.ISBN = Convert.ToInt32(textBoxISBN.Text);
            addThisBook.Author = textBoxAuthor.Text;
            addThisBook.Title = textBoxTitle.Text;
            addThisBook.Rating = Convert.ToInt32(textBoxRating.Text);
            addThisBook.Year = Convert.ToInt32(textBoxYear.Text);
            int isbn = Convert.ToInt32(textBoxISBN.Text);
            //now my new book is all filled with data
            //addthis in the binary tree

            try
            {
                newClass.Add(isbn, addThisBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBoxAuthor.Text = "";
            textBoxISBN.Text = "";
            textBoxRating.Text = "";
            textBoxTitle.Text = "";
            textBoxYear.Text = "";

           
        
            
        }

        //listing all the isbn in the listbox

        private void button4_Click(object sender, EventArgs e)
        {
           
            List<int> getTheList = newClass.getAllTheISBN();
            listBoxISBN.DataSource = getTheList;

            
           
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            //get the selected isbn from the text box
            int target=0;
            try
            {
                 target = Convert.ToInt32(textBoxISBN.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please enter a valid ISBN. ");
            }
            //get the book with the ISBN
            Book yourBook = newClass.Find(target);

            //fill the text box with the values
            textBoxAuthor.Text = yourBook.Author;

            textBoxRating.Text = yourBook.Rating.ToString();
            textBoxTitle.Text = yourBook.Title;
            textBoxYear.Text = yourBook.Year.ToString();


        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int ISBNtoRemove = Convert.ToInt32(textBoxISBN.Text);
            //remove from the bst tree
            BST theRemovedNode = new BST();

            try
            {
                newClass.remove(ISBNtoRemove);
            }
            catch
            {
                MessageBox.Show("No such ISBN exists in the Book list! ");
            }

            //clean the text box
          textBoxAuthor.Text = "";
          textBoxISBN.Text = "";
          textBoxRating.Text = "";
          textBoxTitle.Text = "";
          textBoxYear.Text = "";
            

        }





    }
}
