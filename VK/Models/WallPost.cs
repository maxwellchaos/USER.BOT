using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK.Models
{
    public class WallPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("from_id")]
        public int FromId { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("marked_as_ads")]
        public bool MarkedAsAds { get; set; }

        [JsonProperty("post_type")]
        public string PostType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public JArray AttachmentsJson { get; set; }

        [JsonProperty("post_source")]
        public PostSource PostSource { get; set; }

        [JsonProperty("comments")]
        public PostComments Comments { get; set; }

        [JsonProperty("likes")]
        public PostLikes Likes { get; set; }

        [JsonProperty("reposts")]
        public PostReposts Reposts { get; set; }

        [JsonProperty("views")]
        public PostViews Views { get; set; }

        [JsonProperty("is_favourite")]
        public bool IsFavorite { get; set; }
    }
}
