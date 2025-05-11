using WebApplication2.controller;
using WebApplication2.repository;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication2.service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<LoanController>();
builder.Services.AddScoped<BookController>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

builder.Services.Configure<SmtpEmailService>(
    builder.Configuration.GetSection("EmailSettings"));


builder.Services.AddControllers();

builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=Books.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();



app.Run();