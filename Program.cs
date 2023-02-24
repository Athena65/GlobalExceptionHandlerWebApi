using GlobalExceptionHandlerWebApi.Data;
using GlobalExceptionHandlerWebApi.Middleware;
using GlobalExceptionHandlerWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(config.GetConnectionString("Connection"))); //Database
builder.Services.AddScoped<IBookService,BookService>();//DI
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
