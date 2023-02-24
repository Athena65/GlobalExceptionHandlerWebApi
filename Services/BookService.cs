using GlobalExceptionHandlerWebApi.Data;
using GlobalExceptionHandlerWebApi.Exceptions;
using GlobalExceptionHandlerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandlerWebApi.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;

        public BookService(DataContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateBook(Book book)
        {
            await _context.Books.AddAsync(book);
            if (book.Id != 0)
            {
                throw new ConflictException($"The ID property must be null!");
            }
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var book= await _context.Books.Where(x=>x.Id==id).FirstOrDefaultAsync();  
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();  
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> UpdateBook(int id, Book book)
        {
            var books= await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            books.Title=book.Title;
            books.Author=book.Author;
            books.Description=book.Description;
            books.Pages=book.Pages; 
            books.ISBN13=book.ISBN13;
            await _context.SaveChangesAsync();
            return books;   
        }
    }
}
