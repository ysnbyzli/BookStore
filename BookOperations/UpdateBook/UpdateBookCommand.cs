using BookStore.DBOperations;
using BookStore.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel updateBookModel;

        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            Book book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            book.GenreId = updateBookModel.GenreId != default ? updateBookModel.GenreId : book.GenreId;
            //book.PageCount = updateBookModel.PageCount != default ? updateBookModel.PageCount : book.PageCount;
            //book.PublishDate = updateBookModel.PublishDate != default ? updateBookModel.PublishDate : book.PublishDate;
            book.Title = updateBookModel.Title != default ? updateBookModel.Title : book.Title;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
