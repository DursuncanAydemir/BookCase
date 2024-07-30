using Autofac;
using BookCase.Core.Extensions;
using Autofac.Extensions.DependencyInjection;
//using BookCase.Business.DependencyResolvers.Autofac;
//using BookCase.Core.DependencyResolvers;
//using BookCase.Core.Utilities.IoC;
using BookCase.Core.Utilities.Security.Encryption;
using BookCase.Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BookCase.DataAccess.Concrete.EntityFramework;
using BookCase.Business.Abstract;
using BookCase.Business.Concrete;
//using BookCase.Core.Helpers.FileHelper;
using BookCase.DataAccess.Abstract;
using Serilog;
using Autofac.Core;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new AutofacBusinessModule());
//});

builder.Services.AddCors();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// Log configuration
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
// Add Serilog Library
builder.Logging.AddSerilog(logger);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            //builder.Services.AddDependencyResolvers(new ICoreModule[]
            //{
            //    new CoreModule()
            //});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal,EfUserDal>();

builder.Services.AddSingleton<INoteService, NoteManager>();
builder.Services.AddSingleton<INoteDal, EfNoteDal>();

builder.Services.AddSingleton<IAuthService, AuthManager>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();


builder.Services.AddSingleton<ICategoryDal, EfCategoryDal>();

builder.Services.AddSingleton<ISubCategoryDal, EfSubCategoryDal>();

//builder.Services.AddSingleton<IFileHelper, FileHelperManager>();

builder.Services.AddSingleton<IBookService, BookManager>();
builder.Services.AddSingleton<IBookDal, EfBookDal>();

//builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
