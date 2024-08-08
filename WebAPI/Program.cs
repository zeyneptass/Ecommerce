using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region IocContainer
// IOC Container
// Dal servisleri
builder.Services.AddSingleton<ICartItemDal, EfCartItemDal>();
builder.Services.AddSingleton<ICategoryDal, EfCategoryDal>();
builder.Services.AddSingleton<IOrderItemDal, EfOrderItemDal>();
builder.Services.AddSingleton<IOrderDal, EfOrderDal>();
builder.Services.AddSingleton<IProductImageDal, EfProductImageDal>();
builder.Services.AddSingleton<IProductDal, EfProductDal>();
builder.Services.AddSingleton<IShippingInfoDal, EfShippingInfoDal>();

// Business servisleri
builder.Services.AddSingleton<ICartItemService, CartItemManager>();
builder.Services.AddSingleton<ICategoryService, CategoryManager>();
builder.Services.AddSingleton<IOrderItemService, OrderItemManager>();
builder.Services.AddSingleton<IOrderService, OrderManager>();
builder.Services.AddSingleton<IProductImageService, ProductImageManager>();
builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<IShippingInfoService, ShippingInfoManager>();

builder.Services.AddTransient<IOrderService, OrderManager>();
builder.Services.AddTransient<IOrderItemService, OrderItemManager>();



#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
