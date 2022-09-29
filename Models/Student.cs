namespace DemoMVC.Models
{
    public class Student
    {
        string iD, name;
        int age;

        public Student(string id, string name , int age)
        {
            ID = id;
            Name = name;
            Age = age;
        }
        public Student() { }

        public int Age { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
