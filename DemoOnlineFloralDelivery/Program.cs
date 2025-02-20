

using DemoOnlineFloralDelivery.Converters;
using DemoOnlineFloralDelivery.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DemoOnlineFloralDelivery.Service;

var builder = WebApplication.CreateBuilder(args);
//======
builder.Services.AddCors();

// AddJsonOptions(...) : dung de khai bao gi...
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});
// doc chuoi ket noi ra va ket noi db
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddScoped<BouquetService, BouquetServiceImpl>();
builder.Services.AddScoped<ImageService, ImageServiceImpl>();
builder.Services.AddScoped<EventService, EventServiceImpl>();
builder.Services.AddScoped<CategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<CommentService, CommentServiceImpl>();
builder.Services.AddScoped<CartService, CartServiceImpl>();
builder.Services.AddScoped<AccountService, AccountServiceImpl>();
builder.Services.AddScoped<ContactService, ContactServiceImpl>();
builder.Services.AddScoped<RoleService, RoleServiceImpl>();
builder.Services.AddScoped<DeliveryService, DeliveryServiceImpl>();
builder.Services.AddScoped<BouquetEventService, BouquetEventServiceImpl>();
builder.Services.AddScoped<OrderService, OrderServiceImpl>();
builder.Services.AddScoped<OrderDetailService, OrderDetailServiceImpl>();
var app = builder.Build();
//lay addcors xuong su dung
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseStaticFiles();
app.MapControllers();

app.Run();
