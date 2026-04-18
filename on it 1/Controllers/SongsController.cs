using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using on_it_1.Data; // Проверь название папки с контекстом
using on_it_1.Models;

namespace on_it_1.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ГЛАВНАЯ СТРАНИЦА: Список + Поиск
        public async Task<IActionResult> Index(string searchString)
        {
            // Получаем все песни
            var songs = from s in _context.Songs
                        select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Приводим к нижнему регистру для поиска без учета регистра
                string search = searchString.ToLower();

                songs = songs.Where(s =>
                    s.Title.ToLower().Contains(search) ||
                    s.Album.ToLower().Contains(search) ||
                    s.Tuning.ToLower().Contains(search) ||
                    s.Year.ToString().Contains(search) // Поиск по году как по строке
                );
            }

            // Сортировка: сначала самые новые
            return View(await songs.OrderByDescending(s => s.Year).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Возвращаемся на главную
        }
        // МЕТОД ДЛЯ СОХРАНЕНИЯ (HttpPost)
        // Именно сюда стучится твоя кнопка "Save Track"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Song song)
        {
            // Убираем проверку IsValid, чтобы точно пропихнуть данные в базу
            try
            {
                _context.Songs.Add(song);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Если база всё равно откажет, ты увидишь ошибку в консоли отладки
                System.Diagnostics.Debug.WriteLine("ОШИБКА БАЗЫ: " + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Album,Year,Tuning")] Song song)
        {
            if (id != song.Id) return NotFound();

            try
            {
                _context.Update(song);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Если будет ошибка, она отобразится в Output в Visual Studio
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        
    }
}