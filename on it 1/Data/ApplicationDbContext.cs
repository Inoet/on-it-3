using Microsoft.EntityFrameworkCore;
using on_it_1.Models; // Убедись, что пространство имен совпадает с твоим проектом

namespace on_it_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Твоя таблица с песнями
        public DbSet<Song> Songs { get; set; }

        // Конструктор
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Этот метод принудительно задает настройки подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если конфигурация еще не задана (например, в тестах или миграциях)
            if (!optionsBuilder.IsConfigured)
            {
              
                optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=NirvanaDB;Username=postgres;Password=paleo");
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NirvanaDB;Username=postgres;Password=paleo");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Здесь можно настроить связи, если они появятся позже
        }

    }
}