namespace Buổi_2_QuanLySinhVien.SubjectStudent
{
    // include properties: subID, Name, credit, teacher
    public class SubjectStudent
    {
        public string SubID { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string Teacher { get; set; }
        public List<Student> Students { get; set; }

        public SubjectStudent()
        {
            Students = new List<Student>
            {
                new Student { StuID = "S001", Name = "Nguyen Van A", MidPoint = 8.5, FinalPoint = 9.0, Email = "nguyenvana@example.com" },
                new Student { StuID = "S002", Name = "Tran Thi B", MidPoint = 7.0, FinalPoint = 8.0, Email = "tranthib@example.com" },
                new Student { StuID = "S003", Name = "Le Van C", MidPoint = 6.5, FinalPoint = 7.0, Email = "levanc@example.com" },
                new Student { StuID = "S004", Name = "Pham Thi D", MidPoint = 9.0, FinalPoint = 9.5, Email = "phamthid@example.com" }
            };
        }

        public List<Student> GetStudents() => Students;

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(string stuID)
        {
            Students.RemoveAll(s => s.StuID == stuID);
        }

        public void UpdateStudent(string stuID, Student updatedStudent)
        {
            var studentToUpdate = Students.FirstOrDefault(s => s.StuID == stuID);
            if (studentToUpdate != null)
            {
                studentToUpdate.Name = updatedStudent.Name;
                studentToUpdate.MidPoint = updatedStudent.MidPoint;
                studentToUpdate.FinalPoint = updatedStudent.FinalPoint;
                studentToUpdate.Email = updatedStudent.Email;
            }
        }

        public Student GetStudentById(string stuID)
        {
            return Students.FirstOrDefault(s => s.StuID == stuID);
        }

        public List<Student> GetStudentsByName(string name)
        {
            return Students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Student> getStudentsPassed(double passCondition)
        {
            return Students.Where(s => s.getSubjectPoint() >= passCondition).ToList();
        }

        public List<Student> getStudentsFailed(double passCondition)
        {
            return Students.Where(s => s.getSubjectPoint() < passCondition).ToList();
        }

        // Lấy về danh sách sinh viên mà mỗi sinh viên có điểm midPoint+1
        public List<Student> getStudentsMidPointPlus1()
        {
            // Cách 2: Dùng Select để tạo ra danh sách mới với MidPoint + 1
            return Students.Select(s => new Student
            {
                StuID = s.StuID,
                Name = s.Name,
                MidPoint = s.MidPoint + 1,
                FinalPoint = s.FinalPoint,
                Email = s.Email
            }).ToList();
        }
    }
}
