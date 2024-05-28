using System.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Security.JWT;
using static Core.Utilities.Security.JWT.JwtHelper;
using Core.DependencyResolvers;
using Core.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

//Autofac , Ninject ,CastleWindsor,SturctureMap, LightInject, DryInject --> IoC Container
//AOP --> Bir metodun önünde ,bir metodun sonunda ,bir metod hata verdiğinde çalışan kod parçacıklarını aop mimarisi yazılır.
//AOP -> Autofac yapıyor bunu 
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000"));
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Autofac Configure etmek için kullanılan kod
builder.Host
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureContainer<ContainerBuilder>(builder =>
              {
                  builder.RegisterModule(new AutofacBusinessModule());
              });



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();


builder.Services.AddAuthentication(options=> { 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

builder.Services.AddAuthorization();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});




var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

