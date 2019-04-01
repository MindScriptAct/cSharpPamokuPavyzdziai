using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace httpTesting
{
    class Program
    {
        static void Main(string[] args)
        {


            /* GOOGLE */
            //TestGoogle();
            
            /* beer client test */
            //TestBeerClient();

            /* Custom client */
            //GetTest();
            //GetTestAsync();
            //PostTest();
            //PutTest();
            //DeleteTest();

            Console.ReadKey();
        }

        private static void TestGoogle()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com/");
            string result = client.GetStringAsync("").Result;
            Console.WriteLine(result);
        }


        private static void DeleteTest()
        {
            CoursesClient client = new CoursesClient();

            client.DeleteCourse(670);

        }

        private static void PostTest()
        {
            CoursesClient client = new CoursesClient();

            Course result = client.PostCourse(new Course()
            {
                Name = "Java3"
            });


            Console.WriteLine(result);

        }

        private static void PutTest()
        {
            CoursesClient client = new CoursesClient();

            Course result = client.UpdateProduct(new Course()
            {
                Id = 670,
                Name = "Java3"
            });


            Console.WriteLine(result);


        }

        private static void GetTest()
        {
            CoursesClient client = new CoursesClient();

            List<Course> test = client.GetCourses();

            test.ForEach(Console.WriteLine);
        }

        private async static void GetTestAsync()
        {
            HttpClient client = new HttpClient();
            string url = String.Format("https://vcsapi201809.azurewebsites.net/api/courses");
            var response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            //var serializer = new DataContractJsonSerializer(typeof());

            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //var data = (RootObject)serializer.ReadObject(ms);

            //return data;




            //string json = JsonConvert.SerializeObject(account, Formatting.Indented);




        }



        private static void TestBeerClient()
        {
            BeerClient client = new BeerClient();

            List<Beer> beers = client.GetAllBeer();


            var coronaBeers = from beer in beers
                              where beer.Name.Contains("Corona")
                              select beer;

            foreach (var item in coronaBeers)
            {
                Console.WriteLine(item);
            }
        }
    }
}
