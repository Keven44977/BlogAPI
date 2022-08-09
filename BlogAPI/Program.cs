using BlogAPI.Services;
using Database;
using Microsoft.EntityFrameworkCore;
using Repositories.CategoriesRepository;
using Repositories.PostsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add context with npgsql
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseNpgsql(DatabaseConnexion.GetConnexionString());
});

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IPostsRepository, PostsRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPostsService, PostsService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string[] allowCors = { "http://localhost:8080" };

app.UseCors(options =>
options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(allowCors)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
