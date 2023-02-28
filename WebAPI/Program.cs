using Microsoft.EntityFrameworkCore;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//connect to database
//var connectionString = builder.Configuration.GetConnectionString("DbString");
//builder.Services.AddDbContext<MTLRSDbContext>(options =>
//{
//    options.UseSqlServer(connectionString);
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//a policy to allow every source outside the api to access the api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//in order to use serilog
builder.Host.UseSerilog((ctx,lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Serilog automaticaly start loging https requests etc.
app.UseSerilogRequestLogging();

//tell the pipeline to use the policy we created above
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

//app.MapUserEndpoints();

app.Run();
