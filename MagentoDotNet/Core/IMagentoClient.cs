using MagentoDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Core
{
    public interface IMagentoClient
    {
        Task<List<Product>> GetProducts(QueryFilter filter);
        Task<Product> GetProductById(int id);
    }
}
