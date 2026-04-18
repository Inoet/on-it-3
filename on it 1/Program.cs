using Microsoft.EntityFrameworkCore;
using on_it_1.Data;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddControllersWithViews();

// Подключение PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// АВТОМАТИЧЕСКАЯ МИГРАЦИЯ (Создаст таблицу при запуске)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        //context.Database.Migrate();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        Console.WriteLine($"Ошибка БД: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Songs}/{action=Index}/{id?}"); // Сразу грузим список песен

app.Run();