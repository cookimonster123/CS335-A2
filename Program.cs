using Microsoft.EntityFrameworkCore;
using A2.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<A2DbContext>(options => options.UseSqlite(builder.Configuration["A2DBConnection"]));



builder.Services.AddScoped<IA2Repo, A2Repo>();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); // do we need this???

app.UseAuthorization();

app.MapControllers();

app.Run();