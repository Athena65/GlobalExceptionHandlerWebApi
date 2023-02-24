using GlobalExceptionHandlerWebApi.Models;

namespace GlobalExceptionHandlerWebApi.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<Book> UpdateBook(int id,Book book);
        Task<Book> DeleteBook(int id);    
        Task<Book> CreateBook(Book book);
        //get by name 
    }
}
