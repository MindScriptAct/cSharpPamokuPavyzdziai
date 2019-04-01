using SqlExamples.data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples.Repository
{
    class StudentRepositoryLite : SqlRepository, IRepository<Student>
    {
        private SQLiteConnection connection;
        private SQLiteCommand cmd;
        private SQLiteDataReader dr;

        public override bool Connect()
        {
            return OpenConnection("MyDatabase.sqlite");
        }

        private bool OpenConnection(string dbFalieName)
        {
            if (!File.Exists(dbFalieName))
            {
                SQLiteConnection.CreateFile(dbFalieName);
                connection = new SQLiteConnection("Data Source=" + dbFalieName + ";Version=3;");
                InitTables();
            }
            else
            {
                connection = new SQLiteConnection("Data Source=" + dbFalieName + ";Version=3;");
            }

            return true;
        }

        protected void InitTables()
        {
            string sql = @"CREATE TABLE Students (
	                        'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                        'Name'	TEXT NOT NULL,
	                        'Score'	REAL,
	                        'City'	TEXT
                         );";

            connection.Open();
            using (cmd = new SQLiteCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<Student> GetAll()
        {
            List<Student> retVal = new List<Student>();

            using (cmd = new SQLiteCommand("SELECT * FROM Students", connection))
            {
                connection.Open();
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal.Add(new Student()
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Score = dr.GetFloat(2),
                            City = dr.GetString(3)
                        });

                    }
                }
                connection.Close();
            }

            return retVal;
        }

        public Student Get(int id)
        {
            Student retVal = null;
            using (cmd = new SQLiteCommand("SELECT * FROM Students WHERE Id = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new SQLiteParameter("id", id));
                using (dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        retVal = new Student()
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Score = dr.GetFloat(2),
                            City = dr.GetString(3)
                        };
                    }
                }
                connection.Close();
            }
            return retVal;
        }

        public void Add(Student item)
        {
            using (cmd = new SQLiteCommand(@"INSERT INTO Students('Name','Score','City') 
                                                           VALUES(@name, @score, @city);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SQLiteParameter("name", item.Name));
                cmd.Parameters.Add(new SQLiteParameter("score", item.Score));
                cmd.Parameters.Add(new SQLiteParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }

        }

        public void Update(Student item)
        {

            using (cmd = new SQLiteCommand(@"UPDATE Students 
                                                SET Name = @name, Score = @score, City = @city 
                                                WHERE Id=@id;", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new SQLiteParameter("id", item.Id));
                cmd.Parameters.Add(new SQLiteParameter("name", item.Name));
                cmd.Parameters.Add(new SQLiteParameter("score", item.Score));
                cmd.Parameters.Add(new SQLiteParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new SQLiteCommand(@"DELETE FROM Students WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SQLiteParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }


    }
}
