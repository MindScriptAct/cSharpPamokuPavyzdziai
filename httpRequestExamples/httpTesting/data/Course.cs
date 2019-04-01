using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpTesting
{
    class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }



        public override string ToString()
        {
            return $"Course({Id}, {Name}, {GetStudentList()})";
        }

        private object GetStudentList()
        {
            string retVal = "[";
            for (int s = 0; s < Students.Count; s++)
            {
                retVal += Students[s].Id;
            }
            retVal += "]";
            return retVal;

        }
    }
}
