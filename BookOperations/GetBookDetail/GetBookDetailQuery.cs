using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public int BookId;

        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null) throw new InvalidOperationException("Kitap bulunamadı");
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
