using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PBL3.Infrastructures;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;
using PBL3.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PBL3"));
});

// Thêm service cho Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password validation
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    // User validation
    options.User.RequireUniqueEmail = true; // Mỗi Email chỉ đki đc 1 tài khoản
});
// Custom service
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<FileService>();

builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();
builder.Services.AddScoped<IEmotionRepository, EmotionRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();

// Seed data
builder.Services.AddScoped<SeedData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Applies any pending migrations for the context to the database
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();
// Seed data
await AppDbContext.SeedData(app.Services.CreateScope().ServiceProvider, app.Configuration);

// Seed example data
app.Services.CreateScope().ServiceProvider.GetService<SeedData>()?.SeedExampleData();

app.Run();
