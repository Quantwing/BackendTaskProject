using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BackendTaskProject.Data;
using System;
using System.Linq;

namespace BackendTaskProject.Models
{
    public static class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookContext>>()))
            {
                if (context.BookModel.Any())
                {
                    // I don't know how to clear database so here's the hacky way to do it.
                    //context.BookModel.RemoveRange(context.BookModel.ToArray());
                    return;   // DB has been seeded
                }
                context.BookModel.AddRange(
                    new BookModel { Title = "Pino Paperia", Author = "Minä", Description = "Kirja" },
                    new BookModel { Title = "Kasa Kirjoitusta", Author = "Sinä", Description = "Vihko" },
                    new BookModel { Title = "Ryhmä Raapustuksia", Author = "Hän", Description = "Teos" },
                    new BookModel { Title = "Tiivistelmä Tuherruksia", Author = "Ne", Description = "Opus" }
                );
                context.SaveChanges();
            }
        }
    }
}
