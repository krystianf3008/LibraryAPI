using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using LibraryAPI.Middleware;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LibraryDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDB")));
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ExceptionHandler>();

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandler>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}