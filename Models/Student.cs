using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranBaVuBTH2.Models{
    public class Student{
        [Key]
        public string StudentID { get; set; }
        public string StudentName { get; set; }


        //Foreign key

        public string FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public Faculty? Faculty { get; set; }
    }
}