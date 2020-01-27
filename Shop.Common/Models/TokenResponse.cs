using Newtonsoft.Json;
using System;

namespace Shop.Common.Models
{
	public class TokenResponse
	{
		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("expiration")]
		public DateTime Expiration { get; set; }
	}

}
