using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpTesting
{
    class Beer
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("beer_id")]
        public int BeerId { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("abv")]
        public double Abv { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("attributes")]
        public string Atributes { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("brewer")]
        public string Brever { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("on_sale")]
        public bool OnSale { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Price)}: {Price},{nameof(Country)}: {Country},{nameof(OnSale)}: {OnSale} ";
        }
    }
}
