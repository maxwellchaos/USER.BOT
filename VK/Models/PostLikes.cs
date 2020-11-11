using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class PostLikes
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("user_likes")]
        public int UserLikes { get; set; }

        [JsonProperty("can_like")]
        public bool CanLike { get; set; }

        [JsonProperty("can_publish")]
        public bool CanPublish { get; set; }
    }
}
