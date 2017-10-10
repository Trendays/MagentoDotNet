using MagentoDotNet.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public abstract class BaseMagentoClient : IMagentoClient
    {
        private MagentoClientConfig Config { get; set; }
        protected RestClient RestClient { get; private set; }

        public BaseMagentoClient(MagentoClientConfig config)
        {
            Config = config;

            RestClient = new RestClient(Config.BaseUrl + (Config.UseRewritedUrlFormat ? "/api/rest/" : "/index.php/rest/default/V1/"));
            RestClient.AddDefaultHeader(Config.ContentTypeHeaderWithUnderscore ? "Content_Type" : "Content-Type", "application/json");
            RestClient.Authenticator = OAuth1Authenticator.ForProtectedResource(
                        Config.ConsumerKey,
                        Config.ConsumerSecret,
                        Config.AccessToken,
                        Config.AccessTokenSecret);
        }
        
        protected async Task<T> ExecuteQuery<T>(RestRequest request)
        {
            var response = await RestClient.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                throw new Exception("Request failed", response.ErrorException);
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Request failed with status code : " + (int)response.StatusCode);
            }

            return response.Data;
        }

        public abstract Task<List<IProduct>> GetProducts(QueryFilter filter);
        public abstract Task<IProduct> GetProductById(int id);
    }
}
