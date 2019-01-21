using MySql.Data.MySqlClient;
using SqlExamples.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExamples.Repository
{
    class StudentRepositoryMySql : SqlRepository, IRepository<Student>
    {
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        public override bool Connect()
        {
            return OpenConnection(
                host: "sql7.freesqldatabase.com",
                dbName: "sql7273358",
                dbUser: "sql7273358",
                dbPassword: "zkhSZ4HjcI",
                port: 3306
            );
        }

        private bool OpenConnection(string host, string dbName, string dbUser, string dbPassword, int port)
        {
            string connstring = $"server={host}; database={dbName}; port={port}; user={dbUser}; password={dbPassword};";
            connection = new MySqlConnection(connstring);

            //TODO : check if table needs to be created.
            //InitTables();

            //TODO : check if connection is fine.

            return true;
        }

        private void InitTables()
        {
            string sql = @"CREATE TABLE `Students` ( 
                            `Id` INT NOT NULL AUTO_INCREMENT UNIQUE, 
                            `Name` TEXT NOT NULL , 
                            `Score` REAL NULL , 
                            `City` TEXT NULL , 

                            PRIMARY KEY (`Id`)
                        )";

            connection.Open();
            using (cmd = new MySqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        public List<Student> GetAll()
        {
            List<Student> retVal = new List<Student>();

            using (cmd = new MySqlCommand("SELECT * FROM Students", connection))
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
            using (cmd = new MySqlCommand("SELECT * FROM Students WHERE Id = @id", connection))
            {
                connection.Open();
                cmd.Parameters.Add(new MySqlParameter("id", id));
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
            // MySql nemegsta ' kabutes. ` tinka, arba nieko nedet tinka.. :\
            using (cmd = new MySqlCommand(@"INSERT INTO Students (`Name`, `Score`, `City`) 
                                                           VALUES(@name, @score, @city);", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("name", item.Name));
                cmd.Parameters.Add(new MySqlParameter("score", item.Score));
                cmd.Parameters.Add(new MySqlParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(Student item)
        {
            using (cmd = new MySqlCommand(@"UPDATE Students 
                                               SET Name = @name, Score = @score, City = @city 
                                             WHERE Id=@id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", item.Id));
                cmd.Parameters.Add(new MySqlParameter("name", item.Name));
                cmd.Parameters.Add(new MySqlParameter("score", item.Score));
                cmd.Parameters.Add(new MySqlParameter("city", item.City));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (cmd = new MySqlCommand(@"DELETE FROM Students WHERE ID = @id;", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new MySqlParameter("id", id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}
