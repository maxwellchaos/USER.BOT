using MethodButton.VK.Abstract;
using MethodButton.VK.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Methods
{
    public class Wall : MethodGroup
    {
        public override string Token { get; set; } = "";

        public Wall(string token)
        {
            this.Token = token;
        }

        public List<WallPost> Get(WallGetArguments arguments)
        {
            var response = new WebClient().DownloadString($"https://api.vk.com/method/wall.get?{arguments.ToString()}access_token={Token}&v=5.124");
            var json = JObject.Parse(Encoding.UTF8.GetString(Encoding.Default.GetBytes(response)));
            var wallGetResponse = json.SelectToken("response").ToObject<WallGetResponse>();
            return wallGetResponse.GetWallPosts();
        }
    }
}
