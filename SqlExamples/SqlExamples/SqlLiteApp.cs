using SqlExamples.data;
using SqlExamples.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples
{
    public class SqlLiteApp
    {

        private IRepository<Student> studentRepository;


        public void StartSqlLite()
        {

            StudentRepositoryLite repo = new StudentRepositoryLite();

            if (repo.Connect())
            {
                Console.WriteLine("Student SQLite DB app started. What do you want to do?");
                studentRepository = repo;

                RenderMenu();
            }
            else
            {
                Console.WriteLine("Connection to DB failed. Sorry.. Bye!");
                Console.ReadKey();
            }
        }

        public void StartMySql()
        {

            StudentRepositoryMySql repo = new StudentRepositoryMySql();

            if (repo.Connect())
            {
                Console.WriteLine("Student MySQL DB app started. What do you want to do?");
                studentRepository = repo;

                RenderMenu();
            }
            else
            {
                Console.WriteLine("Connection to DB failed. Sorry.. Bye!");
                Console.ReadKey();
            }
        }

        public void StartSqlCompact()
        {

            StudentRepositorySqlCompact repo = new StudentRepositorySqlCompact();

            if (repo.Connect())
            {
                Console.WriteLine("Student MySQL DB app started. What do you want to do?");
                studentRepository = repo;
            
                RenderMenu();
            }
            else
            {
                Console.WriteLine("Connection to DB failed. Sorry.. Bye!");
                Console.ReadKey();
            }
        }

        private void RenderMenu()
        {
            do
            {
                Console.WriteLine();

                Console.WriteLine("1 - Print table.");
                Console.WriteLine("2 - Add data.");
                Console.WriteLine("3 - Update data.");
                Console.WriteLine("4 - Delete data.");

                ConsoleKeyInfo keyPress = Console.ReadKey(true);

                Console.Clear();

                switch (keyPress.Key)
                {
                    case ConsoleKey.D1: PrintTable(); break;
                    case ConsoleKey.D2: AddTableData(); break;
                    case ConsoleKey.D3: UpdateTableData(); break;
                    case ConsoleKey.D4: DeleteTableData(); break;
                }

            } while (true);
        }

        private void PrintTable()
        {
            Console.WriteLine("Table content : ");
            List<Student> students = studentRepository.GetAll();
            students.ForEach(Console.WriteLine);
        }

        private void AddTableData()
        {
            Console.WriteLine("Adding new student: ");
            Console.Write("Name:");
            string name = Console.ReadLine();
            Console.Write("Score:");
            float score;
            while (!float.TryParse(Console.ReadLine(), out score))
            {
                Console.Write("Please enter float number. Score:");
            }
            Console.Write("Town:");
            string city = Console.ReadLine();


            studentRepository.Add(new Student()
            {
                Name = name,
                Score = score,
                City = city
            });

        }

        private void UpdateTableData()
        {

            Console.WriteLine("Which student you want to update: ");
            Console.Write("Student id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Student id:");
            }

            Student student = studentRepository.Get(id);

            if (student != null)
            {

                //Console.WriteLine("Updating student - " + student);
                Console.Write("New name:");
                string name = Console.ReadLine();
                Console.Write("New score:");
                float score;
                while (!float.TryParse(Console.ReadLine(), out score))
                {
                    Console.Write("Please enter float number. Score:");
                }
                Console.Write("New city:");
                string city = Console.ReadLine();

                studentRepository.Update(new Student()
                {
                    Id = id,
                    Name = name,
                    Score = score,
                    City = city
                });


            }
            else
            {
                Console.WriteLine("Student with this id does not exist.");
            }

        }

        private void DeleteTableData()
        {
            Console.WriteLine("Which student you want to delete: ");
            Console.Write("Student id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter integer number. Student id:");
            }

            studentRepository.Delete(id);
        }
    }
}
