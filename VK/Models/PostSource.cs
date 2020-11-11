using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class PostSource
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
