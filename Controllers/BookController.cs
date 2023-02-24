using GlobalExceptionHandlerWebApi.Exceptions;
using GlobalExceptionHandlerWebApi.Models;
using GlobalExceptionHandlerWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GlobalExceptionHandlerWebApi.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooks();
            if(books.IsNullOrEmpty()) 
            {
                throw new NotFoundException($"There is no entites in Database");
            }
            return Ok(books);
                
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var book= await _bookService.GetBookById(id);
            if (book == null)
            {
                throw new NotFoundException($"The id :{id} Not Found in Entites");
            }
            return Ok(book);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Book newBook)
        {
            try
            {
                var book = await _bookService.CreateBook(newBook);
                return Ok(book+$"{newBook.Title} Created!");
            }
            catch (Exception ex)
            {
                throw new ConflictException(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            try
            {
                return Ok(await _bookService.UpdateBook(id, book) + $"{book.Title} Updated!");
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"The id :{id} {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                return Ok(await _bookService.DeleteBook(id) + $"Deleted Successfully!");
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"The id :{id} {ex.Message}");
            }
        }
    }
}
