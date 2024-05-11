using System.ComponentModel;

namespace AcademicPerformanceLog_MVC.Models
{
    public class Student
    {
        public int ID { get; set; }
        [DisplayName("Имя")]
        public string Firstname { get; set; }
        [DisplayName("Фамилия")]
        public string Lastname { get; set; }
        [DisplayName("Средний балл")]
        public string Score { get; set; }

        public override string ToString() => Firstname + " " + Lastname;

        public List<Discipline> Disciplines { get; } = [];
        public List<Performance> StudentsDisciplines { get; } = [];

    }
}
