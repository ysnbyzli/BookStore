using BookStore.BookOperations.CreateBookCommand;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using BookStore.Entitiy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                query.Handle();
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
            return Ok(result);
            
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    Book book = books.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult Create([FromBody] CreateBookModel book)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = book;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);               
            }
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.updateBookModel = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
