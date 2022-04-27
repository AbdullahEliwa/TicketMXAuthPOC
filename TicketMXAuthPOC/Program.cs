using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TicketMXAuthPOC.Data;
using TicketMXAuthPOC.Models;
using TicketMXAuthPOC.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

#region Http Client.
builder.Services.AddHttpClient("TicketMXApiClient", client =>
{
    client.DefaultRequestHeaders.Clear();
    client.BaseAddress = new Uri("https://devapi.ticketmx.com");
    client.DefaultRequestHeaders.Add("x-api-key", constants.X_Api_Key);
    client.DefaultRequestHeaders.Add("clientId", constants.ClientId);
    client.DefaultRequestHeaders.Add("clientToken", constants.ClientToken);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("TicketMXClient", client =>
{
    client.DefaultRequestHeaders.Clear();
    client.BaseAddress = new Uri("https://dev.ticketmx.com");
    client.DefaultRequestHeaders.Add("x-api-key", constants.X_Api_Key);
    client.DefaultRequestHeaders.Add("clientId", constants.ClientId);
    client.DefaultRequestHeaders.Add("clientToken", constants.ClientToken);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
#endregion


builder.Services.AddScoped<ITicketMXService, TicketMXService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
