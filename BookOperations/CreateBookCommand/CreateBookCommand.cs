using BookStore.DBOperations;
using BookStore.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.CreateBookCommand
{
    public class CreateBookCommand
    {

        public CreateBookModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            Book isThereBook = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title);

            if (isThereBook is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            _dbContext.Books.Add(new Book
            {
                Title = Model.Title,
                GenreId = Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate
            });
            _dbContext.SaveChanges();
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
