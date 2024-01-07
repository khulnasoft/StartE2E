﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Start.Models;

namespace Start.ProductSearch
{
    public class StringContainsProductSearch : IProductSearch
    {
        private readonly IStartContext _context;

        public StringContainsProductSearch(IStartContext context)
        {
            _context = context;
        }

		// TODO: Change this to return List of ProductViewModel?
        public async Task<IEnumerable<Product>> Search(string query)
        {
			try
			{
				var cleanQuery = Depluralize(query);

				var q = _context.Products
					.Where(p => p.Title.ToLower().Contains(cleanQuery));

				return await q.ToListAsync();
			}
			catch
			{
				return new List<Product>();
			}
        }

		public string Depluralize(string query)
		{
			if (query.EndsWith("ies"))
			{
				query = query.Substring(0, query.Length - 3) + "y";
			}
			else if (query.EndsWith("es"))
			{
				query = query.Substring(0, query.Length - 1);
			}
			else if (query.EndsWith("s"))
			{
				query = query.Substring(1, query.Length);
			}
			return query.ToLowerInvariant();
		}
	}
}
