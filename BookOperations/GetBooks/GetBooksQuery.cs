using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            List<Book> bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = (from book in bookList
                                       select new BooksViewModel()
                                           {
                                               Title = book.Title,
                                               PageCount = book.PageCount,
                                               Genre = ((GenreEnum)book.GenreId).ToString(),
                                               PublishDate = book.PublishDate
                                           }
                                       ).ToList();
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
    }

}
