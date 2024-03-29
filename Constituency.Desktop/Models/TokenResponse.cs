﻿using Constituency.Desktop.Entities;
using Newtonsoft.Json;

namespace Constituency.Desktop.Models
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
