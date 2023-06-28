using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NhonVTT_OJT_MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

string databaseConnection = "Server=localhost;Port=5432;Database=minimalbookapi;User Id=postgres;Password=123;";
// string databaseConnectionDocker = "Server=postgres;Port=5432;Database=minimalbookapi;User Id=postgres;Password=123;";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(databaseConnection);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/books", (ApplicationDbContext context)
=> context.Books.ToList());

app.MapGet("/books/{id}", (int id, ApplicationDbContext context)
    => context.Books.FirstOrDefault(x => x.Id == id));

app.MapPost("/books", (BookDTO request, IMapper mapper, ApplicationDbContext context) =>
{
    Book book = mapper.Map<Book>(request);

    context.Add(book);
    context.SaveChanges();
    return book;
});

app.MapPut("/books", (Book request, IMapper mapper, ApplicationDbContext context) =>
{
    var book = context.Books.AsNoTracking().FirstOrDefault(x => x.Id == request.Id) 
        ?? throw new ArgumentNullException("Book not found");

    context.Update(request);
    context.SaveChanges();
    return book.Id;
});

app.MapDelete("/books/{id}", (int id, ApplicationDbContext context) =>
{
    var book = context.Books.FirstOrDefault(x => x.Id == id) 
        ?? throw new ArgumentNullException("Book not found");

    context.Remove(book);
    context.SaveChanges();

    return book.Id;
});

app.Run();
