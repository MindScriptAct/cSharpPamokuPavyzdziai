using SqlExamples.data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples.Repository
{
    class StudentRepositorySqlCompact : SqlRepository, IRepository<Student>
    {
        private SqlConnection connection;
        private SqlCommand cmd;
        private SqlDataReader dr;

        public override bool Connect()
        {
            return OpenConnection(
                host: @"(localdb)\MSSQLLocalDB",
                dbName: "VcsDb"
            );
        }

        private bool OpenConnection(string host, string dbName)
        {
            string connstring = $"Data Source={host}; Initial Catalog={dbName}";

            connection = new SqlConnection(connstring);

            //TODO : check if table needs to be created.
            //InitTables();

            //TODO : check if connection is fine.

            return true;
        }

        private void InitTables()
        {
            string sql = @"CREATE TABLE Students ( 
                            Id INT IDENTITY(1,1) NOT NULL UNIQUE, 
	                        Name TEXT NOT NULL , 
	                        Score REAL NULL , 
	                        City TEXT NULL , 

                        	PRIMARY KEY (Id)
                        )";

            connection.Open();
            using (cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<Student> GetAll()
        {
            List<Student> retVal = new List<Student>();

            using (cmd = new SqlCommand("SELECT * FROM Students", connection))
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
            using (cmd = new SqlCommand("SELECT * FROM Students WHERE Id = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", id));
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
            using (cmd = new SqlCommand(@"INSERT INTO Students (Name, Score, City) 
                                                         VALUES(@name, @score, @city);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SqlParameter("name", item.Name));
                cmd.Parameters.Add(new SqlParameter("score", item.Score));
                cmd.Parameters.Add(new SqlParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }

        }

        public void Update(Student item)
        {
            using (cmd = new SqlCommand(@"UPDATE Students 
                                             SET Name = @name, Score = @score, City = @city 
                                           WHERE Id=@id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SqlParameter("id", item.Id));
                cmd.Parameters.Add(new SqlParameter("name", item.Name));
                cmd.Parameters.Add(new SqlParameter("score", item.Score));
                cmd.Parameters.Add(new SqlParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new SqlCommand(@"DELETE FROM Students WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}
