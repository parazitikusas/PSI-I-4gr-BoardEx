using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServerSideBlazor();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<BoardExDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BoardExDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BoardExAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddScoped<IBoardAdRepository, BoardAdRepository>();        // inject implementation of interface
builder.Services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBoardAdLikeRepository, BoardAdLikeRepository>();
builder.Services.AddScoped<IBoardAdCommentRepository, BoardAdCommentRepository>();
builder.Services.AddScoped<ILogsRepository, LogsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapControllers();

app.MapBlazorHub();

app.Run();
