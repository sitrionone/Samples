using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONECardPublisher
{
    public class CardRouting
    {
        [JsonProperty(Required = Required.AllowNull)]
        public SimplePerson Actor { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public SimplePerson[] SendTo { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string CardId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string MessageType { get; set; }

        [JsonProperty(Required = Required.Always)]
        public CardActionType ActionType { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public Dictionary<string, string> Data { get; set; }

        [JsonProperty(Required = Required.AllowNull)]
        public DateTime CardDate { get; set; }
    }

    public enum CardActionType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }

    public class SimplePerson
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }

    public static class CardRoutingExtensions
    {
        /// <summary>
        /// Returns the object as a JavaScript Serialized (JSON) string.
        /// </summary>
        /// <returns>a JavaScript Serialized (JSON) string representing the current object instance</returns>
        public static string ToJson(this CardRouting data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
