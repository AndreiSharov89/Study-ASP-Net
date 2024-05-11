using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;


namespace AcademicPerformanceLog_MVC.Models
{
    public class Performance
    {
        //[ForeignKey("Student")]
        public int StudentID { get; set; }
        //[ForeignKey("Discipline")]
        public int DisciplineID { get; set; }
        [DisplayName("Оценка")]
        [IntegerValidator(MinValue = 0, MaxValue = 5)]
        public int Mark { get; set; }
        
        public override string ToString()
        {
            return Mark.ToString();
        }


    }
}
