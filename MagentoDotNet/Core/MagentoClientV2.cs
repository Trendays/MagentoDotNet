using MagentoDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public class MagentoClientV2 : BaseMagentoClient
    {
        public MagentoClientV2(MagentoConfig config) : base(config) { }

        public async override Task<List<Product>> GetProducts(QueryFilter filter = null)
        {
            var response = await ExecuteQuery<ProductResponse>("products", RestSharp.Method.GET);
            return response.Items;
        }

        public async override Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
