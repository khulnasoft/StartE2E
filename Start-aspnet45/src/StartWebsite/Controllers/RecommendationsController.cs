using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Start.Models;
using Start.Recommendations;
using Start.Utils;

namespace Start.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly IStartContext db;
        private readonly IRecommendationEngine recommendation;

        public RecommendationsController(IStartContext context, IRecommendationEngine recommendationEngine)
        {
            db = context;
            recommendation = recommendationEngine;
        }

        public async Task<ActionResult> GetRecommendations(string productId)
        {
            if (!ConfigurationHelpers.GetBool("ShowRecommendations"))
            {
                return new EmptyResult();
            }

            var recommendedProductIds = await recommendation.GetRecommendationsAsync(productId);

            var recommendedProducts = await db.Products.Where(x => recommendedProductIds.Contains(x.ProductId.ToString())).ToListAsync();

            return PartialView("_Recommendations", recommendedProducts);
        }
    }
}
