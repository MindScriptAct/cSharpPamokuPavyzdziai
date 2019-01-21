using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples.data
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Score { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"Student(Id:{Id}, Name:{Name}, Score:{Score}, City:{City})";
        }
    }
}
