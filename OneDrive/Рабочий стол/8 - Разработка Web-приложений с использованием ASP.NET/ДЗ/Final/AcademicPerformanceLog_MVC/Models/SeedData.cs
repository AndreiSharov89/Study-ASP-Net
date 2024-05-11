using AcademicPerformanceLog_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicPerformanceLog_MVC.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PerformanceLogContext(
                                        serviceProvider.GetRequiredService<DbContextOptions<PerformanceLogContext>>()))
            {
                if (context == null || context.Disciplines == null)
                {
                    throw new ArgumentNullException("Null context");
                }
                if (context.Disciplines.Any())
                    return;

                context.Disciplines.AddRange(
                    new Discipline
                    {
                        Name = "Технологии программирования"
                    },
                    new Discipline
                    {
                        Name = "Введение в базы данных"
                    },
                    new Discipline
                    {
                        Name = "Программирование на C#"
                    },
                    new Discipline
                    {
                        Name = "PostgreSQL"
                    },
                    new Discipline
                    {
                        Name = "Разработка Windows-приложений на C#"
                    },
                    new Discipline
                    {
                        Name = "Основы технологий разметки (HTML, XML)"
                    },
                    new Discipline
                    {
                        Name = "Технология ADO.NET"
                    },
                    new Discipline
                    {
                        Name = "Разработка Web-приложений с использованием технологии ASP.NET"
                    },
                    new Discipline
                    {
                        Name = "Программирование на Java"
                    },
                    new Discipline
                    {
                        Name = "Программирование на Python"
                    },
                    new Discipline
                    {
                        Name = "СУБД Oracle (SQL, PL/SQL)"
                    },
                    new Discipline
                    {
                        Name = "Программирование на C++"
                    },
                    new Discipline
                    {
                        Name = "Разработка Web-приложений на HTML5, JavaScript и CSS3"
                    });
                
                context.SaveChanges();
            }
            
        }
    }
}
