
using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
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
            builder.Services.AddDbContext<ManagementDbContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IBookInterface, BookRepository>();
            builder.Services.AddScoped<IStudentInterface, StudentRepository>();
            builder.Services.AddScoped<IAuthorInterface, AuthorRepository>();
            builder.Services.AddScoped<ILibraryInterface, LibraryRepository>();
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



            var app = builder.Build();

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
