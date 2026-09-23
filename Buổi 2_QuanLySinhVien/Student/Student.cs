using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buổi_2_QuanLySinhVien.Student
{
    public class Student
    {
        string id;
        string name;
        DateTime birthday;
        [Required]
        public DateTime BirthDay { get; set; }
        public String FullName { get; set; }
        public string ID
        {
            get { return id; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("id cannot be null");
                }
                { id = value; }
            }
        }
        Student(string id, string name, DateTime birthday)
        {
            this.ID = id;
            this.FullName = name;
            this.BirthDay = birthday;
        }
        public String getLastName()
        {
            string[] nameParts = FullName.Trim().Split(' ');
            return nameParts[nameParts.Length - 1];
        }
        public String getFirstName()
        {
            string[] nameParts = FullName.Trim().Split(' ');
            return nameParts[0];
        }
    }
}