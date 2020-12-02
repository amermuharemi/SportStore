using SportStore.Data;
using SportStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        public ProductRepository(AppDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Product> Products => context.Products;
    }
}
