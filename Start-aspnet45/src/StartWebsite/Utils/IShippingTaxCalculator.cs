using System.Collections.Generic;
using Start.Models;

namespace Start.Utils
{
	public interface IShippingTaxCalculator
	{
		OrderCostSummary CalculateCost(IEnumerable<ILineItem> items, string orderZip);
	}
}