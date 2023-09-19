using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SampleCore.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// To get access token from Auth0.
        /// </summary>
        /// <response code="200">Successful</response>
        [HttpGet]
        public async Task<ActionResult<Token>> GetAccessToken()
        {
            HttpClient client = new HttpClient();
           
            string baseAddress =  $"https://{_config.GetValue<string>("Auth0:Domain")}/oauth/token";
            string grant_type = "client_credentials";
            string client_id = _config.GetValue<string>("Auth0:ClientId");
            string client_secret = _config.GetValue<string>("Auth0:ClientSecret");
            string audience = _config.GetValue<string>("Auth0:Audience");

            var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                    {"audience", audience},
                };

            HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token token = JsonConvert.DeserializeObject<Token>(jsonContent);
            return token;
        }
    }
}
