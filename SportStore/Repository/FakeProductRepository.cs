using SportStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name="Football",Price = 25},
            new Product {Name="Surf Board", Price=179},
            new Product {Name="Running Shoes",Price=95}
        }.AsQueryable<Product>();

    }
}
