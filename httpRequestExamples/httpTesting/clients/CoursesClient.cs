using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
namespace httpTesting
{
    class CoursesClient
    {
        private const string uri = "https://vcsapi201809.azurewebsites.net/";


        public List<Course> GetCourses()
        {
            List<Course> courses = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = client.GetAsync("api/courses").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return CreateCourseResponses(result);
                    }
                    return courses;
                }
            }
        }


        public Course PostCourse(Course course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = client.PostAsJsonAsync("api/courses", course).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    return CreateCourseResponse(result);
                }
            }
        }

        public Course PutCourse(Course course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                var data = CreateJson(course);
                using (HttpResponseMessage response = client.PutAsJsonAsync($"api/courses/{course.Id}", CreateJson(course)).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    return CreateCourseResponse(result);
                }
            }
        }

        public void DeleteCourse(int id)
        {
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage response = client.DeleteAsync($"{uri}api/courses/{id}").Result)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }


        public Course UpdateProduct(Course course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = client.PutAsJsonAsync($"api/courses/{course.Id}", CreateJson(course)).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    return CreateCourseResponse(result);
                }
            }
        }


        public Course GetProduct(int id)
        {
            Course course = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = client.GetAsync($"api/courses/{id}").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return CreateCourseResponse(result);
                    }
                    return course;
                }
            }
        }





        private static Course CreateCourseResponse(string content)
        {
            return JsonConvert.DeserializeObject<Course>(content);
        }

        private static List<Course> CreateCourseResponses(string content)
        {
            return JsonConvert.DeserializeObject<List<Course>>(content);
        }

        private static StringContent CreateJson(Course course)
        {
            var json = JsonConvert.SerializeObject(course);
            //string json = "{\"id\": \"670\",    \"name\": \"ActionScript5\"}";
            var content = new StringContent(json);
            return content;
        }
    }
}
