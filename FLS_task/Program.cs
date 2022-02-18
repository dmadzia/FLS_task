using FLS_task.Commerce.Cart.Repository;
using FLS_task.Commerce.Cart.Services;
using FLS_task.Commerce.Catalog.Services;
using FLS_task.Commerce.Discounts.Services;
using FLS_task.Core;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["SessionTimeout"]));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new ViewLocationExpander());
});

builder.Services.AddTransient(typeof(ICatalogProvider), typeof(TestDataCatalogProvider));
builder.Services.AddTransient(typeof(ICartService), typeof(CartService));
builder.Services.AddTransient(typeof(ICartItemValidationService), typeof(CartItemValidationService));
builder.Services.AddTransient(typeof(IDiscountsProvider), typeof(TestDiscountsProvider));
builder.Services.AddTransient(typeof(IDiscountsProcessor), typeof(DiscountsProcessor));
builder.Services.AddTransient(typeof(ICartRepository), typeof(CartSessionRepository));

var app = builder.Build();

app.UseSession();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}");

app.Run();
