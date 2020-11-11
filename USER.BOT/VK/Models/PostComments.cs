using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class PostComments
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("can_post")]
        public int CanPost { get; set; }

        [JsonProperty("groups_can_post")]
        public bool GroupsCanPost { get; set; }
    }
}
