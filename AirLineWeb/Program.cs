using AirLineWeb.Data;
using AirLineWeb.MessageBus;
using AirLineWeb.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirLineWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddAutoMapper(typeof(FlightMappingConfig));
            builder.Services.AddSingleton<IMessageBusClient,MessageBusClient>();
            builder.Services.AddDbContext<AirlineDbContext>(options=>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("AirlineConnection")));
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}