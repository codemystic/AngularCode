using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AngularMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        // GET: All books
        public JsonResult GetAllBooks()
        {
            using (MyTestDBContext contextObj = new MyTestDBContext())
            {
                //List<Book> bookList;
                //if (getOnlyLoaned)
                //{
                //    bookList = contextObj.Books.Where(x => x.IsLoaned == "Y").ToList();
                //}
                //else
                //{
                    //bookList = contextObj.Books.ToList();
                //}
                var bookList = contextObj.Books.ToList();
                return Json(bookList, JsonRequestBehavior.AllowGet);
            }
        }
        //GET: Book by Id
        public JsonResult GetBookById(string id)
        {
            using (MyTestDBContext contextObj = new MyTestDBContext())
            {
                var bookId = Convert.ToInt32(id);
                var getBookById = contextObj.Books.Find(bookId);
                return Json(getBookById, JsonRequestBehavior.AllowGet);
            }
        }
        //Update Book
        public string UpdateBook(Book book)
        {
            if (book != null)
            {
                using (MyTestDBContext contextObj = new MyTestDBContext())
                {
                    int bookId = Convert.ToInt32(book.Id);
                    Book _book = contextObj.Books.Where(b => b.Id == bookId).FirstOrDefault();
                    _book.Title = book.Title;
                    _book.Author = book.Author;
                    _book.Publisher = book.Publisher;
                    _book.Isbn = book.Isbn;
                    _book.IsLoaned = book.IsLoaned == "True" ? "Y" : "N";
                    contextObj.SaveChanges();
                    return "Book record updated successfully";
                }
            }
            else
            {
                return "Invalid book record";
            }
        }
        // Add book
        public string AddBook(Book book)
        {
            if (book != null)
            {
                using (MyTestDBContext contextObj = new MyTestDBContext())
                {
                    contextObj.Books.Add(book);
                    contextObj.SaveChanges();
                    return "Book record added successfully";
                }
            }
            else
            {
                return "Invalid book record";
            }
        }
        // Delete book
        public string DeleteBook(string bookId)
        {

            if (!String.IsNullOrEmpty(bookId))
            {
                try
                {
                    int _bookId = Int32.Parse(bookId);

                    using (MyTestDBContext contextObj = new MyTestDBContext())
                    {
                        var _book = contextObj.Books.Find(_bookId);
                        contextObj.Books.Remove(_book);
                        contextObj.SaveChanges();
                        return "Selected book record deleted sucessfully";
                    }
                }
                catch (Exception)
                {
                    return "Book details not found";
                }
            }
            else
            {
                return "Invalid operation";
            }
        }        
    }
}