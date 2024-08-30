using Microsoft.EntityFrameworkCore;
using A2.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using A2.Helper;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// register DBContext
builder.Services.AddDbContext<A2DbContext>(
    options => options.UseSqlite(builder.Configuration["A2DBConnection"]));


// resgister authentication 
builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("Authentication", null);

// set and register authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizerOnly",
    policy => policy.RequireClaim(ClaimTypes.Role, "Organizer"));

    options.AddPolicy("UserOnly",
    policy => policy.RequireClaim(ClaimTypes.Role, "User"));

    options.AddPolicy("AuthOnly", policy =>
    {
        policy.RequireAssertion(context => context.User.HasClaim(c =>
        (c.Value == "User" || c.Value == "Oranizer")));
    });
});

builder.Services.AddMvc(
    options => options.OutputFormatters.Add(new CalendarOutputFormatter()));



builder.Services.AddScoped<IA2Repo, A2Repo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();