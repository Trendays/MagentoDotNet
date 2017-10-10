using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDotNet.Models.V2
{
    internal class ProductAdapter : IProduct
    {
        private Product p { get; set; }
        internal ProductAdapter(Product product) { p = product; }

        public int Id { get => p.Id; set => p.Id = value; }
        public string Name { get => p.Name; set => p.Name = value; }
        public decimal Price { get => p.Price; set => p.Price = value; }
        public string SKU { get => p.SKU; set => p.SKU = value; }

        public string Description
        {
            get
            {
                return p.CustomAttributes.Where(x => x.AttributeCode.ToLower() == "description").Select(x => x.Value).FirstOrDefault();
            }

            set
            {
                var attr = p.CustomAttributes.FirstOrDefault(x => x.AttributeCode.ToLower() == "description");

                if(attr != null)
                {
                    attr.Value = value;
                }
                else
                {
                    p.CustomAttributes.Add(new CustomAttribute()
                    {
                        AttributeCode = "description",
                        Value = value
                    });
                }
            }
        }
        
        public List<string> Tags { get; set; } = new List<string>(); // Tags are not supported in V2 http://tinyurl.com/ybfzfnja
    }
}
