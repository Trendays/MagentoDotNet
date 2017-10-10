using MagentoDotNet.Models;
using MagentoDotNet.Models.V2;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public class MagentoClientV2 : BaseMagentoClient
    {
        public MagentoClientV2(MagentoClientConfig config) : base(config) { }

        private RestRequest CreateListRequest(string endpoint, QueryFilter filter = null)
        {
            RestRequest request = new RestRequest
            {
                Resource = endpoint,
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
            };

            request.AddParameter("searchCriteria", "");

            return request;
        }

        public async override Task<List<IProduct>> GetProducts(QueryFilter filter = null)
        {
            var request = CreateListRequest("products", filter);
            var response = await ExecuteQuery<ProductResponse>(request);
            return response.Items.Select(x => new ProductAdapter(x) as IProduct).ToList();
        }

        public async override Task<IProduct> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
