using CantinaWebAPI.EndPoints.Orders;
using CantinaWebAPI.EndPoints.Products;
using CantinaWebAPI.EndPoints.Users;
using CantinaWebAPI.EndPoints.Clients;
using CantinaWebAPI.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rabbit.Repositories.Interfaces;
using Rabbit.Repositories;
using Rabbit.Services.Interfaces;
using Rabbit.Services;
using CantinaWebAPI.EndPoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                    //Não desabilitar... Manter nível de exigência alto.
                    //options =>
                    //{
                    //    options.Password.RequireNonAlphanumeric = false;
                    //    options.Password.RequireDigit = false;
                    //    options.Password.RequireLowercase = false;
                    //    options.Password.RequiredLength = 3;
                    //}
                )
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
                };
            });
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
            app.UseAuthentication();
            app.UseAuthorization();


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


            app.MapMethods(ClientPost.Template, ClientPost.Methods, ClientPost.Handle);
            app.MapMethods(ClientGetAll.Template, ClientGetAll.Methods, ClientGetAll.Handle);

            app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);

            app.Run();
        }
    }
}