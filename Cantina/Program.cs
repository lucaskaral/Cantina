using CantinaWebAPI.EndPoints.Orders;
using CantinaWebAPI.EndPoints.Products;
using CantinaWebAPI.EndPoints.Users;
using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rabbit.Repositories.Interfaces;
using Rabbit.Repositories;
using Rabbit.Services.Interfaces;
using Rabbit.Services;

namespace CantinaWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEntityFrameworkNpgsql()
                                .AddDbContext<ApplicationDbContext>(options => options
                                    .UseNpgsql(builder.Configuration.GetConnectionString("CantinaWebAPIDb")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IRabbitMessageService, RabbitMessageService>();
            builder.Services.AddTransient<IRabbitMessageRepository, RabbitMessageRepository>();

            builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));


            var app = builder.Build();
            app.UseCors("corspolicy");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapMethods(OrderGetAll.Template, OrderGetAll.Methods, OrderGetAll.Handle);
            app.MapMethods(OrderDelete.Template, OrderDelete.Methods, OrderDelete.Handle);
            app.MapMethods(OrderGetById.Template, OrderGetById.Methods, OrderGetById.Handle);
            app.MapMethods(OrderPut.Template, OrderPut.Methods, OrderPut.Handle);
            app.MapMethods(OrderPost.Template, OrderPost.Methods, OrderPost.Handle);

            app.MapMethods(ProductPut.Template, ProductPut.Methods, ProductPut.Handle);
            app.MapMethods(ProductGetAll.Template, ProductGetAll.Methods, ProductGetAll.Handle);
            app.MapMethods(ProductDelete.Template, ProductDelete.Methods, ProductDelete.Handle);
            app.MapMethods(ProductGetById.Template, ProductGetById.Methods, ProductGetById.Handle);
            app.MapMethods(ProductPost.Template, ProductPost.Methods, ProductPost.Handle);

            app.MapMethods(UserPut.Template, UserPut.Methods, UserPut.Handle);
            app.MapMethods(UserGetAll.Template, UserGetAll.Methods, UserGetAll.Handle);
            app.MapMethods(UserPost.Template, UserPost.Methods, UserPost.Handle);
            app.MapMethods(UserDelete.Template, UserDelete.Methods, UserDelete.Handle);
            app.MapMethods(UserGetById.Template, UserGetById.Methods, UserGetById.Handle);
            app.MapMethods(UserGetByEmailAndPassword.Template, UserGetByEmailAndPassword.Methods, UserGetByEmailAndPassword.Handle);

            app.Run();
        }
    }
}