using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using PBL3.Infrastructures;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
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

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 200000000;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 200000000;
    options.MultipartBodyLengthLimit = 200000000; // default 128Mb
    options.MultipartHeadersLengthLimit = 200000000;
});

// Custom service
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<FileService>();

builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();
builder.Services.AddScoped<IEmotionRepository, EmotionRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IRepository<Story>, Repository<Story>>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderType>, Repository<OrderType>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


// Paypal service
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));
builder.Services.AddSingleton<PayPalService>();

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
//app.Services.CreateScope().ServiceProvider.GetService<SeedData>()?.SeedExampleData();

app.Run();
