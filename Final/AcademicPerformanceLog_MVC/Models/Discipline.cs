using System.ComponentModel;

namespace AcademicPerformanceLog_MVC.Models
{
    public class Discipline
    {
        public int ID { get; set; }
        [DisplayName("Назание предмета")]
        public string Name { get; set; }
        
        public override string ToString()
        {
            return Name;
        }

        public List<Student> Students { get; } = [];
        public List<Performance> StudentsDisciplines { get; } = [];
    }
}
