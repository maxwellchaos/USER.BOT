using MethodButton.VK.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class WallGetArguments : VkArguments
    {
        [Description("owner_id")]
        public long? OwnerId { get; set; }

        [Description("domain")]
        public string Domain { get; set; }

        [Description("offset")]
        public int? Offset { get; set; }

        [Description("count")]
        public int? Count { get; set; } = 10;

        [Description("filter")]
        public string Filter { get; set; }

        [Description("extended")]
        public bool? Extended { get; set; }

        [Description("fields")]
        public string[] Fields { get; set; }

        public override string ToString() => GetStringResult(this.GetType(), this);

    }
}
