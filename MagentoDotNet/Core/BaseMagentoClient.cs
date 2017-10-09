using MagentoDotNet.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public abstract class BaseMagentoClient : IMagentoClient
    {
        private MagentoConfig Config { get; set; }
        protected RestClient RestClient { get; private set; }

        public BaseMagentoClient(MagentoConfig config)
        {
            Config = config;

            RestClient = new RestClient(Config.BaseUrl + (Config.UseRewritedUrlFormat ? "/api/rest/" : "/index.php/rest/default/V1/"));
            RestClient.AddDefaultHeader(Config.ContentTypeHeaderWithUnderscore ? "Content_Type" : "Content-Type", "application/json");
        }

        protected async Task<T> ExecuteQuery<T>(string endpoint, Method method)
        {
            var request = new RestRequest
            {
                Resource = endpoint,
                Method = method,
                RequestFormat = DataFormat.Json,
            };

            var response = await RestClient.ExecuteTaskAsync<T>(request);

            if(response.ErrorException != null)
            {
                throw new Exception("Request failed", response.ErrorException);
            }

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Request failed with status code : " + (int)response.StatusCode);
            }

            return response.Data;
        }

        public abstract Task<List<Product>> GetProducts(QueryFilter filter);
        public abstract Task<Product> GetProductById(int id);
    }
}
