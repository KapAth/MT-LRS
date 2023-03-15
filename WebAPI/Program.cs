using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repository;
using Serilog;
using Services;
using WebAPI.Configurations;
using WebAPI.Middleware;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//get connection string used to connect to database
var connectionString = builder.Configuration.GetConnectionString("DbString");
builder.Services.AddDbContext<MTLRSDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//a policy to allow every source outside the api to access the api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUserTitlesService, UserTitlesService>();
builder.Services.AddScoped<IUserTypesService, UserTypesService>();
//ensures that each request has its own separate database context and avoids concurrency and data consistency issues
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserTitlesRepository, UserTitlesRepository>();
builder.Services.AddScoped<IUserTypesRepository, UserTypesRepository>();

//in order to use serilog
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Serilog automatically start logging https requests etc.
app.UseSerilogRequestLogging();

//Exception Handler for endpoints
app.UseMiddleware<ExceptionHandlingMiddleware>();

//tell the pipeline to use the policy we created above
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

//app.MapUserEndpoints();

app.Run();