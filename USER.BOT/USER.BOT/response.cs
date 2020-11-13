using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USER.BOT
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class FriendsGet
    {
        public Response response { get; set; }
        public class Item
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool is_closed { get; set; }
            public bool can_access_closed { get; set; }
            public string photo_50 { get; set; }
            public int online { get; set; }
            public string track_code { get; set; }
            public List<int> lists { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class UsersGet
    {
        public List<Response> response { get; set; }
        public class Response
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool is_closed { get; set; }
            public bool can_access_closed { get; set; }
            public string photo_50 { get; set; }
            public string photo_100 { get; set; }
        }
    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GetProfileInfo
    {
        public Response response { get; set; }
        public class City
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Country
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Response
        {
            public string first_name { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
            public string home_town { get; set; }
            public string status { get; set; }
            public string bdate { get; set; }
            public int bdate_visibility { get; set; }
            public City city { get; set; }
            public Country country { get; set; }
            public string phone { get; set; }
            public int relation { get; set; }
            public int sex { get; set; }
        }
    }

}
