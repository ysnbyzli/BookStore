using BookStore.DBOperations;
using BookStore.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }

        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            Book book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

    }
}
