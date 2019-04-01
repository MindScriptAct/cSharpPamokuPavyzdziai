using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace httpTesting
{
    class BeerClient
    {

        private const string BEER_URI = "http://ontariobeerapi.ca/";

        public List<Beer> GetAllBeer()
        {
            List<Beer> retVal = new List<Beer>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BEER_URI);
                using (HttpResponseMessage responce = client.GetAsync("products").Result)
                {
                    if (responce.IsSuccessStatusCode)
                    {
                        string result = responce.Content.ReadAsStringAsync().Result;

                        retVal = JsonConvert.DeserializeObject<List<Beer>>(result);

                    }
                }
            }



            return retVal;
        }
    }
}
