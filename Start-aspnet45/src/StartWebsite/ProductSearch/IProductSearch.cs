using System.Collections.Generic;
using System.Threading.Tasks;
using Start.Models;

namespace Start.ProductSearch
{
    public interface IProductSearch
    {
        Task<IEnumerable<Product>> Search(string query);
    }
}
