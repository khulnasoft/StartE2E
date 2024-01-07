using System.Collections.Generic;
using System.Threading.Tasks;

namespace Start.Recommendations
{
    public class NaiveRecommendationEngine : IRecommendationEngine
    {
        public Task<IEnumerable<string>> GetRecommendationsAsync(string productId)
        {
            return Task.FromResult<IEnumerable<string>>(new[] { productId });
        }
    }
}