using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class PostViews
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}
