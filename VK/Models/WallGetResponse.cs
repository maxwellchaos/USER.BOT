using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MethodButton.VK.Models
{
    public class WallGetResponse
    {
        private List<WallPost> wallPosts;

        /// <summary>
        /// Кол-во постов
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Список постов (в виде JArray)
        /// </summary>
        [JsonProperty("items")]
        public JArray Items { get; set; }

        public List<WallPost> GetWallPosts()
        {
            if (wallPosts == null)
            {
                wallPosts = Items.ToObject<WallPost[]>().ToList();
            }
            return wallPosts;
        }
    }
}
